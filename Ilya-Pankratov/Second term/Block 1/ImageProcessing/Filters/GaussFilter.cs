namespace ImageProcessing
{
    public class GaussFilter : Image
    {
        private double[,] gaussKernel;
        private int kernelSize;
        private int offset;
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

        public GaussFilter(BMPFile fileInfo, FileStream inputFile) : base(fileInfo, inputFile)
        {

        }

        public GaussFilter(BMPFile fileInfo, byte[] inputFile) : base(fileInfo, inputFile)
        {

        }

        public void ApplyGaussFilter(int kernelSize = 3)
        {
            CreateKernel(kernelSize);
            int offset = kernelSize / 2; // offset is a local variable here 
            byte[,] newData = new byte[height, bytesInRow];

            for (int i = offset; i < height - offset; i++)
            {
                for (int j = offset * bytesPerPixel; j < bytesInRow - offset * bytesPerPixel; j += bytesPerPixel)
                {
                    for (int rgb = 0; rgb < 3; rgb++)
                    {
                        newData[i, j + rgb] = ApplyGaussKernel(i - offset, j - offset * bytesPerPixel, rgb);
                    }
                }
            }

            bytes = newData;
            Recovery(offset);
            return;
        }

        private double GaussFunction(int x, int y, float sigma)
        {
            double temp = 2 * sigma * sigma;
            return (1 / (temp * 3.14)) * Math.Exp((-(x * x) - (y * y)) / temp);
        }

        private void CreateKernel(int kernelSize)
        {
            this.kernelSize = kernelSize;
            this.offset = kernelSize / 2 - kernelSize + 1;
            gaussKernel = new double[kernelSize, kernelSize];
            double sum = 0;

            for (int i = 0; i < kernelSize; i++)
            {
                for (int j = 0; j < kernelSize; j++)
                {
                    double result = GaussFunction(i + offset, j + offset, 1);
                    sum += result;
                    gaussKernel[i, j] = result;
                }
            }

            for (int i = 0; i < kernelSize; i++)
            {
                for (int j = 0; j < kernelSize; j++)
                {
                    gaussKernel[i, j] /= sum;
                }
            }
        }

        private byte ApplyGaussKernel(int x, int y, int rgb)
        {
            double sum = 0;
            for (int i = 0; i < kernelSize; i++)
                for (int j = 0; j < kernelSize * bytesPerPixel; j += bytesPerPixel)
                    sum += (double)(gaussKernel[i, j / bytesPerPixel] * bytes[x + i, y + j + rgb]);

            return (byte)sum;
        }
    }
}
