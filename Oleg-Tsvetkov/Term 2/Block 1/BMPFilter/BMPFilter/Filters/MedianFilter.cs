namespace BMPFilter.Filters
{
    public static class MedianFilter
    {
        public static void ApplyFilter(BitMapFile image)
        {
            for (int color = 0; color < 3; ++color)
            {
                for (int i = 1; i < image.Height - 1; ++i)
                {
                    for (int j = image.Channels; j < image.RowByteSize - image.Channels; j += image.Channels)
                    {
                        int[] nearColors = new int[9];
                        for (int n = -1; n < 2; ++n)
                        {
                            for (int m = -1; m < 2; ++m)
                            {
                                nearColors[(n + 1) * 3 + m + 1] = image.ImageBytes[i + n][j + color + image.Channels * m];
                            }
                        }
                        Array.Sort(nearColors);
                        image.ImageBytes[i][j + color] = (byte)nearColors[4];
                    }
                }
            }
        }
    }
}