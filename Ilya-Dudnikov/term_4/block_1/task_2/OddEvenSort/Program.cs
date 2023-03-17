using MPI;
using Environment = MPI.Environment;

namespace OddEvenSort;

public class Program
{
    private static void Help()
    {
        Console.WriteLine("Usage: mpiexec -n <number of communicators> <path to executable> <path to input file> <path to output file");
    }

    public static void Main(string[] args)
    {
        using (var env = new Environment(ref args))
        {
            int[]? inputArray = null;
            var comm = Communicator.world;

            if (comm.Rank == 0)
            {
                if (args.Length < 2)
                {
                    Help();
                    return;
                }

                var inputFile = args[0];
                var streamReader = new StreamReader(inputFile);
                inputArray = streamReader.ReadToEnd().Split().Select(x => int.Parse(x)).ToArray();
                streamReader.Close();

                inputArray = comm.Scatter(Enumerable.Repeat(inputArray, comm.Size).ToArray(), 0);
            }
            else
            {
                inputArray = comm.Scatter<int[]>(0);
            }

            if (inputArray == null)
            {
                Console.WriteLine("Failed to read the input file");
                Help();
                return;
            }

            var sorted = Sort.OddEvenSort(inputArray);

            if (Communicator.world.Rank == 0)
            {
                Console.WriteLine($"{string.Join(" ", sorted)}");
                var outputFile = args[1];
                var streamWriter = new StreamWriter(outputFile);
                foreach (var elem in sorted)
                {
                    streamWriter.Write($"{elem} ");
                }
                streamWriter.Write(System.Environment.NewLine);
                streamWriter.Close();
            }
        }
    }
}