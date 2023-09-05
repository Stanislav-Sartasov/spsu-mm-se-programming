﻿using MPI;

namespace PrimsAlgorithm;

internal class Program
{
    private static void Main(string[] args)
    {
        using (new MPI.Environment(ref args))
        {
            var communicator = Communicator.world;
            var communicatorSize = communicator.Size;

            if (args.Length != 2)
            {
                if (communicator.Rank == 0)
                {
                    Console.WriteLine("Error! Expected 2 arguments — paths to input and output files.");
                }
                return;
            }

            if (communicator.Rank == 0)
            {
                var inputFile = args[0];
                var outputFile = args[1];

                var reader = new StreamReader(inputFile);

                var verticesCount = Convert.ToInt32(reader.ReadLine());
                for (int i = 1; i < communicatorSize; i++)
                {
                    communicator.Send(verticesCount, i, 0);
                }

                // Item at position <i> of array is the list of edges for the communicator with rank <i>
                var allEdges = new List<Edge>[communicatorSize];
                for (int i = 0; i < communicatorSize; i++)
                {
                    allEdges[i] = new List<Edge>();
                }

                var rank = 0;
                while (!reader.EndOfStream)
                {
                    var edgeInArray = reader.ReadLine()!.Split(' ');
                    var vertexFrom = Convert.ToInt32(edgeInArray[0]);
                    var vertexTo = Convert.ToInt32(edgeInArray[1]);
                    var weight = Convert.ToInt32(edgeInArray[2]);

                    var edge = new Edge(vertexFrom, vertexTo, weight);
                    allEdges[rank].Add(edge);

                    rank = (rank + 1) % communicatorSize;
                }

                reader.Close();

                for (int i = 1; i < communicatorSize; i++)
                {
                    communicator.Send(allEdges[i], i, 0);
                }

                // Item at position <i> of array is 'true' if vertex with index <i> is already in tree, else 'false'
                var verticesInTree = new bool[verticesCount];
                verticesInTree[0] = true;

                var edges = allEdges[0];
                var result = 0;

                for (int step = 1; step < verticesCount; step++)
                {
                    var minWeightEdge = new Edge(0, 0, Int32.MaxValue);

                    foreach (var edge in edges)
                    {
                        // Condition that the edge connects vertices from different connectivity components
                        bool isBridge = verticesInTree[edge.VertexFrom] ^ verticesInTree[edge.VertexTo];

                        if (isBridge && edge.Weight < minWeightEdge.Weight)
                        {
                            minWeightEdge = edge;
                        }
                    }

                    for (int i = 1; i < communicatorSize; i++)
                    {
                        var receivedMinEdge = communicator.Receive<Edge>(i, 0);
                        if (receivedMinEdge.Weight < minWeightEdge.Weight)
                        {
                            minWeightEdge = receivedMinEdge;
                        }
                    }

                    result += minWeightEdge.Weight;
                    verticesInTree[minWeightEdge.VertexFrom] = true;
                    verticesInTree[minWeightEdge.VertexTo] = true;

                    for (int i = 1; i < communicatorSize; i++)
                    {
                        communicator.Send(minWeightEdge, i, 0);
                    }
                }

                var writer = new StreamWriter(outputFile);
                writer.WriteLine(verticesCount);
                writer.Write(result);
                writer.Close();
            }
            else
            {
                var verticesCount = communicator.Receive<int>(0, 0);
                var edges = communicator.Receive<List<Edge>>(0, 0);

                var verticesInTree = new bool[verticesCount];
                verticesInTree[0] = true;

                for (int step = 1; step < verticesCount; step++)
                {
                    var minWeightEdge = new Edge(0, 0, Int32.MaxValue);

                    foreach (var edge in edges)
                    {
                        bool isBridge = verticesInTree[edge.VertexFrom] ^ verticesInTree[edge.VertexTo];

                        if (isBridge && edge.Weight < minWeightEdge.Weight)
                        {
                            minWeightEdge = edge;
                        }
                    }

                    communicator.Send(minWeightEdge, 0, 0);

                    var newEdgeInTree = communicator.Receive<Edge>(0, 0);
                    verticesInTree[newEdgeInTree.VertexFrom] = true;
                    verticesInTree[newEdgeInTree.VertexTo] = true;
                }
            }
        }
    }
}