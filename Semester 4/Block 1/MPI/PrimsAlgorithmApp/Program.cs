using MPI;

namespace PrimsAlgorithmApp;

public class Program
{
	public static void Main(string[] args)
	{
		using (new MPI.Environment(ref args))
		{
			var communicator = Communicator.world;

			if (communicator.Rank == 0)
			{
				Console.WriteLine("This program implements parallel Prim's algorithm and write result to file.");
			}

			if (args.Length != 2)
			{
				Console.WriteLine("Invalid number of arguments. Expected filenames for input and output.");
				return;
			}

			int verticesCount;
			List<Edge> edges;
			try
			{
				(verticesCount, edges) = GraphIO.ReadFile(args[0]);
			}
			catch (Exception e)
			{
				Console.WriteLine($"An error occurred while reading the file: {e}");
				return;
			}

			var treeWeight = MinimumSpanningTreeFinder.Find(verticesCount, edges);
			
			if (communicator.Rank == 0)
			{
				try
				{
					GraphIO.WriteToFile(args[1], verticesCount, treeWeight);
					Console.WriteLine("Done!");
				}
				catch (Exception e)
				{
					Console.WriteLine($"An error occurred while writing to file: {e}");
				}
			}
		}
	}
}
