namespace BmpFilters.Filters
{
	public abstract class ConvolutionFilters
	{
		public static byte[][] Convolution(uint width, uint height, int channels, byte[][] image, double[] kernel)
		{
			byte[][] newImage = new byte[height][];
			for (int i = 0; i < height; i++)
				newImage[i] = new byte[width];

			double res = 0;
			for (int color = 0; color < 3; color++)
				for (int row = 1; row < height - 1; row++)
					for (int col = color + channels; col < width - channels; col += channels)
					{
						for (int y = 0; y < 3; y++)
							for (int x = 0; x < 3; x++)
							{
								res += image[row + y - 1][col + (x - 1) * channels] * kernel[y * 3 + x];
							}
						newImage[row][col] = (byte)Math.Min(255, Math.Max(0, res));
						res = 0;
					}

			return newImage;
		}

		public static byte[][] GaussFilter(uint width, uint height, int channels, byte[][] image)
		{
			double[] kernel = { 1.0 / 16, 2.0 / 16, 1.0 / 16, 2.0 / 16, 4.0 / 16, 2.0 / 16, 1.0 / 16, 2.0 / 16, 1.0 / 16 };
			return Convolution(width, height, channels, image, kernel);
		}

		public static byte[][] SobelXFilter(uint width, uint height, int channels, byte[][] image)
		{
			image = GrayFilter.ApplyFilter(width, height, channels, image);
			double[] kernel = { -1, -2, -1, 0, 0, 0, 1, 2, 1 };
			return Convolution(width, height, channels, image, kernel);
		}

		public static byte[][] SobelYFilter(uint width, uint height, int channels, byte[][] image)
		{
			image = GrayFilter.ApplyFilter(width, height, channels, image);
			double[] kernel = { -1, 0, 1, -2, 0, 2, -1, 0, 1 };
			return Convolution(width, height, channels, image, kernel);
		}

		public static byte[][] SobelFilter(uint width, uint height, int channels, byte[][] image)
        {
			byte[][] sx = SobelXFilter(width, height, channels, image);
			byte[][] sy = SobelYFilter(width, height, channels, image);

			for(int i = 0; i < height; i++)
				for(int j = 0; j < width; j++)
					image[i][j] = (byte)Math.Min(255, Math.Sqrt(sx[i][j] * sx[i][j] + sy[i][j] * sy[i][j]));

			return image;
        }
	}
}