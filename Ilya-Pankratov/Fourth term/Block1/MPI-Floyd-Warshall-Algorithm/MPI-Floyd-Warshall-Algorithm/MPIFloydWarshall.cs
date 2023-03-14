using MPI;

namespace MPIFloydWarshallAlgorithm;

public class MPIFloydWarshall
{
    public static void Main(string[] args)
    {
        using (var env = new MPI.Environment(ref args))
        {
            var comm = Communicator.world;
            var isMaster = comm.Rank == 0;
            int vertexNumber = 0;

            if (isMaster)
            {
                Console.WriteLine("The program searches for shortest paths using Floyd " +
                    "Warshall algorithm in an undirected graph.\nAs input the program expects paths " +
                    "to two files: the path to the initial data of the graph and the path to write " +
                    "the result.");

                int[] adjencyMatrix;

                if (args.Length != 2)
                {
                    Console.WriteLine("Arguments: The algorithm expects two arguments");
                    AbortProcesses(comm);
                    return;
                }

                try
                {
                    adjencyMatrix = GraphFileManager.ReadGraph(args[0], out vertexNumber);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"ReadGraph: {e}");
                    AbortProcesses(comm);
                    return;
                }

                // Send number of vertex
                comm.Broadcast<int>(ref vertexNumber, 0);

                // Calculate number of lines to every process
                var counts = new int[comm.Size];
                var start = new int[comm.Size];
                var rows = vertexNumber / (comm.Size - 1);
                var unmanagedRows = vertexNumber % (comm.Size - 1);

                for (int i = 1; i < comm.Size; i++)
                {
                    if (rows != 0)
                    {
                        counts[i] = i - 1 < unmanagedRows ? rows + 1 : rows;
                    }
                    else // If there are more processes than lines
                    {
                        counts[i] = i - 1 < unmanagedRows ? 1 : 0;
                    }

                    start[i] = start[i - 1] + counts[i - 1];
                }

                // Sending number of lines
                var scatteredLines = comm.Scatter(counts, 0);
                var scatteredStart = comm.Scatter(start, 0);

                for (int k = 0; k < vertexNumber; k++)
                {
                    // Send k-line to processes
                    var kline = new ArraySegment<int>(adjencyMatrix, k * vertexNumber, vertexNumber).ToArray();
                    comm.Broadcast<int[]>(ref kline, 0);

                    // Send process' lines
                    var linesSent = 0;
                    for (int i = 1; i < comm.Size; i++)
                    {
                        var toSend = new ArraySegment<int>(adjencyMatrix, linesSent * vertexNumber, vertexNumber * counts[i]).ToArray();
                        comm.Send<int[]>(toSend, i, 0);
                        linesSent += counts[i];
                    }

                    // Get data from other processes
                    var gatheredData = comm.Gather<int[]>(null, 0).Skip(1);
                    adjencyMatrix = gatheredData.SelectMany(x => x).ToArray();
                }

                // Writing result to file
                try
                {
                    GraphFileManager.WriteMatrix(args[1], adjencyMatrix, vertexNumber);
                    Console.WriteLine($"The result of the Floyd-Worshall algorithm was successfully written to the {args[1]}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"WriteResultToFile: {e}");
                }
            }
            else
            {
                // Get initial information
                comm.Broadcast<int>(ref vertexNumber, 0);

                // Get number of liness
                var linesNumber = comm.Scatter<int>(0);
                var startLine = comm.Scatter<int>(0);

                // Main part of the algorithm
                for (int k = 0; k < vertexNumber; k++)
                {
                    // Getting k line
                    int[] kline = null;
                    comm.Broadcast<int[]>(ref kline, 0);

                    // Getting process' lines
                    var lines = comm.Receive<int[]>(0, 0);

                    for (int i = 0; i < linesNumber; i++)
                    {
                        for (int j = 0; j < vertexNumber; j++)
                        {
                            lines[i * vertexNumber + j] = PositiveMin(lines[i * vertexNumber + j], PositiveSum(kline[startLine + i], kline[j]));
                        }
                    }

                    // Send processed data
                    var gatheredValues = comm.Gather<int[]>(lines, 0);
                }
            }
        }
    }

    private static int PositiveMin(int x, int y)
    {
        if (x < 0)
        {
            if (y < 0)
            {
                return x;
            }
            else
            {
                return y;
            }
        }
        else if (y < 0)
        {
            return x;
        }
        else
        {
            return int.Min(x, y);
        }
    }

    private static int PositiveSum(int x, int y)
    {
        if (x < 0)
        {
            return x;
        }
        else if (y < 0)
        {
            return y;
        }
        else
        {
            return x + y;
        }
    }

    private static void AbortProcesses(Communicator comm)
    {
        for (int i = 0; i < comm.Size; i++)
        {
            if (i != comm.Rank)
            {
                comm.Abort(i);
            }
        }
    }
}
