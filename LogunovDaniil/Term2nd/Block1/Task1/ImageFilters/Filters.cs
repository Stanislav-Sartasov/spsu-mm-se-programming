namespace ImageFilters
{
	public static class Filters
	{
		private delegate int filter(Bitmap bmp);
		private delegate byte converter(byte[] values);

		private static string[] FilterNames = { "GRAYSCALE", "MEDIAN", "GAUSS", "SOBELY", "SOBELX" };
		private static filter[] FiltersAvailable = { ApplyGrayscale,
													ApplyMedian,
													ApplyGauss,
													ApplySobelY,
													ApplySobelX };

		// fixates the order in which the pixels are being processed
		private static int[] ArrayX = { -1, 0, 1, -1, 0, 1, -1, 0, 1 };
		private static int[] ArrayY = { -1, -1, -1, 0, 0, 0, 1, 1, 1 };

		// returns filter by its name, or ApplyNoFilter if it wasn't found
		private static filter FindFilterByName(string filterName)
		{
			int id = Array.IndexOf(FilterNames, filterName.ToUpper());
			return id >= 0 && id < FiltersAvailable.Length ? FiltersAvailable[id] : ApplyNoFilterWithError;
		}

		public static bool CheckIfImplemented(string filterName)
		{
			return FindFilterByName(filterName) != ApplyNoFilterWithError;
		}

		// applies named filter to the given image;
		// if the filter wasn't found or the image is not properly loaded,
		// does not change the image data at all and returns a non-zero value
		public static int ApplyFilterByName(string filterName, Bitmap bmp)
		{
			if (!bmp.CheckIfLoaded())
				return -1;
			return FindFilterByName(filterName)(bmp);
		}

		private static int ApplyNoFilterWithError(Bitmap bmp)
		{
			return 1;
		}

		private static byte LimitToByte(int val)
		{
			if (val < 0)
				return 0;
			if (val > 255)
				return 255;
			return (byte)val;
		}

		private static byte ApplyMatrixPixel(byte[] values, int[] matrix, int normal)
		{
			int avg = 0;
			for (int i = 0; i < 9; i++)
				avg += values[i] * matrix[i];
			return LimitToByte(avg / normal);
		}

		private static int ApplyGrayscale(Bitmap bmp)
		{
			BitmapImage img = bmp.Image;
			for (int i = 0; i < img.Height; i++)
			{
				for (int j = 0; j < img.Width; j += img.ByteCount)
				{
					int avg = (img.Data[i, j] + img.Data[i, j + 1] + img.Data[i, j + 2]) / 3;
					img.Data[i, j] = img.Data[i, j + 1] = img.Data[i, j + 2] = (byte)avg;
				}
			}
			return 0;
		}

		private static int ApplyWindowedFilter(BitmapImage img, converter windowFilter)
		{
			byte[] toConvert = new byte[9];
			byte[,] newImgData = new byte[img.Height, img.Width];

			for (int clr = 0; clr < 3; clr++)
			{
				for (int x = 0; x < img.Height; x++)
				{
					for (int y = clr; y < img.Width; y += img.ByteCount)
					{
						for (int i = 0; i < 9; i++)
						{
							int nextX = x + ArrayX[i];
							int nextY = y + ArrayY[i] * img.ByteCount;
							toConvert[i] = (nextX >= 0 && nextX < img.Height && nextY >= 0 && nextY < img.Width)
								? img.Data[nextX, nextY]
								: (byte)0;
						}
						newImgData[x, y] = windowFilter(toConvert);
					}
				}
			}

			for (int x = 0; x < img.Height; x++)
			{
				for (int y = 0; y < img.Width; y++)
				{
					img.Data[x, y] = newImgData[x, y];
				}
			}

			return 0;
		}

		private static byte MedianConverter(byte[] values)
		{
			Array.Sort(values);
			return values[4];
		}

		private static byte GaussConverter(byte[] values)
		{
			int[] matrix = { 1, 2, 1, 2, 4, 2, 1, 2, 1 };
			return ApplyMatrixPixel(values, matrix, 16);
		}

		private static byte SobelXConverter(byte[] values)
		{
			int[] matrix = { 1, 0, -1, 2, 0, -2, 1, 0, -1 };
			return ApplyMatrixPixel(values, matrix, 1);
		}

		private static byte SobelYConverter(byte[] values)
		{
			int[] matrix = { 1, 2, 1, 0, 0, 0, -1, -2, -1 };
			return ApplyMatrixPixel(values, matrix, 1);
		}

		private static int ApplyGauss(Bitmap bmp)
		{
			return ApplyWindowedFilter(bmp.Image, GaussConverter);
		}

		private static int ApplySobelX(Bitmap bmp)
		{
			ApplyGrayscale(bmp);
			return ApplyWindowedFilter(bmp.Image, SobelXConverter);
		}

		private static int ApplySobelY(Bitmap bmp)
		{
			ApplyGrayscale(bmp);
			return ApplyWindowedFilter(bmp.Image, SobelYConverter);
		}

		private static int ApplyMedian(Bitmap bmp)
		{
			return ApplyWindowedFilter(bmp.Image, MedianConverter);
		}
	}
}
