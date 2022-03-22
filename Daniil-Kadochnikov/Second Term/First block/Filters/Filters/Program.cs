using System;

namespace Filters
{
	class Program
	{
		static int Main(string[] Args)
		{
			Console.WriteLine("\nThe program gets a 24 or 32 bit BMP-file and applies one of the following filters:\n-Median filter 3x3 - median;\n-Gaussian filter 3x3 - gauss3;\n-Gaussian filter 5x5 - gauss5;\n-Sobel filter on X - sobelX;\n-Sobel filter on Y - sobelY;\n-grey filter - grey.\n\n");
			
			if (Args.Length != 3)
			{
				Console.WriteLine("Inappropriate amount of arguments.\nAppropriate from: <program> <input file name> <filter name> <output file name>\n");
				return -1;
			}

			Image Image = new Image();
			Image.BitMapping(Args[0]);

			if (Args[1] == "grey")
			{
				new GreyFilter().ApplyingGreyFilter(Image);
				Image.SavingFile(Args[2]);
				return 0;
			}
			else if (Args[1] == "median")
			{
				new MedianFilter().ApplyingMedianFilter(Image, 3);
				Image.SavingFile(Args[2]);
				return 0;
			}
			else if (Args[1] == "gauss3")
			{
				Gauss3Filter Filter = new Gauss3Filter();
				Filter.MatrixFormalization(Image, 3, Filter.Kernel, Filter.Divider);
				Image.SavingFile(Args[2]);
				return 0;
			}
			else if (Args[1] == "gauss5")
			{
				Gauss5Filter Filter = new Gauss5Filter();
				Filter.MatrixFormalization(Image, 5, Filter.Kernel, Filter.Divider);
				Image.SavingFile(Args[2]);
				return 0;
			}
			else if (Args[1] == "sobelX")
			{
				new GreyFilter().ApplyingGreyFilter(Image);
				SobelXFilter Filter = new SobelXFilter();
				Filter.MatrixFormalization(Image, 3, Filter.Kernel, Filter.Divider);
				Image.SavingFile(Args[2]);
				return 0;
			}
			else if (Args[1] == "sobelY")
			{
				new GreyFilter().ApplyingGreyFilter(Image);
				SobelYFilter Filter = new SobelYFilter();
				Filter.MatrixFormalization(Image, 3, Filter.Kernel, Filter.Divider);
				Image.SavingFile(Args[2]);
				return 0;
			}
			else
			{
				Console.WriteLine("ERROR: you have written the worg filter name. Filters' names:\n-Median filter 3x3 - median;\n-Gaussian filter 3x3 - gauss3;\n-Gaussian filter 5x5 - gauss5;\n-Sobel filter on X - sobelX;\n-Sobel filter on Y - sobelY;\n-grey filter - grey.\n\n");
				return -1;
			}
		}
	}
}
