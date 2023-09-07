namespace PrimsAlgorithmApp
{
	[Serializable]
	public struct Edge
	{
		public int VertexFrom { get; }
		public int VertexTo { get; }
		public int Weight { get; }

		public Edge(int vertexFrom, int vertexTo, int weight)
		{
			VertexFrom = vertexFrom;
			VertexTo = vertexTo;
			Weight = weight;
		}
	}
}
