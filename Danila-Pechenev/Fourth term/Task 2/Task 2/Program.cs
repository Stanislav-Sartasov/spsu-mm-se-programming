using MPI;

namespace Task2;

internal class Program
{
    private static int Main(string[] args)
    {
        using (var env = new MPI.Environment(ref args))
        {
            var communicator = Communicator.world;
            int communicatorSize = communicator.Size;
            int communicatorRank = communicator.Rank;

            if (args.Length < 2)
            {
                if (communicatorRank == 0)
                {
                    Console.WriteLine("Program expects paths to input and output files as input.");
                }

                return -1;
            }

            if (communicatorRank == 0)
            {
                string inputFile = args[0];
                string outputFile = args[1];

                var sr = new StreamReader(inputFile);
                int N = int.Parse(sr.ReadLine());
                for (int comm = 1; comm < communicatorSize; comm++)
                {
                    communicator.Send<int>(N, comm, 0);
                }

                var firstVertices = new List<int>[communicatorSize];
                var secondVertices = new List<int>[communicatorSize];
                var weights = new List<int>[communicatorSize];
                for (int i = 0; i < communicatorSize; i++)
                {
                    firstVertices[i] = new List<int>();
                    secondVertices[i] = new List<int>();
                    weights[i] = new List<int>();
                }

                string[] subs;
                int rank;
                int index = 0;
                while (!sr.EndOfStream)
                {
                    
                    subs = sr.ReadLine().Split(' ');
                    rank = index % communicatorSize;
                    firstVertices[rank].Add(int.Parse(subs[0]));
                    secondVertices[rank].Add(int.Parse(subs[1]));
                    weights[rank].Add(int.Parse(subs[2]));
                    index++;
                }

                sr.Close();

                for (int comm = 1; comm < communicatorSize; comm++)
                {
                    communicator.Send<int[]>(firstVertices[comm].ToArray(), comm, 0);
                    communicator.Send<int[]>(secondVertices[comm].ToArray(), comm, 0);
                    communicator.Send<int[]>(weights[comm].ToArray(), comm, 0);

                }

                var firstV0 = firstVertices[0].ToArray();
                var secondV0 = secondVertices[0].ToArray();
                var weight0 = weights[0].ToArray();

                int E = firstV0.Length;
                int result = 0;

                var visitedVertices = new bool[N];
                visitedVertices[0] = true;

                int firstVertex;
                int secondVertex;
                int weight;
                int receivedMinWeight;
                int receivedFoundVertexFrom;
                for (int i = 1; i < N; i++)
                {
                    int minWeight = 1000000000;
                    int foundVertex = -1;
                    for (int edgeIndex = 0; edgeIndex < E; edgeIndex++)
                    {
                        firstVertex = firstV0[edgeIndex];
                        secondVertex = secondV0[edgeIndex];
                        weight = weight0[edgeIndex];
                        if (visitedVertices[firstVertex] ^ visitedVertices[secondVertex])
                        {
                            if (weight < minWeight)
                            {
                                minWeight = weight;
                                foundVertex = visitedVertices[firstVertex] ? secondVertex : firstVertex;
                            }
                        }
                    }
                    
                    for (int comm = 1; comm < communicatorSize; comm++)
                    {
                        receivedMinWeight = communicator.Receive<int>(comm, 0);
                        receivedFoundVertexFrom = communicator.Receive<int>(comm, 0);

                        if (receivedMinWeight < minWeight)
                        {
                            minWeight = receivedMinWeight;
                            foundVertex = receivedFoundVertexFrom;
                        }
                    }

                    result += minWeight;
                    visitedVertices[foundVertex] = true;
                    for (int comm = 1; comm < communicatorSize; comm++)
                    {
                        communicator.Send<int>(foundVertex, comm, 0);
                    }
                }

                var sw = new StreamWriter(outputFile);
                sw.WriteLine(N);
                sw.WriteLine(result);
                sw.Close();
            }
            else
            {
                int N = communicator.Receive<int>(0, 0);
                var firstVertices = communicator.Receive<int[]>(0, 0);
                var secondVertices = communicator.Receive<int[]>(0, 0);
                var weights = communicator.Receive<int[]>(0, 0);

                int E = firstVertices.Length;
                var visitedVertices = new bool[N];
                visitedVertices[0] = true;

                int firstVertex;
                int secondVertex;
                int weight;
                for (int i = 1; i < N; i++)
                {
                    int minWeight = 1000000000;
                    int foundVertex = -1;
                    for (int edgeIndex = 0; edgeIndex < E; edgeIndex++)
                    {
                        firstVertex = firstVertices[edgeIndex];
                        secondVertex = secondVertices[edgeIndex];
                        weight = weights[edgeIndex];
                        if (visitedVertices[firstVertex] ^ visitedVertices[secondVertex])
                        {
                            if (weight < minWeight)
                            {
                                minWeight = weight;
                                foundVertex = visitedVertices[firstVertex] ? secondVertex : firstVertex;
                            }
                        }
                    }

                    communicator.Send<int>(minWeight, 0, 0);
                    communicator.Send<int>(foundVertex, 0, 0);

                    visitedVertices[communicator.Receive<int>(0, 0)] = true;
                }
            }

            return 0;
        }
    }
}
