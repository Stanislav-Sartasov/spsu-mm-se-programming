namespace Block1Task1
{
    /// <summary>
    /// Stores the values of each pixel channel (RGB).
    /// </summary>
    public struct Pixel
    {
        /// <summary>
        /// Blue-channel of the pixel.
        /// </summary>
        public byte Blue;

        /// <summary>
        /// Green-channel of the pixel.
        /// </summary>
        public byte Green;

        /// <summary>
        /// Red-channel of the pixel.
        /// </summary>
        public byte Red;

        /// <summary>
        /// Reads pixel from the file stream.
        /// </summary>
        /// <param name="binaryReader">Used to read data from the file stream.</param>
        public void ReadPixel(BinaryReader binaryReader)
        {
            this.Blue = binaryReader.ReadByte();
            this.Green = binaryReader.ReadByte();
            this.Red = binaryReader.ReadByte();
        }

        /// <summary>
        /// Writes pixel to the file stream.
        /// </summary>
        /// <param name="binaryWriter">Used to write data to the file stream.</param>
        public void WritePixel(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(this.Blue);
            binaryWriter.Write(this.Green);
            binaryWriter.Write(this.Red);
        }
    }

    /// <summary>
    /// Stores information about the file.
    /// </summary>
    public struct BitmapFileHeader
    {
        /// <summary>
        /// The size, in bytes, of the bitmap file.
        /// </summary>
        public int BfSize;

        /// <summary>
        /// The offset, in bytes, from the beginning of the BitmapFileHeader structure to the bitmap bits.
        /// </summary>
        public int BfOffBits;

        private short bfType;

        private short bfReserved1;

        private short bfReserved2;

        /// <summary>
        /// Reads BitmapFileHeader from the file stream.
        /// </summary>
        /// <param name="binaryReader">Used to read data from the file stream.</param>
        public void ReadBitmapFileHeader(BinaryReader binaryReader)
        {
            this.bfType = binaryReader.ReadInt16();
            this.BfSize = binaryReader.ReadInt32();
            this.bfReserved1 = binaryReader.ReadInt16();
            this.bfReserved2 = binaryReader.ReadInt16();
            this.BfOffBits = binaryReader.ReadInt32();
        }

        /// <summary>
        /// Writes BitmapFileHeader to the file stream.
        /// </summary>
        /// <param name="binaryWriter">Used to write data to the file stream.</param>
        public void WriteBitmapFileHeader(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(this.bfType);
            binaryWriter.Write(this.BfSize);
            binaryWriter.Write(this.bfReserved1);
            binaryWriter.Write(this.bfReserved2);
            binaryWriter.Write(this.BfOffBits);
        }
    }

    /// <summary>
    /// Stores information about the image.
    /// </summary>
    public struct BitmapInfoHeader
    {
        /// <summary>
        /// Specifies the width of the bitmap, in pixels.
        /// </summary>
        public int BiWidth;

        /// <summary>
        /// Specifies the height of the bitmap, in pixels.
        /// </summary>
        public int BiHeight;

        /// <summary>
        /// Specifies the number of bits per pixel (bpp).
        /// </summary>
        public short BiBitCount;

        /// <summary>
        /// Specifies the size, in bytes, of the image.
        /// </summary>
        public int BiSizeImage;

        /// <summary>
        /// Specifies the number of color indices in the color table that are actually used by the bitmap.
        /// </summary>
        public int BiClrUsed;

        /// <summary>
        /// Specifies the number of color indices that are considered important for displaying the bitmap.
        /// </summary>
        public int BiClrImportant;

        private int biSize;

        private short biPlanes;

        private int biCompression;

        private int biXPelsPerMeter;

        private int biYPelsPerMeter;

        /// <summary>
        /// Reads BitmapInfoHeader from the file stream.
        /// </summary>
        /// <param name="binaryReader">Used to read data from the file stream.</param>
        public void ReadBitmapInfoHeader(BinaryReader binaryReader)
        {
            this.biSize = binaryReader.ReadInt32();
            this.BiWidth = binaryReader.ReadInt32();
            this.BiHeight = binaryReader.ReadInt32();
            this.biPlanes = binaryReader.ReadInt16();
            this.BiBitCount = binaryReader.ReadInt16();
            this.biCompression = binaryReader.ReadInt32();
            this.BiSizeImage = binaryReader.ReadInt32();
            this.biXPelsPerMeter = binaryReader.ReadInt32();
            this.biYPelsPerMeter = binaryReader.ReadInt32();
            this.BiClrUsed = binaryReader.ReadInt32();
            this.BiClrImportant = binaryReader.ReadInt32();
        }

        /// <summary>
        /// Writes BitmapInfoHeader to the file stream.
        /// </summary>
        /// <param name="binaryWriter">Used to write data to the file stream.</param>
        public void WriteBitmapInfoHeader(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(this.biSize);
            binaryWriter.Write(this.BiWidth);
            binaryWriter.Write(this.BiHeight);
            binaryWriter.Write(this.biPlanes);
            binaryWriter.Write(this.BiBitCount);
            binaryWriter.Write(this.biCompression);
            binaryWriter.Write(this.BiSizeImage);
            binaryWriter.Write(this.biXPelsPerMeter);
            binaryWriter.Write(this.biYPelsPerMeter);
            binaryWriter.Write(this.BiClrUsed);
            binaryWriter.Write(this.BiClrImportant);
        }
    }

    /// <summary>
    /// Image processing: convolution / RGB => Grayscale.
    /// </summary>
    public class ImageProcessor
    {
        /// <summary>
        /// Сonvolution of the image.
        /// </summary>
        /// <param name="pathToRead">The path to the file to read.</param>
        /// <param name="pathToWrite">The path to the file to write.</param>
        /// <param name="kernel">Kernel of filter type for convolution.</param>
        /// <returns>0 - success, 1 - file not found.</returns>
        public static int Filter(string pathToRead, string pathToWrite, double[] kernel)
        {
            FileStream fileStreamIn;
            try
            {
                fileStreamIn = new FileStream(@pathToRead, FileMode.Open, FileAccess.Read);
            }
            catch (Exception)
            {
                return 1;
            }

            BinaryReader binaryReader = new BinaryReader(fileStreamIn);
            ReadImage(binaryReader, out BitmapFileHeader fileHeader, out BitmapInfoHeader infoHeader, out Pixel[,] image);
            binaryReader.Close();
            fileStreamIn.Close();

            Pixel[,] outImage = new Pixel[infoHeader.BiHeight, infoHeader.BiWidth];
            double sumBlue, sumGreen, sumRed;
            int finalSumBlue, finalSumGreen, finalSumRed;
            for (int line = 1; line < infoHeader.BiHeight - 1; line++)
            {
                for (int column = 1; column < infoHeader.BiWidth - 1; column++)
                {
                    sumBlue = 0;
                    sumGreen = 0;
                    sumRed = 0;
                    for (int kernelLine = 0; kernelLine < 3; kernelLine++)
                    {
                        for (int kernelColumn = 0; kernelColumn < 3; kernelColumn++)
                        {
                            sumBlue += image[line - 1 + kernelLine, column - 1 + kernelColumn].Blue * kernel[(3 * kernelLine) + kernelColumn];
                            sumGreen += image[line - 1 + kernelLine, column - 1 + kernelColumn].Green * kernel[(3 * kernelLine) + kernelColumn];
                            sumRed += image[line - 1 + kernelLine, column - 1 + kernelColumn].Red * kernel[(3 * kernelLine) + kernelColumn];
                        }
                    }

                    finalSumBlue = (int)Math.Round(sumBlue);
                    finalSumGreen = (int)Math.Round(sumGreen);
                    finalSumRed = (int)Math.Round(sumRed);

                    finalSumBlue = finalSumBlue > 255 ? 255 : finalSumBlue;
                    finalSumBlue = finalSumBlue < 0 ? 0 : finalSumBlue;
                    finalSumGreen = finalSumGreen > 255 ? 255 : finalSumGreen;
                    finalSumGreen = finalSumGreen < 0 ? 0 : finalSumGreen;
                    finalSumRed = finalSumRed > 255 ? 255 : finalSumRed;
                    finalSumRed = finalSumRed < 0 ? 0 : finalSumRed;

                    outImage[line, column].Blue = (byte)finalSumBlue;
                    outImage[line, column].Green = (byte)finalSumGreen;
                    outImage[line, column].Red = (byte)finalSumRed;
                }
            }

            FileStream fileStreamOut = new FileStream(@pathToWrite, FileMode.Create, FileAccess.Write);
            BinaryWriter binaryWriter = new BinaryWriter(fileStreamOut);
            WriteImage(binaryWriter, in fileHeader, in infoHeader, outImage);
            binaryWriter.Close();
            fileStreamOut.Close();
            return 0;
        }

        /// <summary>
        /// Reads, converts to grayscale image and writes.
        /// </summary>
        /// <param name="pathToRead">The path to the file to read.</param>
        /// <param name="pathToWrite">The path to the file to write.</param>
        /// <returns>0 - success, 1 - file not found.</returns>
        public static int ReadGrayscaleAndWriteImage(string pathToRead, string pathToWrite)
        {
            FileStream fileStreamIn;
            try
            {
                fileStreamIn = new FileStream(@pathToRead, FileMode.Open, FileAccess.Read);
            }
            catch (Exception)
            {
                return 1;
            }

            BinaryReader binaryReader = new BinaryReader(fileStreamIn);
            ReadImage(binaryReader, out BitmapFileHeader fileHeader, out BitmapInfoHeader infoHeader, out Pixel[,] image);
            binaryReader.Close();
            fileStreamIn.Close();

            fileHeader.BfSize = 1078 + (infoHeader.BiHeight * infoHeader.BiWidth);
            fileHeader.BfOffBits = 1078;
            infoHeader.BiBitCount = 8;
            infoHeader.BiSizeImage = fileHeader.BfSize;
            infoHeader.BiClrUsed = 256;
            infoHeader.BiClrImportant = 0;

            FileStream fileStreamOut = new FileStream(@pathToWrite, FileMode.Create, FileAccess.Write);
            BinaryWriter binaryWriter = new BinaryWriter(fileStreamOut);

            fileHeader.WriteBitmapFileHeader(binaryWriter);
            infoHeader.WriteBitmapInfoHeader(binaryWriter);

            for (int i = 0; i < 256; i++)
            {
                binaryWriter.Write((byte)i);
                binaryWriter.Write((byte)i);
                binaryWriter.Write((byte)i);
                binaryWriter.Write((byte)0);
            }

            for (int line = 0; line < infoHeader.BiHeight; line++)
            {
                for (int column = 0; column < infoHeader.BiWidth; column++)
                {
                    binaryWriter.Write((byte)Math.Round((0.299 * image[line, column].Red) + (0.587 * image[line, column].Green) + (0.114 * image[line, column].Blue)));
                }
            }

            binaryWriter.Close();
            fileStreamOut.Close();
            return 0;
        }

        private static void ReadImage(BinaryReader binaryReader, out BitmapFileHeader fileHeader, out BitmapInfoHeader infoHeader, out Pixel[,] image)
        {
            fileHeader = default;
            fileHeader.ReadBitmapFileHeader(binaryReader);

            infoHeader = default;
            infoHeader.ReadBitmapInfoHeader(binaryReader);

            image = new Pixel[infoHeader.BiHeight, infoHeader.BiWidth];
            for (int line = 0; line < infoHeader.BiHeight; line++)
            {
                for (int column = 0; column < infoHeader.BiWidth; column++)
                {
                    image[line, column].ReadPixel(binaryReader);

                    if (infoHeader.BiBitCount == 32)
                    {
                        binaryReader.ReadByte();
                    }
                }
            }
        }

        private static void WriteImage(BinaryWriter binaryWriter, in BitmapFileHeader fileHeader, in BitmapInfoHeader infoHeader, Pixel[,] image)
        {
            fileHeader.WriteBitmapFileHeader(binaryWriter);
            infoHeader.WriteBitmapInfoHeader(binaryWriter);

            for (int line = 0; line < infoHeader.BiHeight; line++)
            {
                for (int column = 0; column < infoHeader.BiWidth; column++)
                {
                    image[line, column].WritePixel(binaryWriter);

                    if (infoHeader.BiBitCount == 32)
                    {
                        binaryWriter.Write((byte)0);
                    }
                }
            }
        }
    }
}