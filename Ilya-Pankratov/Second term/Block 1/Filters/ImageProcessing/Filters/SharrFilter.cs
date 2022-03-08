namespace ImageProcessing
{
    public class SharrFilter : GreyFilter
    { 
        public enum FilterType {SharrFilterX, SharrFilterY, SharrFilter};
        private int kernelSize = 3;

        private int[,] SharrKernelX =
        {{3, 0, -3},
         {10, 0, -10},
         {3, 0, -3}};

        private int[,] SharrKernelY =
        {{3, 10, 3},
         {0, 0, 0},
         {-3, -10, -3}};

        public SharrFilter(BMPFile fileInfo, FileStream inputFile) : base(fileInfo, inputFile)
        {
        }

        public SharrFilter(BMPFile fileInfo, byte[] inputFile) : base(fileInfo, inputFile)
        {
        }

        public void ApplySharrFilter(FilterType name)
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
                        case FilterType.SharrFilterX:
                            newData[i, j + rgb] = ByteSharrFilterX(i - offset, j - offset * bytesPerPixel);
                            break;

                        case FilterType.SharrFilterY:
                            newData[i, j + rgb] = ByteSharrFilterY(i - offset, j - offset * bytesPerPixel);
                            break;

                        case FilterType.SharrFilter:
                            newData[i, j + rgb] = ByteSharrFilter(i - offset, j - offset * bytesPerPixel);
                            break;
                        }
                    }
                }
            }

            bytes = newData;
            Recovery(offset);
            return;
        }

        private byte ByteSharrFilterX(int x, int y)
        {
            int sum = 0;
            int sensivity = 300;

            for (int i = 0; i < kernelSize; i++)
                for (int j = 0; j < kernelSize * bytesPerPixel; j += bytesPerPixel)
                    sum += SharrKernelX[i, j / bytesPerPixel] * (int)bytes[x + i, y + j];

            return (byte)(Math.Abs(sum) < sensivity ? 0 : 255);
        }

        private byte ByteSharrFilterY(int x, int y)
        {
            int sum = 0;
            int sensivity = 300;

            for (int i = 0; i < kernelSize; i++)
                for (int j = 0; j < kernelSize * bytesPerPixel; j += bytesPerPixel)
                    sum += SharrKernelY[i, j / bytesPerPixel] * (int)bytes[x + i, y + j];

            return (byte)(Math.Abs(sum) < sensivity ? 0 : 255);
        }

        private byte ByteSharrFilter(int x, int y)
        {
            int[] sum = { 0, 0 };
            int sensivity = 300;


            for (int i = 0; i < kernelSize; i++)
                for (int j = 0; j < kernelSize * bytesPerPixel; j += bytesPerPixel)
                {
                    sum[0] += SharrKernelX[i, j / bytesPerPixel] * (int)bytes[x + i, y + j];
                    sum[1] += SharrKernelY[i, j / bytesPerPixel] * (int)bytes[x + i, y + j];
                }

            return (byte)(Math.Sqrt(sum[0] * sum[0] + sum[1] * sum[1]) < sensivity ? 0 : 255);
        }
    }
}
