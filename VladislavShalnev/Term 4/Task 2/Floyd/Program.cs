using System;
using MPI;

namespace Floyd;

public class Program
{
	public static void Main(string[] args)
	{
		using var env = new MPI.Environment(ref args);

		var comm = Communicator.world;
		int workersNumber = comm.Size - 1;
		int rank = comm.Rank;

		int primaryRank = 0;

		if (rank == primaryRank)
		{
			Console.WriteLine("This program implements Floyd's algorithm for an undirected graph.");

			if (args.Length != 2)
			{
				Console.WriteLine("Invalid number of arguments. Expected filenames for input and output.");
				return;
			}

			int vertNumber;
			int[] matrix;

			try
			{
				(vertNumber, matrix) = ReadFromFile(args[0]);
			}
			catch (Exception e)
			{
				Console.WriteLine($"An error occurred while reading the graph: {e}");
				return;
			}

			var rowCounts = new int[workersNumber];
			int remnant = vertNumber % workersNumber;

			for (int i = 0; i < workersNumber; i++)
			{
				int dest = i + 1;
				comm.Send(vertNumber, dest, 0);

				// Count the number of rows for the worker
				rowCounts[i] = vertNumber / workersNumber + (i < remnant ? 1 : 0);

				comm.Send(rowCounts[i], dest, 1);
			}

			for (int k = 0; k < vertNumber; k++)
			{
				var kRow = new ArraySegment<int>(matrix, k * vertNumber, vertNumber);

				int accumulator = 0;
				for (int i = 0; i < workersNumber; i++)
				{
					int dest = i + 1;
					comm.Send(kRow, dest, 2);

					// Slice rows for the worker
					var rows = new ArraySegment<int>(matrix, accumulator * vertNumber, vertNumber * rowCounts[i]);
					accumulator += rowCounts[i];

					comm.Send(rows, dest, 3);
				}

				var gathered = comm.Gather<ArraySegment<int>?>(null, primaryRank);

				var result = gathered
					.Skip(1)
					.Aggregate(new List<int>(), (acc, rows) => acc.Concat(rows).ToList());

				matrix = result.ToArray();
			}

			try
			{
				WriteToFile(args[1], vertNumber, matrix);
				Console.WriteLine("Done!");
			}
			catch (Exception e)
			{
				Console.WriteLine($"An error occurred while writing to the output file: {e}");
			}
		}
		else
		{
			int vertNumber = comm.Receive<int>(primaryRank, 0);
			int rowCount = comm.Receive<int>(primaryRank, 1);

			for (int k = 0; k < vertNumber; k++)
			{
				var kRow = comm.Receive<ArraySegment<int>>(primaryRank, 2);
				var rows = comm.Receive<ArraySegment<int>>(primaryRank, 3);

				for (int i = 0; i < rowCount; i++)
				{
					for (int j = 0; j < vertNumber; j++)
					{
						int[] weights = {rows[i * vertNumber + k], kRow[j]};

						// Skip if path does not exist
						if (weights.Any(weight => weight < 0))
							continue;

						int sum = rows[i * vertNumber + k] + kRow[j];

						if (rows[i * vertNumber + j] < 0)
						{
							rows[i * vertNumber + j] = sum;
						}
						else
						{
							rows[i * vertNumber + j] =
								Math.Min(rows[i * vertNumber + j], sum);
						}
					}
				}

				comm.Gather(rows, primaryRank);
			}
		}
	}

	private static (int, int[]) ReadFromFile(string path)
	{
		using var reader = new StreamReader(path);

		int vertNumber = Int32.Parse(reader.ReadLine());

		var matrix = new int[vertNumber * vertNumber];

		for (int i = 0; i < vertNumber * vertNumber; i++)
			matrix[i] = i == (i / vertNumber) * (vertNumber + 1) ? 0 : -1;

		string? line;
		while ((line = reader.ReadLine()) != null)
		{
			string[] data = line.Split();
			int first = Int32.Parse(data[0]);
			int second = Int32.Parse(data[1]);
			int weight = Int32.Parse(data[2]);

			matrix[first * vertNumber + second] = weight;

			// Symmetrize the adjacency matrix
			matrix[second * vertNumber + first] = weight;
		}

		return (vertNumber, matrix);
	}


	private static void WriteToFile(string path, int vertNumber, int[] matrix)
	{
		using var writer = new StreamWriter(path);

		for (int i = 0; i < vertNumber; i++)
		{
			var slice = new ArraySegment<int>(matrix, i * vertNumber, vertNumber);
			writer.WriteLine(string.Join(" ", slice));
		}
	}
}