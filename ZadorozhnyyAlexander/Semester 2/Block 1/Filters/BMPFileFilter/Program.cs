using System;
using System.Linq;
using System.IO;

namespace BMPFileFilter
{
    class Program
    {
		static void Main(string[] args)
		{
			Console.WriteLine("This programm modificate your bmp image with 6 availible filters:\n " +
				"1 -> SobelX\n2 -> SobelY\n3 -> SobelBoth\n4 -> Middle\n5 -> Gauss3x3\n6 -> Grey\n");

			if (args.Length != 3)
			{
				Console.WriteLine($"Wrong count of input parameters. You must enter only 3 parameters, but you enter: {args.Length}\n");
				return;
			}
			string[] ArrayFilters = { "SobelX", "SobelY", "SobelBoth", "Middle", "Gauss3x3", "Grey" };

			if (!ArrayFilters.Contains(args[1]))
			{
                Console.WriteLine("\nYou chose non-exist filter.\n");
				return;
			}

			FileStream openingFile;
			try
            {
                Console.WriteLine(args[0]);
				openingFile = new FileStream(args[0], FileMode.Open, FileAccess.ReadWrite);
			}
			catch
            {
                Console.WriteLine("Invalid file path specified");
				return;
            }

			FileStream SavingFile;
			try
			{
				SavingFile = new FileStream(args[2], FileMode.Create, FileAccess.ReadWrite);
			}
			catch
			{
				openingFile.Close();
				Console.WriteLine("Failed to open the output file.");
				return;
			}

			BitMapFile file = new(openingFile);
			openingFile.Close();

			if (args[1] == "SobelX")
				Filters.ApplySobelFilter(file, "X");
			else if (args[1] == "SobelY")
				Filters.ApplySobelFilter(file, "Y");
			else if (args[1] == "SobelBoth")
				Filters.ApplySobelFilter(file, "Both");
			else if (args[1] == "Middle")
				Filters.ApplyMiddleFilter(file);
			else if (args[1] == "Gauss3x3")
				Filters.ApplyGauss3x3Filter(file);
			else
				Filters.ApplyGreyFilter(file);

			Console.WriteLine("Your image was successfully filtered!");
			file.WriteNewFile(SavingFile);
			SavingFile.Close();
		}
    }
}
