using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Description\n" +
				"Filters: sobelX, gray, sobelY sobelXY, scharrX, scharrY, scharrXY, " +
				"gauss(2n+1), median\n" +
				"Args: 0 - initialImageName, 1 - filterName, 2 - newImageName");

			Filter filterPack = new Filter();

			Image image = new Image(args[0] + ".bmp");

			image.ApplyFilter(filterPack[args[1]], args[2] + ".bmp");
			
		}
	}
}
