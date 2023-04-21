namespace ImageFilters
{
	public class Program
	{
		private static int IncorrectArgumentsMessage()
		{
			Console.WriteLine("Error: argument mismatch; please use the following pattern:");
			Console.WriteLine("    <input file> <filter name> <output file>");
			return 1;
		}

		private static void GreetingsMessage()
		{
			Console.WriteLine("This program applies named filter to the given image.");
		}

		private static void FarewellMessage()
		{
			Console.WriteLine("The filter has been successfully applied!");
		}

		private static int ErrorMessage(int errorCode, string error)
		{
			Console.WriteLine($"Error: {error}");
			return errorCode;
		}

		public static int Main(string[] args)
		{
			GreetingsMessage();
			if (args.Length != 3)
				return IncorrectArgumentsMessage();

			if (!File.Exists(args[0]))
				return ErrorMessage(2, $"file {args[0]} does not exist");

			if (!Filters.CheckIfImplemented(args[1]))
				return ErrorMessage(3, $"filter {args[1]} is not supported or not yet implemented");

			Bitmap image = new();
			using (FileStream fin = File.OpenRead(args[0]))
			{
				if (image.ReadBitmap(fin) != 0)
					return ErrorMessage(-1, "image is either corrupted or not supported");
			}

			if (Filters.ApplyFilterByName(args[1], image) != 0)
				return ErrorMessage(1, "filter could not be applied");

			using (FileStream fout = File.Create(args[2]))
			{
				if (image.WriteBitmap(fout) != 0)
					return ErrorMessage(2, $"could not write processed image to {args[2]}");
			}

			FarewellMessage();
			return 0;
		}
	}
}