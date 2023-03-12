using MPI;

namespace PrimsAlgorithm;

internal class Program
{
    private static void Main(string[] args)
    {
        using (new MPI.Environment(ref args))
        {
            var communicator = Communicator.world;
            var communicatorSize = communicator.Size;

            if (communicator.Rank == 0)
            {
                var edgesInString = new[]
                {
                    "0 1 10", "1 2 10", "2 3 10", "3 4 10", "4 5 10", "5 6 10", "6 7 10", "7 8 10", "8 9 10", "0 9 10", "1 3 6", "2 4 8"
                };
                const int verticesCount = 10;

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
                foreach (var edgeInString in edgesInString)
                {
                    var edgeInArray = edgeInString.Split(' ');
                    var vertexFrom = Convert.ToInt32(edgeInArray[0]);
                    var vertexTo = Convert.ToInt32(edgeInArray[1]);
                    var weight = Convert.ToInt32(edgeInArray[2]);

                    var edge = new Edge(vertexFrom, vertexTo, weight);
                    allEdges[rank].Add(edge);

                    rank = (rank + 1) % communicatorSize;
                }

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

                Console.WriteLine($"Result = {result} on 0");
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