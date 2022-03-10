namespace BMPFilter.Filters
{
    public static class GrayFilter
    {
        private const double RedCoefficient = 0.3;
        private const double GreenCoefficient = 0.59;
        private const double BlueCoefficient = 0.11;

        public static void ApplyFilter(BitMapFile image)
        {
            for (int i = 0; i < image.Height; ++i)
            {
                for (int j = 0; j < image.RowByteSize; j += image.Channels)
                {
                    // Colors are stored in BGR order
                    byte color = (byte)(BlueCoefficient * image.ImageBytes[i][j] +
                                         GreenCoefficient * image.ImageBytes[i][j + 1] +
                                         RedCoefficient * image.ImageBytes[i][j + 2]);
                    image.ImageBytes[i][j] = color;
                    image.ImageBytes[i][j + 1] = color;
                    image.ImageBytes[i][j + 2] = color;
                }
            }
        }
    }
}