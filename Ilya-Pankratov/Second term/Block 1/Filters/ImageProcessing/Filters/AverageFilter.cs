namespace ImageProcessing
{
    public class AverageFilter : Image
    {
        private int[,] averageingKernel;
        private int kernelSize;

        private int KernelSize
        {
            get => kernelSize;
            set
            {
                if (value < 3 || value % 2 == 0)
                {
                    kernelSize = 3;
                }
                else
                {
                    kernelSize = value;
                }
                return;
            }
        }

        public AverageFilter(BMPFileHeader fileInfo, FileStream inputFile) : base(fileInfo, inputFile)
        {
        }

        public AverageFilter(BMPFileHeader fileInfo, byte[] inputFile) : base(fileInfo, inputFile)
        {
        }

        public void ApplyAverageFilter(int kernelSize = 3)
        {
            CreateKernel(kernelSize);
            int offset = kernelSize / 2;
            byte[,] newData = new byte[height, bytesInRow];

            for (int i = offset; i < height - offset; i++)
            {
                for (int j = offset * bytesPerPixel; j < bytesInRow - offset * bytesPerPixel; j += bytesPerPixel)
                {
                    for (int rgb = 0; rgb < 3; rgb++)
                    {
                        newData[i, j + rgb] = ApplyKernel(i - offset, j - offset * bytesPerPixel, rgb);
                    }
                }
            }

            bytes = newData;
            Recovery(offset);
            return;
        }

        private void CreateKernel(int kernelSize = 3)
        {
            this.kernelSize = kernelSize;
            averageingKernel = new int[kernelSize, kernelSize];
            for (int i = 0 ; i < kernelSize; i++)
            {
                for (int j = 0 ; j < kernelSize; j++)
                {
                    averageingKernel[i, j] = 1;
                }
            }

            return;
        }

        private byte ApplyKernel(int x, int y, int rgb)
        {
            double sum = 0;

            for (int i = 0; i < kernelSize; i++)
            {
                for (int j = 0 ; j < kernelSize * bytesPerPixel; j += bytesPerPixel)
                {
                    sum += (double)averageingKernel[i, j / bytesPerPixel] * (int)bytes[x + i, y + j + rgb];
                }
            }

            return (byte)(sum / (kernelSize * kernelSize));
        }
    }
}
