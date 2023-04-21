namespace BmpFilters.Filters
{
    public abstract class GrayFilter
    {
        public static byte[][] ApplyFilter(uint width, uint height, int channels, byte[][] image)
        {
            byte avg;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x += channels)
                {
                    avg = (byte)(image[y][x] * 0.299 + image[y][x + 1] * 0.587 + image[y][x + 2] * 0.114);
                    image[y][x] = avg;
                    image[y][x + 1] = avg;
                    image[y][x + 2] = avg;
                }
            }

            return image;
        }
    }
}
