using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("This program applies one of the filters to the image.");
			Console.WriteLine("List of available filters: Gray, Median, Gauss, SobelX, SobelY.\n");

			if (args.Length != 3)
			{
				Console.WriteLine("Incorrect number of arguments.");
				return;
			}

			BMPImage image = new BMPImage(args[0]);

			switch (args[1].ToLower())
			{
				case "gray":
					GrayFilter.Gray(image);
					break;
				case "median":
					MedianFilter.Median(image);
					break;
				case "gauss":
					KernelFilters.Gauss(image);
					break;
				case "sobelx":
					KernelFilters.SobelX(image);
					break;
				case "sobely":
					KernelFilters.SobelY(image);
					break;
				default:
					Console.WriteLine("Unknown filter selected. Available names: Gray, Median, Gauss, SobelX, SobelY.");
					return;
			}
			image.WriteToFile(args[2]);

			Console.WriteLine("The filter has been appled.");
		}
	}
}