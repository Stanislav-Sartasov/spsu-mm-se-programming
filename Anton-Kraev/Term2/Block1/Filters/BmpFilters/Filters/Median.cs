namespace BmpFilters.Filters
{
    public abstract class MedianFilter
    {
        public static byte[][] ApplyFilter(uint width, uint height, int channels, byte[][] image)
        {
			byte[] matrix = new byte[9];
			int c = 0;
			
			for (int color = 0; color < 3; color++)
				for (int row = 1; row < height - 1; row++)
					for (int col = color + channels; col < width - channels; col += channels)
					{
						for (int y = row - 1; y < row + 2; y++)
							for (int x = col - channels; x < col + channels + 1; x += channels)
							{
								matrix[c] = image[y][x];
								c++;
							}
						c = 0;
						Array.Sort(matrix);
						image[row][col] = matrix[4];
					}

			return image;
		}
    }
}