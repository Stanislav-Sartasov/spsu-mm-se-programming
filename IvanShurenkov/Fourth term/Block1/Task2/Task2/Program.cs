using MPI;
using Graph;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (new MPI.Environment(ref args))
            {
                var communicator = Communicator.world;
                int communicatorId = communicator.Rank;
                int cntCommunicator = communicator.Size;
                List<Edge> edges;
                List<int>[] graph;
                int cntVertex;
                if (args.Length != 2)
                {
                    if (0 == communicatorId)
                    {
                        Console.WriteLine("Need 2 args");
                    }
                    return;
                }

                List<Edge>[] edgesByRank;
                if (0 == communicatorId)
                {
                    // read
                    StreamReader input = new StreamReader(args[0]);
                    cntVertex = Convert.ToInt32(input.ReadLine());
                    // send
                    for (int i = 1; i < cntCommunicator; i++)
                    {
                        communicator.Send(cntVertex, i, 0);
                    }
                    // read
                    edgesByRank = new List<Edge>[cntCommunicator];

                    graph = new List<int>[cntVertex];
                    for (int j = 0; j < cntVertex; j++)
                    {
                        graph[j] = new List<int>();
                    }
                    for (int i = 0; i < cntCommunicator; i++)
                    {
                        edgesByRank[i] = new List<Edge>();
                    }
                    int id = 0;
                    while (!input.EndOfStream)
                    {
                        var edge = input.ReadLine()!.Split(' ');
                        int src = Convert.ToInt32(edge[0]);
                        int dst = Convert.ToInt32(edge[1]);
                        int w = Convert.ToInt32(edge[2]);
                        if (0 == id % cntCommunicator)
                        {
                            graph[src].Add(id);
                            graph[dst].Add(id);
                        }
                        edgesByRank[id % cntCommunicator].Add(new Edge(src, dst, w, id));
                        id++;
                    }
                    input.Close();

                    edges = edgesByRank[0];

                    // send
                    for (int i = 1; i < cntCommunicator; i++)
                    {
                        communicator.Send<List<Edge>>(edgesByRank[i], i, 0);
                    }
                }
                else
                {
                    // receive
                    cntVertex = communicator.Receive<int>(0, 0);
                    graph = new List<int>[cntVertex];
                    for (int j = 0; j < cntVertex; j++)
                    {
                        graph[j] = new List<int>();
                    }
                    edgesByRank = new List<Edge>[cntCommunicator];

                    edges = communicator.Receive<List<Edge>>(0, 0);

                    for (int i = 0; i < edges.Count; i++)
                    {
                        graph[edges[i].Src].Add(edges[i].Id);
                        graph[edges[i].Dest].Add(edges[i].Id);
                    }
                }

                int sum = 0;
                int cnt = 0;
                bool[] used = new bool[cntVertex];
                used[0] = true;
                PriorityQueue<int, int> queue = new PriorityQueue<int, int>();
                for (int i = 0; i < graph[0].Count; i++)
                {
                    queue.Enqueue(graph[0][i], edges[graph[0][i] / cntCommunicator].Weight);
                }

                while (cnt < cntVertex - 1)
                {
                    Edge minEdge = new Edge(-1, -1, Int32.MaxValue, -1);
                    while (queue.Count > 0 && queue.TryDequeue(out int edgeId, out int weight))
                    {
                        Edge edge = edges[edgeId / cntCommunicator];
                        if (used[edge.Src] ^ used[edge.Dest])
                        {
                            minEdge = edge;
                            break;
                        }
                    }
                    Edge minREdge = minEdge;

                    // send, receive
                    if (0 == communicatorId)
                    {
                        minREdge = minEdge;
                        for (int i = 1; i < cntCommunicator; i++)
                        {
                            int receiveEdgeId = communicator.Receive<int>(i, 0);
                            if (-1 == receiveEdgeId)
                            {
                                continue;
                            }
                            Edge receiveEdge = edgesByRank[receiveEdgeId % cntCommunicator][receiveEdgeId / cntCommunicator];
                            if (receiveEdge.Weight < minREdge.Weight)
                            {
                                minREdge = receiveEdge;
                            }
                        }
                        for (int i = 1; i < cntCommunicator; i++)
                        {
                            communicator.Send<Edge>(minREdge, i, 0);
                        }
                    }
                    else
                    {
                        communicator.Send<int>(minEdge.Id, 0, 0);
                        minREdge = communicator.Receive<Edge>(0, 0);
                    }
                    if (minREdge.Id != minEdge.Id && -1 != minEdge.Id)
                    {
                        queue.Enqueue(minEdge.Id, minEdge.Weight);
                    }
                    minEdge = minREdge;
                    if (-1 == minEdge.Id)
                    {
                        break;
                    }

                    int vertex = 0;
                    if (used[minEdge.Src])
                    {
                        used[minEdge.Dest] = true;
                        vertex = minEdge.Dest;
                    }
                    else
                    {
                        used[minEdge.Src] = true;
                        vertex = minEdge.Src;
                    }
                    for (int i = 0; i < graph[vertex].Count; i++)
                    {
                        Edge edge = edges[graph[vertex][i] / cntCommunicator];
                        if (used[edge.Src] ^ used[edge.Dest])
                        {
                            queue.Enqueue(graph[vertex][i], edge.Weight);
                        }
                    }
                    sum += minEdge.Weight;
                    cnt++;
                }
                if (0 == communicatorId)
                {
                    StreamWriter output = new StreamWriter(args[1]);
                    output.WriteLine(cnt + 1);
                    output.WriteLine(sum);
                    output.Close();

                    //Console.WriteLine(cnt + 1);
                    //Console.WriteLine(sum);
                }
            }
        }
    }
}