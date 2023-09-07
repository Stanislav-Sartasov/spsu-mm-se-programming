using MPI;

namespace PrimsAlgorithmApp
{
	public static class MinimumSpanningTreeFinder
	{
		public static int Find(int verticesCount, List<Edge> edges)
		{
			var communicator = Communicator.world;

			var rankEdges = GetEdgesSubset(edges);

			var treeWeight = 0;
			if (communicator.Rank == 0)
			{
				var verticesInTree = new bool[verticesCount];
				verticesInTree[0] = true;
				for (var step = 1; step < verticesCount; step++)
				{
					var minWeightEdge = new Edge(0, 0, Int32.MaxValue);

					foreach (var edge in rankEdges)
					{
						var isBridge = verticesInTree[edge.VertexFrom] ^ verticesInTree[edge.VertexTo];

						if (isBridge && edge.Weight < minWeightEdge.Weight)
						{
							minWeightEdge = edge;
						}
					}

					for (var i = 1; i < communicator.Size; i++)
					{
						var receivedMinEdge = communicator.Receive<Edge>(i, 0);
						if (receivedMinEdge.Weight < minWeightEdge.Weight)
						{
							minWeightEdge = receivedMinEdge;
						}
					}

					treeWeight += minWeightEdge.Weight;
					verticesInTree[minWeightEdge.VertexFrom] = true;
					verticesInTree[minWeightEdge.VertexTo] = true;

					for (var i = 1; i < communicator.Size; i++)
					{
						communicator.Send(minWeightEdge, i, 0);
					}
				}

				return treeWeight;
			}
			else
			{
				var verticesInTree = new bool[verticesCount];
				verticesInTree[0] = true;
				for (var step = 1; step < verticesCount; step++)
				{
					var minWeightEdge = new Edge(0, 0, Int32.MaxValue);

					foreach (var edge in rankEdges)
					{
						var isBridge = verticesInTree[edge.VertexFrom] ^ verticesInTree[edge.VertexTo];

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

				return treeWeight;
			}
		}

		private static List<Edge> GetEdgesSubset(List<Edge> allEdges)
		{
			var communicator = Communicator.world;

			if (communicator.Rank == 0)
			{
				var edgesSubsets = new List<Edge>[communicator.Size];
				for (int i = 0; i < edgesSubsets.Length; i++)
				{
					edgesSubsets[i] = new List<Edge>();
				}

				var rank = 0;
				foreach (Edge edge in allEdges)
				{
					edgesSubsets[rank].Add(edge);
					rank = (rank + 1) % communicator.Size;
				}

				for (int i = 1; i < communicator.Size; i++)
				{
					communicator.Send(edgesSubsets[i], i, 0);
				}

				return edgesSubsets[0];
			}
			else
			{
				return communicator.Receive<List<Edge>>(0, 0);
			}
		}
	}
}
