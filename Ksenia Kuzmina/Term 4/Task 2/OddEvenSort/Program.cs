using MPI;
using System.Text;

namespace OddEvenSort
{
	public class Program
	{
		public static int Main(string[] args)
		{
            if (args.Length < 2)
            {
                Console.WriteLine("Pass as arguments the file to sort and the file to save the result");
                return -1;
            }

            using (MPI.Environment env = new MPI.Environment(ref args))
			{
				List<int> arr = ReadFromFile(args[0]);
                List<int> sortedArray = new List<int>();

				Intracommunicator communicator = Communicator.world;
				sortedArray = Sort(communicator, ref arr);

				WriteToFile(sortedArray, args[1]);
				return 0;
			}
		}

		private static List<int> ReadFromFile(string filename)
		{
			List<int> arr = new();

			try
			{
				arr = arr.Concat(File.ReadAllText(filename).Split(' ').Select(x => int.Parse(x))).ToList();
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
			}

			return arr;
		}

		private static List<int> Sort(Intracommunicator comm, ref List<int> arr)
		{
			List<int> scattered;

			if (comm.Rank == 0)
			{
				scattered = comm.Scatter(Split(arr, comm.Size).ToArray(), 0);
			}
			else
			{
				scattered = comm.Scatter<List<int>>(0);
			}

			scattered.Sort();

			List<int> received;
			List<int> res;

			for (int i = 0; i < comm.Size; i++)
			{
				if ((i + comm.Rank) % 2 == 0)
				{
					if (comm.Rank + 1 < comm.Size)
					{
						comm.Send(scattered, comm.Rank + 1, 0);
						scattered = comm.Receive<List<int>>(comm.Rank + 1, 1);
					}
				}
				else
				{
					if (comm.Rank - 1 >= 0)
					{
						received = comm.Receive<List<int>>(comm.Rank - 1, 0);

						res = Merge(scattered, received);

						comm.Send(res, comm.Rank - 1, 1);
					}
				}
			}

			List<int>[] gathered = comm.Gather(scattered, 0);
			List<int> result = new List<int>();

			if (comm.Rank == 0)
			{
				for (int i = 0; i < comm.Size; i++)
				{
					result = result.Concat(gathered[i]).ToList();
				}
			}

			return result;
		}

		private static List<int> Merge(List<int> fst, List<int> scd)
		{
			List<int> res = new();

			res = fst.Concat(scd).OrderBy(x => x).ToList();

			fst.Clear();
			scd.Clear();

			return res;
		}

		private static List<T>[] Split<T>(List<T> list, int parts)
		{
			List<T>[] result = new List<T>[parts];
			int step = list.Count / parts;

			for (int i = 0; i < parts; i++)
			{
				result[i] = list.GetRange(step * i, step);
				if (i == parts - 1)
				{
					result[i] = list.GetRange(step * i, list.Count - step * i);
				}
			}

			return result;
		}

		public static void WriteToFile(List<int> arrayOfIntNumbers, string filename)
		{
			try
			{
				var stringBuilder = new StringBuilder();

				foreach (var arrayElement in arrayOfIntNumbers)
				{
					stringBuilder.AppendLine(arrayElement.ToString());
				}

				File.WriteAllText(filename, stringBuilder.ToString());
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
			}
		}
	}
}