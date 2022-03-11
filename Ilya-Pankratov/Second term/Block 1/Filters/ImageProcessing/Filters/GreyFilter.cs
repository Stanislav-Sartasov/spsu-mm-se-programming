namespace ImageProcessing
{
    public class GreyFilter : Image
    {
        public GreyFilter(BMPFileHeader fileInfo, FileStream inputFile) : base(fileInfo, inputFile) 
        { 
        }

        public GreyFilter(BMPFileHeader fileInfo, byte[] inputFile) : base(fileInfo, inputFile)
        {
        }

        private byte TransferPixelToGrayColor(int x, int y)
        {
            return (byte)(0.2126 * bytes[x, y] + 0.7152 * bytes[x, y + 1] + 0.0722 * bytes[x, y + 2]);
        }

        public void ApplyGreyFilter()
        {
            int offset = 0;

            for (int i = offset; i < height; i++)
            {
                for (int j = offset * bytesPerPixel; j < bytesInRow - offset * bytesPerPixel; j += bytesPerPixel)
                {
                    bytes[i, j ] = bytes[i, j + 1] = bytes[i, j + 2] = TransferPixelToGrayColor(i, j);
                }
            }

            return;
        }
    }
}