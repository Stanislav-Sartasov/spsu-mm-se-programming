using System;
using System.Runtime.InteropServices;

namespace Filters
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("This program applies filters to an image.");

			if (args.Length != 3)
			{
				Console.WriteLine("You need to enter three arguments.");
				return;
			}

			Image image = new Image(args[0]);
			if (args[1] == "Gauss")
				Gauss(image);
			else if (args[1] == "Gray")
				GrayFilter(image);
			else if (args[1] == "SobelX")
				SobelX(image);
			else if (args[1] == "SobelY")
				SobelY(image);
			else if (args[1] == "Median")
				Median(image);
			else
			{
				Console.WriteLine("You have entered the wrong filter name. Available names: Gauss, Gray, SobelX, SobelY, Median.");
				return;
			}

			image.SavePicture(args[2]);

			Console.WriteLine("The filter has been appled.");
		}

		public static void Gauss(Image image)
		{
			float[,] matrix = new float[,]
			{
				{1.0f/16.0f, 1.0f/8.0f, 1.0f/16.0f},
				{1.0f/8.0f, 1.0f/4.0f, 1.0f/8.0f},
				{1.0f/16.0f, 1.0f/8.0f, 1.0f/16.0f}
			};
			Filter(matrix, image);
		}

		public static void SobelX(Image image)
		{
			GrayFilter(image);
			float[,] matrix = new float[,]
			{
				{-1f, 0f, 1f},
				{-2f, 0f, 2f},
				{-1f, 0f, 1f}
			};
			Filter(matrix, image);
		}

		public static void SobelY(Image image)
		{
			GrayFilter(image);
			float[,] matrix = new float[,]
			{
				{-1, -2, -1},
				{0, 0, 0f},
				{1f, 2f, 1f}
			};
			Filter(matrix, image);
		}

		public static void GrayFilter(Image image)
		{
			for (int i = 0; i < image.Width; i++)
			{
				for (int j = 0; j < image.Height; j++)
				{
					Pixel currentpixel;
					int sum;
					currentpixel = image.GetPixel(i, j);
					sum = (currentpixel.Red + currentpixel.Green + currentpixel.Blue) / 3;
					currentpixel.Red = (byte)sum;
					currentpixel.Green = (byte)sum;
					currentpixel.Blue = (byte)sum;
					image.SetPixel(currentpixel, i, j);
				}
			}
		}

		public static void Median(Image image)
		{
			Pixel[,] pixels = new Pixel[image.Width, image.Height];

			int[] r = new int[9];
			int[] g = new int[9];
			int[] b = new int[9];

			for (int i = 1; i < image.Width - 1; i++)
			{
				for (int j = 1; j < image.Height - 1; j++)
				{
					for (int k = 0; k < 3; k++)
					{
						for (int l = 0; l < 3; l++)
						{
							r[3 * k + l] = (image.GetPixel(i + k - 1, j + l - 1).Red);
							g[3 * k + l] = (image.GetPixel(i + k - 1, j + l - 1).Green);
							b[3 * k + l] = (image.GetPixel(i + k - 1, j + l - 1).Blue);
						}
					}
					Array.Sort(r);
					Array.Sort(g);
					Array.Sort(b);
					pixels[i, j] = new Pixel((byte)r[4], (byte)g[4], (byte)b[4]);
				}
			}

			for (int i = 1; i < image.Width - 1; i++)
			{
				for (int j = 1; j < image.Height - 1; j++)
				{
					image.SetPixel(pixels[i, j], i, j);
				}
			}
		}

		public static void Filter(float[,] matrix, Image image)
		{
			Pixel[,] pixels = new Pixel[image.Width, image.Height];

			float r;
			float g;
			float b;

			for (int i = 1; i < image.Width - 1; i++)
			{
				for (int j = 1; j < image.Height - 1; j++)
				{
					r = 0;
					g = 0;
					b = 0;

					for (int k = 0; k < 3; k++)
					{
						for (int l = 0; l < 3; l++)
						{
							r += (image.GetPixel(i + k - 1, j + l - 1).Red * matrix[l, k]);
							g += (image.GetPixel(i + k - 1, j + l - 1).Green * matrix[l, k]);
							b += (image.GetPixel(i + k - 1, j + l - 1).Blue * matrix[l, k]);
						}
					}

					pixels[i, j] = new Pixel((byte)Math.Abs(r), (byte)Math.Abs(g), (byte)Math.Abs(b));
				}
			}

			for (int i = 1; i < image.Width - 1; i++)
			{
				for (int j = 1; j < image.Height - 1; j++)
				{
					image.SetPixel(pixels[i, j], i, j);
				}
			}
		}
	}
}