using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
	public class GrayFilter
	{
		public static void Gray(BMPImage image)
		{
			for (int x = 0; x < image.Width; x++)
				for (int y = 0; y < image.Height; y++)
				{
					Pixel pixel = image.GetPixel(x, y);
					int sum = pixel.Red + pixel.Green + pixel.Blue;
					pixel.Red = pixel.Green = pixel.Blue = (byte)(sum / 3);
					image.SetPixel(pixel, x, y);
				}
		}
	}
}
