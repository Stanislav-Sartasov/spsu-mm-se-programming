using System;

namespace Filters
{
	class ConvMatrix
	{
		internal static void MultByConvMatrix(double[] core, byte[] mas, uint width, uint height, int div)
		{
			byte[] block = new byte[3 * height * width];
			double r = 0;
			double g = 0;
			double b = 0;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					for (int s = 0; s < 3; s++)
					{
						for (int t = 0; t < 3; t++)
						{
							if ((i + s - 1 >= 0) && (i + s - 1 < height) && (j + t - 1 >= 0) && (j + t - 1) < width)
							{
								r += mas[((i + s - 1) * width + j + t - 1) * 3] * core[s * 3 + t];
								g += mas[((i + s - 1) * width + j + t - 1) * 3 + 1] * core[s * 3 + t];
								b += mas[((i + s - 1) * width + j + t - 1) * 3 + 2] * core[s * 3 + t];
							}
						}
					}
					if (div != 0)
					{
						block[(i * width + j) * 3] = (byte)(r / div);
						block[(i * width + j) * 3 + 1] = (byte)(g / div);
						block[(i * width + j) * 3 + 2] = (byte)(b / div);
					}
					else
					{
						double x;
						if (r + g + b > 384)
							x = 255;
						else
							x = (double) ((Math.Abs((int) r) + Math.Abs((int) b) + (double)Math.Abs((int) g)) / 3);
						block[(i * width + j) * 3] = (byte)x;
						block[(i * width + j) * 3 + 1] = (byte)x;
						block[(i * width + j) * 3 + 2] = (byte)x;
					}
					r = g = b = 0;
				}
			}
			for (int i = 1; i < height - 1; i++)
			{
				for (int j = 1; j < width - 1; j++)
				{
					mas[(i * width + j) * 3] = block[(i * width + j) * 3];
					mas[(i * width + j) * 3 + 1] = block[(i * width + j) * 3 + 1];
					mas[(i * width + j) * 3 + 2] = block[(i * width + j) * 3 + 2];
				}
			}
		}
	}
}
