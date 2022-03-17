using System;

namespace BMPFilters
{
	public class Filters
	{
		public static void ApplyMedian(BMPImage image)
		{
			for (int i = 1; i < image.Height - 1; i++)
			{
				for (int j = 1; j < image.Width - 1; j++)
				{
					for (int c = 0; c < image.Channels; c++)
					{
						int[] surroundings = new int[9];
						for (int k = -1; k < 2; k++)
						{
							for (int l = -1; l < 2; l++)
							{
								surroundings[(k + 1) * 3 + l + 1] = image.Bytes[i + k][j + l][c];
							}
						}
						Array.Sort(surroundings);
						image.Bytes[i][j][c] = (byte)surroundings[4];
					}
				}
			}
		}

		public static void ApplyGauss(BMPImage image)
		{
			double[] kernel = new double[] { 0.0626, 0.1250, 0.0626, 0.1250, 0.2497, 0.1250, 0.0626, 0.1250, 0.0626 };
			Convolute(image, kernel);
		}

		public static void ApplySobelX(BMPImage image)
		{
			double[] kernel = new double[] { 1, 0, -1, 2, 0, -2, 1, 0, -1 };
			Convolute(image, kernel);
		}

		public static void ApplySobelY(BMPImage image)
		{
			double[] kernel = new double[] { 1, 2, 1, 0, 0, 0, -1, -2, -1 };
			Convolute(image, kernel);
		}

		public static void ApplyGrayscale(BMPImage image)
		{
			for (int i = 1; i < image.Height - 1; i++)
			{
				for (int j = 1; j < image.Width - 1; j++)
				{
					for (int c = 0; c < image.Channels; c++)
					{
						if (c == 3)
							image.Bytes[i][j][c] = 255;
						else
							image.Bytes[i][j][c] = (byte)Math.Max(image.Bytes[i][j][2], Math.Max(image.Bytes[i][j][1], image.Bytes[i][j][0]));
					}
				}
			}
		}

		private static void Convolute(BMPImage image, double[] kernel)
		{
			byte[][][] copy = new byte[image.Height][][];
			for (int i = 0; i < image.Height; i++)
			{
				copy[i] = new byte[image.Width][];
				for (int j = 0; j < image.Width; j++)
				{
					copy[i][j] = new byte[image.Channels];
				}
			}

			for (int i = 1; i < image.Height - 1; i++)
			{
				for (int j = 1; j < image.Width - 1; j++)
				{
					for (int c = 0; c < image.Channels; c++)
					{
						double pixelByte = 0;
						for (int k = -1; k < 2; k++)
						{
							for (int l = -1; l < 2; l++)
							{
								pixelByte += kernel[(k + 1) * 3 + l + 1] * image.Bytes[i + k][j + l][c];
							}
						}
						if (pixelByte == 0)
							copy[i][j][c] = 0;
						else if (Math.Abs(pixelByte) < 255)
							copy[i][j][c] = (byte)Math.Abs(pixelByte);
						else
							copy[i][j][c] = 255;
					}
				}
			}

			for (int i = 0; i < image.Height; i++)
			{
				for (int j = 0; j < image.Width; j++)
				{
					for (int c = 0; c < image.Channels; c++)
					{
						image.Bytes[i][j][c] = copy[i][j][c];
					}
				}
			}
		}
	}
}
