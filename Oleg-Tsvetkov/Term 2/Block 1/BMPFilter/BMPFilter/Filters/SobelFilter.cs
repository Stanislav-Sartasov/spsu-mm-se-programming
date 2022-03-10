using System;

namespace BMPFilter.Filters
{
    public static class SobelFilter
    {
        private static readonly double[,] SobelYCoefficients = {{-1, -2, -1},
            {0,  0,  0},
            {1,  2,  1}};
        private static readonly double[,] SobelXCoefficients = {{-1, 0, 1},
            {-2, 0, 2},
            {-1, 0, 1}};
        private const int SobelEdgeLimit = 70;
        public enum Type
        {
            X, Y
        }
        public static void ApplyFilter(BitMapFile image, SobelFilter.Type type)
        {
            GrayFilter.ApplyFilter(image);

            byte[][] copyImageBytes = new byte[image.Height][];

            for (int i = 0; i < image.Height; ++i)
            {
                copyImageBytes[i] = new byte[image.RowByteSize];
                for (int j = 0; j < image.RowByteSize; ++j)
                {
                    copyImageBytes[i][j] = image.ImageBytes[i][j];
                }
            }

            for (int i = 1; i < image.Height - 1; ++i)
            {
                for (int j = image.Channels; j < image.RowByteSize - image.Channels; j += image.Channels)
                {
                    int result = 0;
                    for (int n = -1; n < 2; ++n)
                    {
                        for (int m = -1; m < 2; ++m)
                        {
                            if (type == Type.Y)
                            {
                                result += (int)(copyImageBytes[i + n][j + image.Channels * m] * SobelYCoefficients[n + 1, m + 1]);
                            }
                            else
                            {
                                result += (int)(copyImageBytes[i + n][j + image.Channels * m] * SobelXCoefficients[n + 1, m + 1]);
                            }
                        }
                    }

                    for (int k = 0; k < 3; ++k)
                    {
                        image.ImageBytes[i][j + k] = Math.Abs(result) < SobelEdgeLimit ? (byte)0 : (byte)255;
                    }
                }
            }
        }
    }
}