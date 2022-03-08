using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class Image
    {
        protected byte[,] bytes;
        protected int width;
        protected int height;
        protected int bytesInRow;
        protected int bytesPerPixel;
        protected byte[] byteRepresentation; // representation of BMP header in bytes

        public Image(BMPFile fileInfo, FileStream inputFile)
        {
            byteRepresentation = fileInfo.ByteRepresentation;
            width = (int)fileInfo.Width;
            height = (int)fileInfo.Height;
            bytesPerPixel = (int)fileInfo.BitPerPixel / 8;
            bytesInRow = bytesPerPixel * width;

            inputFile.Seek(fileInfo.ImageOffset, SeekOrigin.Begin);
            bytes = new byte[height, bytesInRow];

            for (int i = 0; i < height; i++)
            {
                for (int y = 0; y < bytesInRow; y++)
                {
                    bytes[i, y] = (byte)inputFile.ReadByte();
                }
            }
        }

        public Image(BMPFile fileInfo, byte[] inputFile)
        {
            byteRepresentation = fileInfo.ByteRepresentation;
            width = (int)fileInfo.Width;
            height = (int)fileInfo.Height;
            bytesPerPixel = (int)fileInfo.BitPerPixel / 8;
            bytesInRow = bytesPerPixel * width;
            bytes = new byte[height, bytesInRow];
            int counter = 0;

            for (int i = 0; i < height; i++)
            {
                for (int y = 0; y < bytesInRow; y++)
                {
                    bytes[i, y] = inputFile[fileInfo.ImageOffset + counter];
                    counter ++;
                }
            }
        }

        public void WriteImage(FileStream outputFile)
        {
            outputFile.Write(byteRepresentation);

            for (int i = 0; i < height; i++)
            {
                for (int y = 0; y < bytesInRow; y++)
                {
                    outputFile.WriteByte(bytes[i, y]);
                }
            }

            return;
        }

        public void WriteImage(byte[] outputFile)
        {
            int counter = 0;

            for (int i = 0; i < byteRepresentation.Length; i++)
            {
                outputFile[i] = byteRepresentation[i];
            }
          
            for (int i = 0; i < height; i++)
            {
                for (int y = 0; y < bytesInRow; y++)
                {
                    outputFile[byteRepresentation.Length + counter] = bytes[i, y];
                    counter++;
                }
            }

            return;
        }

        public void Recovery(int offset)
        {
            for (int i = 0; i < offset; i++)
            {
                for (int j = 0; j < bytesInRow; j += bytesPerPixel)
                {
                    for (int rgb = 0; rgb < 3; rgb++)
                    {
                        if (i == (j / bytesPerPixel))
                        {
                            bytes[i, j + rgb] = bytes[2 * offset - i - 1, 2 * offset * bytesPerPixel - j + rgb];
                            bytes[height - 1 - i, j + rgb] = bytes[height - 2 * offset + i, 2 * offset * bytesPerPixel - j + rgb];
                        }
                        else
                        {
                            bytes[i, j + rgb] = bytes[2 * offset - i - 1, j + rgb];
                            bytes[height - 1 - i, j + rgb] = bytes[height - 2 * offset + i, j + rgb];
                        }
                    }
                }
                for (int j = offset; j < height - offset; j++)
                    for (int rgb = 0; rgb < 3; rgb++)
                    {
                        bytes[j, i * bytesPerPixel + rgb] = bytes[j, (2 * offset - 1 - i) * bytesPerPixel + rgb];
                        bytes[j, (bytesInRow - bytesPerPixel) - i * bytesPerPixel + rgb] = bytes[j, bytesInRow + (i - 2 * offset) * bytesPerPixel + rgb];
                    }
            }
        }
    }
}
