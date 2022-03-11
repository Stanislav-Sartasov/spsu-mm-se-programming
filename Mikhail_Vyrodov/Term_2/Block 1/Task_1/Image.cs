using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task_1
{
    public class Image
    {
        private Argb[][] Pixels;

        private dib_header Dheader;

        private bitmap_header BmHeader;

        private byte[] Garbage;

        public void GetAtrs(ref uint height,ref uint width)
        {
            height = Dheader.Height;
            width = Dheader.Width;
        }

        public void GetArr(Argb[][] arr)
        {
            for(int i = 0; i < Dheader.Height; i++)
            {
                for(int k = 0; k < Dheader.Width; k++)
                {
                    arr[i][k] = new Argb();
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
                    using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, false))
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
                        Pixels = new Argb[Dheader.Height][];
                        for (uint i = 0; i < Dheader.Height; i++)
                        {
                            Pixels[i] = new Argb[Dheader.Width];
                        }
                        for (int i = 0; i < Dheader.Height; i++)
                        {
                            for (int k = 0; k < Dheader.Width; k++)
                            {
                                Pixels[i][k] = new Argb();
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
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
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
            byte redd, greenn, bluee, alphaa, flag = 0;
            Pixels ExtendedPic = new Pixels(Dheader.Height + 2, Dheader.Width + 2);
            Pixels ExtendedPicCopy = new Pixels(Dheader.Height + 2, Dheader.Width + 2);
            for (i = 0; i < Dheader.Height + 2; ++i)
            {
                for (j = 0; j < Dheader.Width + 2; ++j)
                {
                    ExtendedPicCopy.Arr[i][j] = new Argb();
                    ExtendedPic.Arr[i][j] = new Argb();
                }
            }
            for (i = 0; i < Dheader.Height; i++)
            {
                ExtendedPic.Arr[i + 1][0] = Pixels[i][0];
                ExtendedPic.Arr[i + 1][Dheader.Width + 1] = Pixels[i][Dheader.Width - 1];
                for (j = 0; j < Dheader.Width; j++)
                {
                    ExtendedPic.Arr[i + 1][j + 1] = Pixels[i][j];
                }
            }
            for (j = 0; j < Dheader.Width + 2; j++)
            {
                ExtendedPic.Arr[0][j] = ExtendedPic.Arr[1][j];
                ExtendedPic.Arr[Dheader.Height + 1][j] = ExtendedPic.Arr[Dheader.Height][j];
            }
            for (i = 0; i < Dheader.Height + 2; ++i)
            {
                for (j = 0; j < Dheader.Width + 2; ++j)
                {
                    redd = ExtendedPic.Arr[i][j].Red; // I did it because i wanted not reference copy of the array but copy of its args
                    bluee = ExtendedPic.Arr[i][j].Blue;
                    greenn = ExtendedPic.Arr[i][j].Green;
                    alphaa = ExtendedPic.Arr[i][j].Alpha;
                    ExtendedPicCopy.Arr[i][j].Red = redd;
                    ExtendedPicCopy.Arr[i][j].Blue = bluee;
                    ExtendedPicCopy.Arr[i][j].Green = greenn;
                    ExtendedPicCopy.Arr[i][j].Alpha = alphaa;
                } 
            }
            for (i = 1; i <= Dheader.Height; i++)
            {
                for (j = 1; j <= Dheader.Width; j++)
                {
                    if (filter == "Median")
                    {
                        ExtendedPic.MedianFilter(ExtendedPicCopy.Arr, (int)i, (int)j);
                    }
                    else if (filter == "Gauss")
                    {
                        ExtendedPic.GaussFilter3x3(ExtendedPicCopy.Arr, (int)i, (int)j);
                    }
                    else if (filter == "SobelX")
                    {
                        ExtendedPic.SobelX(ExtendedPicCopy.Arr, (int)i, (int)j);
                    }
                    else if (filter == "SobelY")
                    {
                        ExtendedPic.SobelY(ExtendedPicCopy.Arr, (int)i, (int)j);
                    }
                    else if (filter == "GrayScale")
                    {
                        ExtendedPic.Arr[i][j].GrayScale();
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
                    this.Pixels[i - 1][j - 1] = ExtendedPic.Arr[i][j];
                }
            }
            this.WriteImage(outputName);
        }
    }

    struct dib_header
    {
        public uint HeaderSize;
        public uint Width;
        public uint Height;
        public ushort ColorPlanes;
        public ushort BitsPerPixel;
        public uint Compression;
        public uint ImageSize;
        public int[] Palette;
    }

    struct bitmap_header
    {
        public char[] Name;
        public uint Size;
        public int Garbage;
        public uint ImageOffset;
    }
}
