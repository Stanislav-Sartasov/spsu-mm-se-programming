using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task1
{
    public class Image
    {
        private protected class BmpHeader
        {
            public byte[] BfType = new byte[2];
            public int BfSize;
            public int Reserved;
            public int BfOffbits;
            public int BiSize;
            public int BiWidth;
            public int BiHeight;
            public short BiPlanes;
            public short BiBitCount;
            public int BiCompression;
            public int BiSizeImage;
            public int BiXPelsPerMeter;
            public int BiYPelsPerMeter;
            public int BiClrUsed;
            public int BiClrImportant;
            public byte[] Palette;
            public List<byte> ResidualBytes = new List<byte>();

            public bool IsValid() => BfType[0] == 'B' && BfType[1] == 'M' && BiCompression == 0 && (BiBitCount == 24 || BiBitCount == 32);

            public int GetPadding() => (4 - (BiWidth * BiBitCount / 8) % 4) % 4;
        }

        private protected BmpHeader bmpHeader;
        private protected RGB[,] pixels;
        private bool writingSuccess = true;
        private protected bool filterSuccess = true;
        public bool GeneralSuccess => writingSuccess && filterSuccess;

        private protected Image(BmpHeader bmpHeader, RGB[,] pixels)
        {
            this.bmpHeader = bmpHeader;
            this.pixels = pixels;
        }

        public static Image ReadBmp(string? file)
        {
            if (file is null)
            {
                Console.WriteLine("You have not provided the path to the reading file.");
                return EmptyImage.CreateEmptyImage();
            }

            try
            {
                using (BinaryReader fileIn = new BinaryReader(File.Open(file, FileMode.Open)))
                {
                    BmpHeader bmpHeader = new BmpHeader();
                    ReadBmpHeader(fileIn, bmpHeader);
                    if (!bmpHeader.IsValid())
                    {
                        Console.WriteLine("The image must be in bmp format and have a color depth of 32 or 24 bits per pixel and must not be compressed");
                        return EmptyImage.CreateEmptyImage();
                    }
                    ReadPalette(fileIn, bmpHeader);
                    RGB[,] data = new RGB[bmpHeader.BiHeight, bmpHeader.BiWidth];
                    ReadPixels(fileIn, data, bmpHeader);
                    return new Image(bmpHeader, data);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File with path: {file} not found.");
                return EmptyImage.CreateEmptyImage();
            }
            catch (IOException)
            {
                Console.WriteLine("Working with this file is not possible.");
                return EmptyImage.CreateEmptyImage();
            }
        }

        private static void ReadBmpHeader(BinaryReader fileIn, BmpHeader bmpHeader)
        {
            bmpHeader.BfType[0] = fileIn.ReadByte();
            bmpHeader.BfType[1] = fileIn.ReadByte();
            bmpHeader.BfSize = fileIn.ReadInt32();
            bmpHeader.Reserved = fileIn.ReadInt32();
            bmpHeader.BfOffbits = fileIn.ReadInt32();
            bmpHeader.BiSize = fileIn.ReadInt32();
            bmpHeader.BiWidth = fileIn.ReadInt32();
            bmpHeader.BiHeight = fileIn.ReadInt32();
            bmpHeader.BiPlanes = fileIn.ReadInt16();
            bmpHeader.BiBitCount = fileIn.ReadInt16();
            bmpHeader.BiCompression = fileIn.ReadInt32();
            bmpHeader.BiSizeImage = fileIn.ReadInt32();
            bmpHeader.BiXPelsPerMeter = fileIn.ReadInt32();
            bmpHeader.BiYPelsPerMeter = fileIn.ReadInt32();
            bmpHeader.BiClrUsed = fileIn.ReadInt32();
            bmpHeader.BiClrImportant = fileIn.ReadInt32();
        }

        private static void ReadPalette(BinaryReader fileIn, BmpHeader bmpHeader)
        {
            if (bmpHeader.BfOffbits != 54)
            {
                bmpHeader.Palette = new byte[bmpHeader.BfOffbits - 54];
                for (int i = 0; i < bmpHeader.BfOffbits - 54; i++)
                {
                    bmpHeader.Palette[i] = fileIn.ReadByte();
                }
            }
        }

        private static void ReadPixels(BinaryReader fileIn, RGB[,] data, BmpHeader bmpHeader)
        {
            for (int i = 0; i < bmpHeader.BiHeight; i++)
            {
                for (int j = 0; j < bmpHeader.BiWidth; j++)
                {
                    data[i, j] = new RGB(new byte[] { fileIn.ReadByte(), fileIn.ReadByte(), fileIn.ReadByte() });
                    if (bmpHeader.BiBitCount == 32)
                    {
                        fileIn.ReadByte();
                    }
                }
                fileIn.ReadBytes(bmpHeader.GetPadding());
            }

            while (fileIn.PeekChar() > -1)
            {
                bmpHeader.ResidualBytes.Add(fileIn.ReadByte());
            }
        }

        public int? WriteBmp(string? file)
        {
            writingSuccess = true;

            if (file is null || (file.IndexOfAny(Path.GetInvalidFileNameChars()) < 0))
            {
                Console.WriteLine("You have not provided the path to the writing file or path is not correct.");
                writingSuccess = false;
                return null;
            }

            try
            {
                using (BinaryWriter fileOut = new BinaryWriter(File.Open(file, FileMode.Create)))
                {
                    WriteBmpHeader(fileOut);
                    WritePalette(fileOut);
                    WriteBmpPixels(fileOut);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to write image.");
                writingSuccess = false;
                return null;
            }
            return 1;
        }

        private void WriteBmpHeader(BinaryWriter fileOut)
        {
            fileOut.Write(bmpHeader.BfType);
            fileOut.Write(bmpHeader.BfSize);
            fileOut.Write(bmpHeader.Reserved);
            fileOut.Write(bmpHeader.BfOffbits);
            fileOut.Write(bmpHeader.BiSize);
            fileOut.Write(bmpHeader.BiWidth);
            fileOut.Write(bmpHeader.BiHeight);
            fileOut.Write(bmpHeader.BiPlanes);
            fileOut.Write(bmpHeader.BiBitCount);
            fileOut.Write(bmpHeader.BiCompression);
            fileOut.Write(bmpHeader.BiSizeImage);
            fileOut.Write(bmpHeader.BiXPelsPerMeter);
            fileOut.Write(bmpHeader.BiYPelsPerMeter);
            fileOut.Write(bmpHeader.BiClrUsed);
            fileOut.Write(bmpHeader.BiClrImportant);
        }

        private void WritePalette(BinaryWriter fileOut)
        {
            if (bmpHeader.BfOffbits != 54)
            {
                fileOut.Write(bmpHeader.Palette);
            }
        }

        private void WriteBmpPixels(BinaryWriter fileOut)
        {
            byte alpha = 0;
            for (int i = 0; i < bmpHeader.BiHeight; i++)
            {
                for (int j = 0; j < bmpHeader.BiWidth; j++)
                {
                    fileOut.Write(pixels[i, j].ArrayRGB);
                    if (bmpHeader.BiBitCount == 32)
                    {
                        fileOut.Write(alpha);
                    }
                }
                for (int l = 0; l < bmpHeader.GetPadding(); l++)
                {
                    fileOut.Write(alpha);
                }
            }

            foreach (byte item in bmpHeader.ResidualBytes)
            {
                fileOut.Write(item);
            }
        }

        public virtual void ApplyFilter(string? nameFilter)
        {
            filterSuccess = true;

            IFilter? filter = nameFilter switch
            {
                "GrayScale" => new GrayScale(),
                "Median" => new Median(),
                "GaussFive" => new Gauss(),
                "SobelX" => new SobelX(),
                "SobelY" => new SobelY(),
                "Sobel" => new Sobel(),
                _ => null
            };

            if (filter is null)
            {
                Console.WriteLine("The name of the filter is entered incorrectly.");
                filterSuccess = false;
                return;
            }

            filter.PixelConverse(ref pixels, bmpHeader.BiHeight, bmpHeader.BiWidth);
        }
    }
}
