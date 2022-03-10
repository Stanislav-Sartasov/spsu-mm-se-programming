using System;
using System.IO;
using System.Linq;

namespace Task_1
{
    public class Image
    {
        public byte[] Header { get; private set; } = new byte[54];
        public byte[][] Pixels { get; private set; }
        public int[] SizesOfImage { get; private set; } = new int[3];       // 0 - biWidht * biBitCount
                                                                            // 1 - biHight
                                                                            // 2 - biBitCount

        public Image(string fileName)
        {
            try
            {
                using(FileStream fileStream = new FileStream(fileName, FileMode.Open))
                {
                    Header = Header
                        .Select(x => (byte)fileStream.ReadByte())
                        .ToArray();
                    int counter = 0;
                    SizesOfImage = SizesOfImage
                        .Select(x => Header
                            .Skip(18 + (counter / 4) * (4 + (counter / 8))) // 18, 22, 28
                            .Take(4 - (counter / 8)) // 4, 4, 2
                            .Select(x => x * (int)Math.Pow(256, counter++ % 4) / (1 + 7 * (counter / 8))) // divides SizesOfImage[2] by 8
                            .Sum())
                        .ToArray();
                    SizesOfImage[0] *= SizesOfImage[2];
                    Pixels = new byte[SizesOfImage[1]][]
                        .Select(x => new byte[SizesOfImage[0]]
                            .Select(y => (byte)fileStream.ReadByte())
                            .ToArray())
                        .ToArray();
                    fileStream.Close();
                }
            }
            catch
            {
                // Header[0] = 0
            }
        }

        public int MakeNewFile(string fileName)
        {
            try
            {
                using(FileStream fileStream = new FileStream(fileName, FileMode.Create))
                {
                    foreach(byte x in Header)
                    {
                        fileStream.WriteByte(x);
                    }
                    foreach (byte[] x in Pixels)
                    {
                        foreach (byte y in x)
                        {
                            fileStream.WriteByte(y);
                        }
                    }
                    fileStream.Close();
                }
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public void GreyFilter()
        {
            int counter = 0;
            Pixels = Pixels
                .Select(x => x
                    .GroupBy(y => counter++ / SizesOfImage[2])
                    .Select(y => y
                        .Select(z => (byte)(0.3 * y.First() + 0.59 * y.Skip(1).First() + 0.11 * y.Skip(2).First())))
                    .SelectMany(y => y)
                    .ToArray())
                .ToArray();
        }

        public void MiddleFilter()
        {
            for (int i = 1; i < SizesOfImage[1] - 1; i++)
            {
                for (int j = SizesOfImage[2]; j < SizesOfImage[0] - SizesOfImage[2]; j += SizesOfImage[2])
                {
                    int counter = 0;
                    byte[] buffer = Pixels
                        .Skip(i - 1)
                        .Take(3)
                        .Select(x => x
                            .Skip(j - SizesOfImage[2])
                            .Take(3 * SizesOfImage[2]))
                        .SelectMany(x => x)
                        .ToArray()
                        .GroupBy(x => counter++ % SizesOfImage[2])
                        .Select(x => x
                            .OrderBy(y => y))
                        .SelectMany(x => x)
                        .ToArray();
                    for (int k = 0; k < 3; k++)
                    {
                        Pixels[i][j + k] = buffer.Skip(4 + 9 * k).First();
                    }
                }
            }
        }

        public void GaussFilter()
        {
            double[] gaussFilter = { 0.307801, 0.384396, 0.307801 };
            for (int i = 1; i < SizesOfImage[1] - 1; i++)
            {
                for (int j = SizesOfImage[2]; j < SizesOfImage[0] - SizesOfImage[2]; j += SizesOfImage[2])
                {
                    int counter = 0;
                    int anotherCounter = 0;
                    byte[] buffer = Pixels
                        .Skip(i - 1)
                        .Take(3)
                        .Select(x => x
                            .Skip(j - SizesOfImage[2])
                            .Take(3 * SizesOfImage[2]))
                        .SelectMany(x => x)
                        .ToArray()
                        .GroupBy(x => anotherCounter++ % SizesOfImage[2])
                        .Select(x => x
                            .Select(y => y * gaussFilter[(counter / 3) % 3] * gaussFilter[counter++ % 3]))
                        .Select(x => x
                            .Select(y => (byte)x.Sum())
                            .ToArray())
                        .SelectMany(x => x)
                        .ToArray();
                    for (int k = 0; k < 3; k++)
                    {
                        Pixels[i][j + k] = buffer.Skip(9 * k).First();
                    }
                }
            }
        }

        public void SobelAxisFilter(int flag) // 1 - xAxis, 0 - yAxis
        {
            GreyFilter();
            const int limit = 73;
            byte[][] bufferPixels = new byte[SizesOfImage[1]][]
                .Select(x => new byte[SizesOfImage[0]])
                .ToArray();
            int[] sobelFilter = { -1, 2 * (flag - 1), -1 + 2 * flag, -2 * flag, 0, 2 * flag, 1 - 2 * flag, 2 - 2 * flag, 1 }; // {-1, 0, 1, -2, 0, 2, -1, 0, 1} for flag == 1, {-1, -2, -1, 0, 0, 0, 1, 2, 1} for == 0
            for (int i = 1; i < SizesOfImage[1] - 1; i++)
            {
                for (int j = SizesOfImage[2]; j < SizesOfImage[0] - SizesOfImage[2]; j++)
                {
                    int counter = 0;
                    byte[] buffer = Pixels
                        .Skip(i - 1)
                        .Take(3)
                        .Select(x => x
                            .Skip(j - SizesOfImage[2])
                            .Take(3 * SizesOfImage[2]))
                        .SelectMany(x => x)
                        .ToArray()
                        .Where((x, y) => y % 3 == 0)
                        .ToArray();
                    bufferPixels[i][j / SizesOfImage[2]] = (byte)(255 * Convert.ToInt32(Math.Abs(buffer.Select(x => x * sobelFilter[counter++ % 9]).Sum()) > limit)); // 255 for > limit and 0 for <=
                }
            }
            int k = 0;
            int l = 0;
            Pixels = Pixels
                .Select(x => x
                    .Select(y => bufferPixels[k++ / SizesOfImage[0]][(l++ % SizesOfImage[0]) / SizesOfImage[2]])
                    .ToArray())
                .ToArray();
        }
    }
}
