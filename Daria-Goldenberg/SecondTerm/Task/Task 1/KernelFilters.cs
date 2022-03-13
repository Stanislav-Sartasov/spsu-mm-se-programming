using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
	public class KernelFilters
	{
		public static void UseMatrix(BMPImage image, float[,] kernel)
		{
			float red;
			float green;
			float blue;

			Pixel[,] copy = new Pixel[image.Width, image.Height];

			for (int x = 1; x < image.Width - 1; x++)
				for (int y = 1; y < image.Height - 1; y++)
				{
					red = 0;
					green = 0;
					blue = 0;
					for (int i = 0; i < 3; i++)
						for (int j = 0; j < 3; j++)
						{
							red += image.GetPixel(x + i - 1, y + j - 1).Red * kernel[i, j];
							green += image.GetPixel(x + i - 1, y + j - 1).Green * kernel[i, j];
							blue += image.GetPixel(x + i - 1, y + j - 1).Blue * kernel[i, j];
						}
					red = Math.Abs(red);
					green = Math.Abs(green);
					blue = Math.Abs(blue);
					copy[x, y] = new Pixel((byte)red, (byte)green, (byte)blue);
				}

			for (int x = 1; x < image.Width - 1; x++)
				for (int y = 1; y < image.Height - 1; y++)
					image.SetPixel(copy[x, y], x, y);
		}
		public static void SobelY(BMPImage image)
		{
			GrayFilter.Gray(image);
			float[,] kernel = new float[,] { { -1f, -2f, -1f }, { 0f, 0f, 0f }, { 1f, 2f, 1f } };
			UseMatrix(image, kernel);
		}

		public static void SobelX(BMPImage image)
		{
			GrayFilter.Gray(image);
			float[,] kernel = new float[,] { { -1f, 0f, 1f }, { -2f, 0f, 2f }, { -1f, 0f, 1 } };
			UseMatrix(image, kernel);
		}

		public static void Gauss(BMPImage image)
		{
			float[,] kernel = new float[,] { { 1f / 16f, 1f / 8f, 1f / 16f }, { 1f / 8f, 1f / 4f, 1f / 8f }, { 1f / 16f, 1f / 8f, 1f / 16f } };
			UseMatrix(image, kernel);
		}
	}
}
