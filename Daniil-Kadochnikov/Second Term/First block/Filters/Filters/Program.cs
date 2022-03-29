using System;

namespace Filters
{
	class Program
	{
		static int Main(string[] args)
		{
			Console.WriteLine("\nThe program gets a 24 or 32 bit BMP-file and applies one of the following filters:\n-Median filter 3x3 - median;\n-Gaussian filter 3x3 - gauss3;\n-Gaussian filter 5x5 - gauss5;\n-Sobel filter on X - sobelX;\n-Sobel filter on Y - sobelY;\n-grey filter - grey.\n\n");
			
			if (args.Length != 3)
			{
				Console.WriteLine("Inappropriate amount of arguments.\nAppropriate from: <program> <input file name> <filter name> <output file name>\n");
				return -1;
			}

			Image image = new Image();
			image.BitMap(args[0]);

			if (args[1] == "grey")
			{
				new GreyFilter().ApplyGreyFilter(image);
				image.SaveFile(args[2]);
				return 0;
			}
			else if (args[1] == "median")
			{
				new MedianFilter().ApplyMedianFilter(image, 3);
				image.SaveFile(args[2]);
				return 0;
			}
			else if (args[1] == "gauss3")
			{
				Gauss3Filter filter = new Gauss3Filter();
				filter.FormalizeMatrix(image, 3, filter.Kernel, filter.Divider);
				image.SaveFile(args[2]);
				return 0;
			}
			else if (args[1] == "gauss5")
			{
				Gauss5Filter filter = new Gauss5Filter();
				filter.FormalizeMatrix(image, 5, filter.Kernel, filter.Divider);
				image.SaveFile(args[2]);
				return 0;
			}
			else if (args[1] == "sobelX")
			{
				new GreyFilter().ApplyGreyFilter(image);
				SobelXFilter filter = new SobelXFilter();
				filter.FormalizeMatrix(image, 3, filter.Kernel, filter.Divider);
				image.SaveFile(args[2]);
				return 0;
			}
			else if (args[1] == "sobelY")
			{
				new GreyFilter().ApplyGreyFilter(image);
				SobelYFilter filter = new SobelYFilter();
				filter.FormalizeMatrix(image, 3, filter.Kernel, filter.Divider);
				image.SaveFile(args[2]);
				return 0;
			}
			else
			{
				Console.WriteLine("ERROR: you have written the wrong filter name. Filters' names:\n-Median filter 3x3 - median;\n-Gaussian filter 3x3 - gauss3;\n-Gaussian filter 5x5 - gauss5;\n-Sobel filter on X - sobelX;\n-Sobel filter on Y - sobelY;\n-grey filter - grey.\n\n");
				return -1;
			}
		}
	}
}
