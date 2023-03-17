using MPI;


namespace QuickSort
{
    public class Program
    {
        private static Intracommunicator comm;
        private static int commSize;
        private static int commRank;

        private static string readPath;
        private static string writePath;
        private static string separator;

        private static bool isFileReadable = false;
        private static bool isFileWritable = false;

        private static List<int> data = new List<int>();

        public static int Main(string[] args)
        {
            using (var env = new MPI.Environment(ref args))
            {
                comm = Communicator.world;
                commSize = comm.Size;
                commRank = comm.Rank;

                List<List<int>> dataSublists = new List<List<int>>();

                if (args.Length < 3)
                {
                    if (commRank == 0)
                        Console.WriteLine("Error! You have to enter input file path, separator and output file path!");
                    return -1;
                }
                else if ((commSize & (commSize - 1)) != 0)
                {
                    if (commRank == 0)
                        Console.WriteLine("Error! The number of processors must be a power of two!");
                    return -2;
                }
                else
                {
                    if (commRank == 0)
                    {
                        readPath = args[0];
                        separator = args[1];
                        writePath = args[2];
                    }
                }

                if (commRank == 0)
                {
                    var allData = ReadFile();

                    for (int i = 1; i < comm.Size; i++)
                        comm.Send(isFileReadable, i, 0);

                    if (!isFileReadable)
                        Console.WriteLine("Error! Can't read your file!");
                    else
                    {
                        if (allData.Count % commSize != 0)
                        {
                            while (allData.Count % commSize != 0)
                                allData.Add(int.MaxValue);
                        }

                        var subSize = allData.Count / commSize;
                        for (int i = 0; i < commSize; i++)
                        {
                            var subList = allData.GetRange(i * subSize, subSize);
                            subList.Sort();
                            dataSublists.Add(subList);
                        }
                        data = comm.Scatter(dataSublists.ToArray(), 0);
                    }
                }
                else
                {
                    if (!comm.Receive<bool>(0, 0))
                        return -3;
                    data = comm.Scatter<List<int>>(0);
                }

                data = Sort();

                if (commRank == 0)
                {
                    WriteToFile();
                    Console.WriteLine(!isFileWritable ? "Error! Incorrect writing path or another problem with output!" : "Your list was sorted!");
                }
            }
            return 0;
        }

        private static List<int> ReadFile()
        {
            List<string> tempData = null;
            try
            {
                using (var reader = new StreamReader(readPath))
                {
                    var tmp = reader.ReadToEnd();
                    tempData = tmp.Split(separator).ToList();
                }

                isFileReadable = true;
                return tempData.Select(x => Convert.ToInt32(x)).ToList();
            }
            catch
            {
                return new List<int>();
            }
        }

        private static List<int> Sort()
        {
            for (int i = 0; i < (int)Math.Log2(commSize); i++)
            {
                var groups = CreateGroups(i);

                var numberOfDissections = i == 0 ? 1 : (2 << i - 1);
                var countPairsInDimension = (commSize >> 1) / numberOfDissections;

                for (int j = 0; j < numberOfDissections; j++)
                {
                    // Choosing and sending pivot

                    var (fst, snd) = groups[j * countPairsInDimension];

                    var dimensionProcesses = new List<int>();

                    foreach (var (procF, procS) in groups.GetRange(j * countPairsInDimension + 1, countPairsInDimension - 1))
                    {
                        dimensionProcesses.Add(procF);
                        dimensionProcesses.Add(procS);
                    }

                    double pivot = 0;

                    if (commRank == fst)
                        comm.Send(data, snd, 2);
                    else if (commRank == snd)
                        pivot = data.Concat(comm.Receive<List<int>>(fst, 2)).Average();

                    if (commRank == snd)
                    {
                        comm.Send(pivot, fst, 3);
                        foreach (var proc in dimensionProcesses)
                            comm.Send(pivot, proc, 3);
                    }
                    else if (commRank == fst || dimensionProcesses.Contains(commRank))
                        pivot = comm.Receive<double>(snd, 3);

                    // Compare and split pairs of processes

                    foreach (var (procF, procS) in groups.GetRange(j * countPairsInDimension, countPairsInDimension))
                        CompareAndSplit(procF, procS, pivot);
                }
            }

            return comm.Gather(data, 0).SelectMany(x => x.Where(it => it != int.MaxValue)).ToList();
        }

        private static List<(int, int)> CreateGroups(int index)
        {
            var negIndex = (int)Math.Log2(commSize) - index - 1;
            var step = negIndex == 0 ? 1 : 2 << (negIndex - 1);

            var nums = Enumerable.Range(0, commSize).ToList();
            var groups = new List<(int, int)>();

            while (nums.Count != 0)
            {
                var secNum = nums[0] + step;
                groups.Add((nums[0], secNum));
                nums.Remove(nums[0]);
                nums.Remove(secNum);
            }

            return groups;
        }

        private static void CompareAndSplit(int rankF, int rankS, double pivot)
        {
            if (commRank == rankF)
            {
                comm.Send(data, rankS, 0);
                data = comm.Receive<List<int>>(rankS, 1);
            }
            else if (commRank == rankS)
            {
                var prevDataF = comm.Receive<List<int>>(rankF, 0);

                var mergedList = Merge(prevDataF, data);
                var splitIndex = mergedList.FindIndex(x => x > pivot);

                comm.Send(mergedList.GetRange(0, splitIndex), rankF, 1);
                data = mergedList.GetRange(splitIndex, mergedList.Count - splitIndex);
            }
        }

        private static List<int> Merge(List<int> fst, List<int> snd)
        {
            int i = 0, j = i;
            List<int> result = new List<int>();
            while (i < fst.Count && j < snd.Count)
                result.Add(fst[i] < snd[j] ? fst[i++] : snd[j++]);
            while (i < fst.Count)
                result.Add(fst[i++]);
            while (j < snd.Count)
                result.Add(snd[j++]);
            return result;
        }

        private static void WriteToFile()
        {
            try
            {
                using (var writer = new StreamWriter(writePath))
                {
                    writer.WriteLine(String.Join(" ", data));
                    isFileWritable = true;
                }
            }
            catch
            {
            }
        }
    }
}