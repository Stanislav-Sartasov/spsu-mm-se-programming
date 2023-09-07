using MPI;

namespace PrimsAlgorithmApp
{
	public static class GraphIO
	{
		public static (int, List<Edge>) ReadFile(string path)
		{
			var communicator = Communicator.world;

			if (communicator.Rank == 0)
			{
				var reader = new StreamReader(path);

				var verticesCount = Convert.ToInt32(reader.ReadLine());

				var edges = new List<Edge>();
				while (!reader.EndOfStream)
				{
					var edgeInArray = reader.ReadLine()!.Split(' ');
					var vertexFrom = Convert.ToInt32(edgeInArray[0]);
					var vertexTo = Convert.ToInt32(edgeInArray[1]);
					var weight = Convert.ToInt32(edgeInArray[2]);

					var edge = new Edge(vertexFrom, vertexTo, weight);
					edges.Add(edge);
				}

				reader.Close();

				return (verticesCount, edges);
			}
			else
			{
				var reader = new StreamReader(path);
				var verticesCount = Convert.ToInt32(reader.ReadLine());
				reader.Close();

				return (verticesCount, new List<Edge>());
			}
			
		}

		public static void WriteToFile(string path, int verticesCount, int treeWeight)
		{
			var writer = new StreamWriter(path);

			writer.WriteLine(verticesCount);
			writer.Write(treeWeight);
			
			writer.Close();
		}
	}
}
