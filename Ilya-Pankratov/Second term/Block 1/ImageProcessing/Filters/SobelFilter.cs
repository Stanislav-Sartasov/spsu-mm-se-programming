namespace ImageProcessing
{
    public class SobelFilter : GreyFilter
    {
        public enum FilterType { SobelFilterX, SobelFilterY, SobelFilter };
        private int kernelSize = 3;

        private int[,] sobelKernelX =
        {{1, 0, -1},
         {2, 0, -2},
         {1, 0, -1}};

        private int[,] sobelKernelY =
        {{1, 2, 1},
         {0, 0, 0},
         {-1, -2, -1}};

        public SobelFilter(BMPFile fileInfo, FileStream inputFile) : base(fileInfo, inputFile)
        {
        }

        public SobelFilter(BMPFile fileInfo, byte[] inputFile) : base(fileInfo, inputFile)
        {
        }

        public void ApplySobelFilter(FilterType name)
        {
            int offset = kernelSize / 2;
            byte[,] newData = new byte[height, bytesInRow];

            ApplyGreyFilter();

            for (int i = offset; i < height - offset; i++)
            {
                for (int j = offset * bytesPerPixel; j < bytesInRow - offset * bytesPerPixel; j += bytesPerPixel)
                {
                    for (int rgb = 0; rgb < 3; rgb++)
                    {
                        switch (name)
                        {
                            case FilterType.SobelFilterX:
                                newData[i, j + rgb] = ByteSobelFilterX(i - offset, j - offset * bytesPerPixel);
                                break;

                            case FilterType.SobelFilterY:
                                newData[i, j + rgb] = ByteSobelFilterY(i - offset, j - offset * bytesPerPixel);
                                break;

                            case FilterType.SobelFilter:
                                newData[i, j + rgb] = ByteSobelFilter(i - offset, j - offset * bytesPerPixel);
                                break;
                        }
                    }
                }
            }

            bytes = newData;
            Recovery(offset);
            return;
        }

        public byte ByteSobelFilterX(int x, int y)
        {
            int sum = 0;
            int sensivity = 60;

            for (int i = 0; i < kernelSize; i++)
                for (int j = 0; j < kernelSize * bytesPerPixel; j += bytesPerPixel)
                    sum += sobelKernelX[i, j / bytesPerPixel] * (int)bytes[x + i, y + j];

            return (byte)(Math.Abs(sum) < sensivity ? 0 : 255);
        }

        public byte ByteSobelFilterY(int x, int y)
        {
            int sum = 0;
            int sensivity = 60;

            for (int i = 0; i < kernelSize; i++)
                for (int j = 0; j < kernelSize * bytesPerPixel; j += bytesPerPixel)
                    sum += sobelKernelY[i, j / bytesPerPixel] * (int)bytes[x + i, y + j];

            return (byte)(Math.Abs(sum) < sensivity ? 0 : 255);
        }

        public byte ByteSobelFilter(int x, int y)
        {
            int[] sum = {0, 0};
            int sensivity = 60;


            for (int i = 0; i < kernelSize; i++)
                for (int j = 0; j < kernelSize * bytesPerPixel; j += bytesPerPixel)
                {
                    sum[0] += sobelKernelX[i, j / bytesPerPixel] * (int)bytes[x + i, y + j];
                    sum[1] += sobelKernelY[i, j / bytesPerPixel] * (int)bytes[x + i, y + j];
                }

            return (byte)(Math.Sqrt(sum[0] * sum[0] + sum[1] * sum[1]) < sensivity ? 0 : 255);
        }
    }
}
