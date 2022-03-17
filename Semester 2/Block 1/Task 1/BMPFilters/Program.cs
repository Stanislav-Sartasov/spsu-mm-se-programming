using System;
using System.IO;

namespace BMPFilters
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("This program applies to input 24 or 32 bit BMP image one of median, gauss, sobelX, sobelY, grayscale filters.");

			if (args.Length != 3)
			{
				Console.WriteLine("Input error. Please, enter data in following order: program name, input file, filter name, output file.");
				return;
			}

			if (String.Compare(args[1], "median") != 0 && String.Compare(args[1], "gauss") != 0 && String.Compare(args[1], "sobelX") != 0 && String.Compare(args[1], "sobelY") != 0 && String.Compare(args[1], "grayscale") != 0)
			{
				Console.WriteLine("There is only median, gauss, sobelX, sobelY and grayscale filters.");
				return;
			}

			FileStream input;
			try
			{
				input = new FileStream(args[0], FileMode.Open, FileAccess.Read);
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("File not found.");
				return;
			}

			BMPImage image = new BMPImage(input);

			FileStream output;
			try
			{
				output = new FileStream(args[2], FileMode.OpenOrCreate, FileAccess.Write);
			}
			catch (Exception)
			{
				Console.WriteLine("Failed to create or to find output file.");
				input.Close();
				return;
			}

			if (args[1].Equals("median"))
			{
				Filters.ApplyMedian(image);
			}
			else if (args[1].Equals("gauss"))
			{
				Filters.ApplyGauss(image);
			}
			else if (args[1].Equals("sobelX"))
			{
				Filters.ApplySobelX(image);
			}
			else if (args[1].Equals("sobelY"))
			{
				Filters.ApplySobelY(image);
			}
			else if (args[1].Equals("grayscale"))
			{
				Filters.ApplyGrayscale(image);
			}

			image.WriteToFile(output);

			input.Close();
			output.Close();
		}
	}
}
