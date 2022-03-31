namespace ImageProcessing
{
    public class NegativeFilter : Image
    {
        public NegativeFilter(BMPFileHeader fileInfo, FileStream inputFile) : base(fileInfo, inputFile)
        {
        }

        public NegativeFilter(BMPFileHeader fileInfo, byte[] inputFile) : base(fileInfo, inputFile)
        {
        }

        private byte TranserToOppositeMeaning(int x, int y, int rgb)
        {
            return (byte)(255 - bytes[x, y + rgb]);
        }

        public void ApplyNegativeFilter()
        {
            int offset = 0;

            for (int i = offset; i < height; i++)
            {
                for (int j = offset * bytesPerPixel; j < bytesInRow - offset * bytesPerPixel; j += bytesPerPixel)
                {
                    for (int rgb = 0; rgb < 3; rgb++)
                    {
                        bytes[i, j + rgb] = TranserToOppositeMeaning(i, j , rgb);
                    }
                }
            }
            return;
        }
    }
}
