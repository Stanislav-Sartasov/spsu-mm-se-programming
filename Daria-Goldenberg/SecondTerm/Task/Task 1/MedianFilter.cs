using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Task_1
{
	public class MedianFilter
	{
		public static void Median(BMPImage image)
		{
			byte[] red = new byte[9];
			byte[] green = new byte[9];
			byte[] blue = new byte[9];

			Pixel[,] copy = new Pixel[image.Width, image.Height];

			for (int x = 1; x < image.Width - 1; x++)
				for (int y = 1; y < image.Height - 1; y++)
				{
					for (int i = 0; i < 3; i++)
						for (int j = 0; j < 3; j++)
						{
							red[3 * i + j] = (image.GetPixel(x + i - 1, y + j - 1).Red);
							green[3 * i + j] = (image.GetPixel(x + i - 1, y + j - 1).Green);
							blue[3 * i + j] = (image.GetPixel(x + i - 1, y + j - 1).Blue);
						}
					Array.Sort(red);
					Array.Sort(green);
					Array.Sort(blue);
					copy[x, y] = new Pixel(red[4], green[4], blue[4]);
				}

			for (int x = 1; x < image.Width - 1; x++)
				for (int y = 1; y < image.Height - 1; y++)
					image.SetPixel(copy[x, y], x, y);
		}
	}
}
