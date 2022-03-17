using System;
using System.IO;

namespace Task_1
{
    public class Image
    {
        private Pixel[][] Pixels;

        private DibHeader Dheader;

        private BitmapHeader BmHeader;

        private byte[] Garbage;

        public void GetAtrs(ref uint height,ref uint width)
        {
            height = Dheader.Height;
            width = Dheader.Width;
        }

        public void GetArr(Pixel[][] arr)
        {
            for(int i = 0; i < Dheader.Height; i++)
            {
                for(int k = 0; k < Dheader.Width; k++)
                {
                    arr[i][k] = new Pixel();
                    arr[i][k] = Pixels[i][k];
                }
            }
        }

        public byte ReadImage(string fileName)
        {
            if (File.Exists(fileName))
            {
                using (var stream = File.Open(fileName, FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        BmHeader.Name = reader.ReadChars(2);
                        BmHeader.Size = reader.ReadUInt32();
                        BmHeader.Garbage = reader.ReadInt32();
                        BmHeader.ImageOffset = reader.ReadUInt32();
                        Dheader.HeaderSize = reader.ReadUInt32();
                        Dheader.Width = reader.ReadUInt32();
                        Dheader.Height = reader.ReadUInt32();
                        Dheader.ColorPlanes = reader.ReadUInt16();
                        Dheader.BitsPerPixel = reader.ReadUInt16();
                        Dheader.Compression = reader.ReadUInt32();
                        Dheader.ImageSize = reader.ReadUInt32();
                        Dheader.Palette = new int[4];
                        for (int i = 0; i < 4; i++)
                        {
                            Dheader.Palette[i] = reader.ReadInt32();
                        }
                        stream.Position = BmHeader.ImageOffset;
                        Pixels = new Pixel[Dheader.Height][];
                        for (uint i = 0; i < Dheader.Height; i++)
                        {
                            Pixels[i] = new Pixel[Dheader.Width];
                        }
                        for (int i = 0; i < Dheader.Height; i++)
                        {
                            for (int k = 0; k < Dheader.Width; k++)
                            {
                                Pixels[i][k] = new Pixel();
                            }
                        }
                        if (Dheader.BitsPerPixel == 32)
                        {
                            for (int i = (int)Dheader.Height - 1; i >= 0; i--)
                            {
                                for (int k = 0; k < Dheader.Width; k++)
                                {
                                    Pixels[i][k].Blue = reader.ReadByte();
                                    Pixels[i][k].Green = reader.ReadByte();
                                    Pixels[i][k].Red = reader.ReadByte();
                                    Pixels[i][k].Alpha = reader.ReadByte();
                                }
                            }
                        }
                        else
                        {
                            Garbage = new byte[((24 * Dheader.Width + 31) / 32) * 4 - Dheader.Width * 3];
                            for (int i = (int)Dheader.Height - 1; i >= 0; i--)
                            {
                                for (int k = 0; k < Dheader.Width; k++)
                                {
                                    Pixels[i][k].Blue = reader.ReadByte();
                                    Pixels[i][k].Green = reader.ReadByte();
                                    Pixels[i][k].Red = reader.ReadByte();
                                    Pixels[i][k].Alpha = 0;
                                }
                                Garbage = reader.ReadBytes((int)(((24 * Dheader.Width + 31) / 32) * 4 - Dheader.Width * 3));
                            }
                        }
                        Console.WriteLine("Image was successfuly read");
                        return 0;
                    }
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist");
                return 1;
            }
        }

        public void WriteImage(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Create))
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(BmHeader.Name);
                    writer.Write(BmHeader.Size);
                    writer.Write(BmHeader.Garbage);
                    writer.Write(BmHeader.ImageOffset);
                    writer.Write(Dheader.HeaderSize);
                    writer.Write(Dheader.Width);
                    writer.Write(Dheader.Height);
                    writer.Write(Dheader.ColorPlanes);
                    writer.Write(Dheader.BitsPerPixel);
                    writer.Write(Dheader.Compression);
                    writer.Write(Dheader.ImageSize);
                    for (int i = 0; i < 4; i++)
                    {
                        writer.Write(Dheader.Palette[i]);
                    }
                    if (Dheader.BitsPerPixel == 32)
                    {
                        for (int i = (int)Dheader.Height - 1; i >= 0; i--)
                        {
                            for (int k = 0; k < Dheader.Width; k++)
                            {
                                writer.Write(Pixels[i][k].Blue);
                                writer.Write(Pixels[i][k].Green);
                                writer.Write(Pixels[i][k].Red);
                                writer.Write(Pixels[i][k].Alpha);
                            }
                        }
                    }
                    else
                    {
                        Garbage = new byte[((24 * Dheader.Width + 31) / 32) * 4 - Dheader.Width * 3];
                        for (int i = (int)Dheader.Height - 1; i >= 0; i--)
                        {
                            for (int k = 0; k < Dheader.Width; k++)
                            {
                                writer.Write(Pixels[i][k].Blue);
                                writer.Write(Pixels[i][k].Green);
                                writer.Write(Pixels[i][k].Red);
                                Pixels[i][k].Alpha = 0;
                            }
                            writer.Write(Garbage);
                        }
                    }
                    Console.WriteLine("Image was successfuly written");
                }
            }
        }

        public void ApplyFilters(string outputName, string filter)
        {
            uint i, j;
            byte redCopy, greenCopy, blueCopy, alphaCopy, flag = 0;
            Pixels extendedPic = new Pixels(Dheader.Height + 2, Dheader.Width + 2);
            Pixels extendedPicCopy = new Pixels(Dheader.Height + 2, Dheader.Width + 2);
            for (i = 0; i < Dheader.Height + 2; ++i)
            {
                for (j = 0; j < Dheader.Width + 2; ++j)
                {
                    extendedPicCopy.Arr[i][j] = new Pixel();
                    extendedPic.Arr[i][j] = new Pixel();
                }
            }
            for (i = 0; i < Dheader.Height; i++)
            {
                extendedPic.Arr[i + 1][0] = Pixels[i][0];
                extendedPic.Arr[i + 1][Dheader.Width + 1] = Pixels[i][Dheader.Width - 1];
                for (j = 0; j < Dheader.Width; j++)
                {
                    extendedPic.Arr[i + 1][j + 1] = Pixels[i][j];
                }
            }
            for (j = 0; j < Dheader.Width + 2; j++)
            {
                extendedPic.Arr[0][j] = extendedPic.Arr[1][j];
                extendedPic.Arr[Dheader.Height + 1][j] = extendedPic.Arr[Dheader.Height][j];
            }
            for (i = 0; i < Dheader.Height + 2; ++i)
            {
                for (j = 0; j < Dheader.Width + 2; ++j)
                {
                    redCopy = extendedPic.Arr[i][j].Red; // I did it because i wanted not reference copy of the array but copy of its args
                    blueCopy = extendedPic.Arr[i][j].Blue;
                    greenCopy = extendedPic.Arr[i][j].Green;
                    alphaCopy = extendedPic.Arr[i][j].Alpha;
                    extendedPicCopy.Arr[i][j].Red = redCopy;
                    extendedPicCopy.Arr[i][j].Blue = blueCopy;
                    extendedPicCopy.Arr[i][j].Green = greenCopy;
                    extendedPicCopy.Arr[i][j].Alpha = alphaCopy;
                } 
            }
            for (i = 1; i <= Dheader.Height; i++)
            {
                for (j = 1; j <= Dheader.Width; j++)
                {
                    if (filter == "Median")
                    {
                        extendedPic.MedianFilter(extendedPicCopy.Arr, (int)i, (int)j);
                    }
                    else if (filter == "Gauss")
                    {
                        extendedPic.GaussFilter3x3(extendedPicCopy.Arr, (int)i, (int)j);
                    }
                    else if (filter == "SobelX")
                    {
                        extendedPic.SobelX(extendedPicCopy.Arr, (int)i, (int)j);
                    }
                    else if (filter == "SobelY")
                    {
                        extendedPic.SobelY(extendedPicCopy.Arr, (int)i, (int)j);
                    }
                    else if (filter == "GrayScale")
                    {
                        extendedPic.Arr[i][j].GrayScale();
                    }
                    else
                    {
                        Console.WriteLine("Incorrect name of filter");
                        flag = 1;
                        break;
                    }
                }
                if (flag == 1)
                {
                    break;
                }
            }
            for (i = 1; i < Dheader.Height + 1; ++i)
            {
                for (j = 1; j < Dheader.Width + 1; ++j)
                {
                    this.Pixels[i - 1][j - 1] = extendedPic.Arr[i][j];
                }
            }
            this.WriteImage(outputName);
        }
    }
}
