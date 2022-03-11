namespace ImageProcessing
{
    public class MedianFilter : Image
    {
        private int[] medianKernel;
        private int index = 0;
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

        public MedianFilter(BMPFileHeader fileInfo, FileStream inputFile) : base(fileInfo, inputFile)
        {
        }

        public MedianFilter(BMPFileHeader fileInfo, byte[] inputFile) : base(fileInfo, inputFile)
        {
        }

        public void ApplyMedianFilter(int kernelSize = 3)
        {
            this.kernelSize = kernelSize;
            medianKernel = new int[kernelSize * kernelSize];
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

        private byte ApplyKernel(int x, int y, int rgb)
        {
            for (int i = 0; i < kernelSize; i++)
            {
                for (int j = 0; j < kernelSize * bytesPerPixel; j += bytesPerPixel)
                {
                    medianKernel[index++] = (int)bytes[x + i, y + j + rgb];
                }
            }

            index = 0;
            Array.Sort(medianKernel);
            return (byte)medianKernel[kernelSize * kernelSize / 2];
        }
    }
}
