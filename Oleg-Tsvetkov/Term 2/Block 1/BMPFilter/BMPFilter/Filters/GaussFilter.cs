namespace BMPFilter.Filters
{
    public static class GaussFilter
    {
        private static readonly double[] GaussCoefficients = { 0.25, 0.125, 0.0625 };
        public static void ApplyFilter(BitMapFile image)
        {
            for (int color = 0; color < 3; ++color)
            {
                for (int i = 1; i < image.Height - 1; ++i)
                {
                    for (int j = image.Channels; j < image.RowByteSize - image.Channels; j += image.Channels)
                    {
                        double result = 0;
                        for (int n = -1; n < 2; ++n)
                        {
                            for (int m = -1; m < 2; ++m)
                            {
                                double coefficient;
                                if (m == 0 && n == 0)
                                {
                                    coefficient = GaussCoefficients[0];
                                }
                                else if (m != 0 && n != 0)
                                {
                                    coefficient = GaussCoefficients[2];
                                }
                                else
                                {
                                    coefficient = GaussCoefficients[1];
                                }
                                result += image.ImageBytes[i + n][j + color + image.Channels * m] * coefficient;
                            }
                        }

                        image.ImageBytes[i][j + color] = (byte)result;
                    }
                }
            }
        }
    }
}