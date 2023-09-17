using MPI;

namespace QuickSort
{
    internal class Program
    {
        private static readonly int _root = 0;
        private static List<int> _originalArray = new();

        private static void SendExpectedUsage()
		{
            Console.WriteLine("2 arguments are expected: input and output paths");
		}

        private static List<int> ReadInputFile(string path)
		{
            return File.ReadAllText(path).Split(' ').Select(x => Int32.Parse(x)).ToList();
		}

        private static bool CheckArguments(string[] args)
		{
            if (args.Length != 2)
			{
                SendExpectedUsage();
                return false;
			}
            if (!File.Exists(args[0]))
			{
                Console.WriteLine($"Could not load file \"{args[0]}\"! Aborting...");
                return false;
			}
            
            return true;
		}

        private static int GetHighestPowerOf2(int n)
		{
            int res = 1;
            while (n >= res) res *= 2;
            return res / 2;
		}

        private static List<int>[] SplitArray(List<int> original, int parts, int resultSize)
		{
            var result = new List<int>[resultSize];
            for (int i = 0; i < resultSize; i++)
			{
                result[i] = new();
			}
            for (int i = 0; i < original.Count; i++)
			{
                result[i % parts].Add(original[i]);
			}
            return result;
		}

        private static Tuple<List<int>, List<int>> SplitByPivot(List<int> original, int pivot)
        {
            List<int> left = new();
            List<int> right = new();
            foreach (int i in original)
            {
                if (i <= pivot)
                    left.Add(i);
                else
                    right.Add(i);
            }
            return new Tuple<List<int>, List<int>>(left, right);
        }

        private static List<int> MergeSortedArrays(List<int> left, List<int> right)
		{
            List<int> result = new();
            var iL = 0;
            var iR = 0;
            while (iL < left.Count && iR < right.Count)
			{
                if (left[iL] < right[iR])
				{
                    result.Add(left[iL]);
                    iL++;
                    continue;
				}
                result.Add(right[iR]);
                iR++;
			}
            for (; iL < left.Count; iL++)
			{
                result.Add(left[iL]);
			}
            for (; iR < right.Count; iR++)
			{
                result.Add(right[iR]);
			}
            return result;
		}

        static int Main(string[] args)
        {
            using (var environment = new MPI.Environment(ref args))
            {
                var comm = Communicator.world;
                var totalProcesses = comm.Size;
                var rank = comm.Rank;
                bool isRoot = rank == _root;

                int activeProcesses = GetHighestPowerOf2(totalProcesses);
                bool isActive = rank < activeProcesses;
                List<int> localArray = new();
                List<int> originalArray = new();

                if (isRoot)
				{
                    if (!CheckArguments(args))
                        return -1;
                    originalArray = ReadInputFile(args[0]);
                    Console.WriteLine($"Number of active processes: {activeProcesses}");
                    if (activeProcesses != totalProcesses)
					{
                        Console.WriteLine($"Note: number of processes is not a power of 2;");
                        Console.WriteLine($"      {totalProcesses - activeProcesses} processes will idle.");
					}
                    var splittedArray = SplitArray(originalArray, activeProcesses, totalProcesses);
                    localArray = comm.Scatter(splittedArray, _root);
				}
                else
				{
                    localArray = comm.Scatter<List<int>>(_root);
				}

                localArray.Sort();

                for (int phase = activeProcesses; phase >= 2; phase /= 2)
                {
                    if (!isActive) break;

                    // each phase has twice as many distinct groups from the previous one,
                    // with their own pivot points

                    // this variable decides group's root that will share pivot among others
                    var groupRoot = rank & ~(phase - 1);
                    bool isGroupRoot = groupRoot == rank;

                    int pivot;
                    if (isGroupRoot)
                    {
                        pivot = localArray[localArray.Count / 2];
                        for (int i = rank + 1; i < rank + phase; i++)
                        {
                            comm.Send(pivot, i, 0);
                        }
                    }
                    else
                    {
                        pivot = comm.Receive<int>(groupRoot, 0);
                    }

                    // this decides process pairings
                    var pairWith = rank ^ (phase / 2);
                    bool isPairRoot = rank < pairWith;

                    var (left, right) = SplitByPivot(localArray, pivot);

                    if (isPairRoot)
					{
                        var pairedList = comm.Receive<List<int>>(pairWith, 0);
                        comm.Send(right, pairWith, 0);
                        localArray = MergeSortedArrays(left, pairedList);
					}
                    else
					{
                        comm.Send(left, pairWith, 0);
                        var pairedList = comm.Receive<List<int>>(pairWith, 0);
                        localArray = MergeSortedArrays(right, pairedList);
					}
				}

                var sortedArrays = comm.Gather(localArray, _root);
                if (isRoot)
				{
                    originalArray = sortedArrays.SelectMany(x => x).ToList();
                    using (var sw = new StreamWriter(args[1]))
					{
                        sw.WriteLine(String.Join(' ', originalArray));
					}
                    Console.WriteLine($"Sorted array was saved at \"{args[1]}\"!");
				}
            }

            return 0;
        }
    }
}