using MPI;
using System.Text;

namespace OddEvenSort;

public class Program
{
	public static void Main(string[] args)
	{
		using var _ = new MPI.Environment(ref args);
		var comm = Communicator.world;
		var fileIn = "";
		var fileOut = "";

		if (comm.Rank == 0)
		{
			Console.Write("Input file: ");
			fileIn = Console.ReadLine();

			Console.Write("Output file: ");
			fileOut = Console.ReadLine();
		}

		comm.Barrier();

		var data = new List<int>();
		if (!CollectData(comm, data, fileIn)) return;

		OddEvenSort(comm, ref data);

		if (comm.Rank != 0) return;
		var res = string.Join(" ", data.Select(x => x.ToString()));
		WriteToFile(fileOut, res);
	}

	private static void OddEvenSort(Intracommunicator comm, ref List<int> target)
	{
		// Scatter between processes
		List<int> scattered;
		if (comm.Rank == 0)
		{
			var dataArr = new List<int>[comm.Size];
			for (var i = 0; i < comm.Size; i++)
			{
				var t = i == comm.Size - 1 ? int.MaxValue : target.Count / comm.Size;
				dataArr[i] = target.Skip(i * (target.Count / comm.Size)).Take(t).ToList();
			}

			scattered = comm.Scatter(dataArr, 0);
		}
		else
		{
			scattered = comm.Scatter<List<int>>(0);
		}

		// Perform odd-even sort
		for (var sortIteration = 0; sortIteration < comm.Size; sortIteration++)
		{
			if (comm.Rank % 2 == sortIteration % 2)
			{
				if (comm.Rank + 1 >= comm.Size) continue;

				comm.Send(scattered, comm.Rank + 1, 1);
				scattered = comm.Receive<List<int>>(comm.Rank + 1, 2);
			}
			else
			{
				if (comm.Rank - 1 < 0) continue;

				var received = comm.Receive<List<int>>(comm.Rank - 1, 1);
				scattered.AddRange(received);
				scattered.Sort();
				comm.Send(scattered.Take(received.Count).ToList(), comm.Rank - 1, 2);
				scattered = scattered.Skip(received.Count).ToList();
			}
		}

		// Gather results
		var scatteredArray = comm.Gather(scattered, 0);
		if (comm.Rank != 0) return;
		target.Clear();
		Array.ForEach(scatteredArray, target.AddRange);
	}

	private static IEnumerable<int> ReadFile(string name)
	{
		var res = File.ReadAllText(name);
		return res.Split(" ").Select(int.Parse);
	}

	private static void WriteToFile(string name, string res)
	{
		using var sw = new StreamWriter(name, false, Encoding.UTF8, 65536);
		sw.Write(res);
	}

	// Reads file and returns true if operation was successful, otherwise returns false and prints error to stdout
	private static bool CollectData(Intracommunicator comm, List<int> data, string fileIn)
	{
		var barr = false;

		if (comm.Rank == 0)
		{
			try
			{
				data.AddRange(ReadFile(fileIn));
				barr = true;
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("File \"{0}\" does not exist", fileIn);
			}
			catch (FormatException)
			{
				Console.WriteLine("File does not contain integers separated by whitespace");
			}
			catch (OverflowException)
			{
				Console.WriteLine("File contains a number too big");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception occurred: {0}", ex.Message);
			}
			finally
			{
				barr = comm.Scatter(Enumerable.Repeat(barr, comm.Size).ToArray(), 0);
			}
		}
		else
		{
			barr = comm.Scatter<bool>(0);
		}

		return barr;
	}
}