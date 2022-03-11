namespace Block1Task1
{
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