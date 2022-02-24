using System;
using System.IO;
using System.Linq;

namespace Task_1
{
    public class Image
    {
        public byte[] header = new byte[54];
        public byte[][] image;
        public int[] sizesOfImage = new int[3]; // 0 - biWidht * biBitCount
                                                // 1 - biHight
                                                // 2 - biBitCount

        public Image(string fileName)
        {
            try
            {
                using(FileStream fileStream = new FileStream(fileName, FileMode.Open))
                {
                    header = header
                        .Select(x => (byte)fileStream.ReadByte())
                        .ToArray();
                    int counter = 0;
                    sizesOfImage = sizesOfImage
                        .Select(x => header
                            .Skip(18 + (counter / 4) * (4 + (counter / 8))) // 18, 22, 28
                            .Take(4 - (counter / 8)) // 4, 4, 2
                            .Select(x => x * (int)Math.Pow(256, counter++ % 4) / (1 + 7 * (counter / 8))) // divides sizesOfImage[2] by 8
                            .Sum())
                        .ToArray();
                    sizesOfImage[0] *= sizesOfImage[2];
                    image = new byte[sizesOfImage[1]][]
                        .Select(x => new byte[sizesOfImage[0]]
                            .Select(y => (byte)fileStream.ReadByte())
                            .ToArray())
                        .ToArray();
                    fileStream.Close();
                }
            }
            catch
            {
                // header[0] = 0
            }
        }

        public int MakeNewFile(string fileName)
        {
            try
            {
                using(FileStream fileStream = new FileStream(fileName, FileMode.Create))
                {
                    foreach(byte x in header)
                    {
                        fileStream.WriteByte(x);
                    }
                    foreach (byte[] x in image)
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
            image = image
                .Select(x => x
                    .GroupBy(y => counter++ / sizesOfImage[2])
                    .Select(y => y
                        .Select(z => (byte)(0.3 * y.First() + 0.59 * y.Skip(1).First() + 0.11 * y.Skip(2).First())))
                    .SelectMany(y => y)
                    .ToArray())
                .ToArray();
        }

        public void MiddleFilter()
        {
            for (int i = 1; i < sizesOfImage[1] - 1; i++)
            {
                for (int j = sizesOfImage[2]; j < sizesOfImage[0] - sizesOfImage[2]; j += sizesOfImage[2])
                {
                    int counter = 0;
                    byte[] buffer = image
                        .Skip(i - 1)
                        .Take(3)
                        .Select(x => x
                            .Skip(j - sizesOfImage[2])
                            .Take(3 * sizesOfImage[2]))
                        .SelectMany(x => x)
                        .ToArray()
                        .GroupBy(x => counter++ % sizesOfImage[2])
                        .Select(x => x
                            .OrderBy(y => y))
                        .SelectMany(x => x)
                        .ToArray();
                    for (int k = 0; k < 3; k++)
                    {
                        image[i][j + k] = buffer.Skip(4 + 9 * k).First();
                    }
                }
            }
        }

        public void GaussFilter()
        {
            double[] gaussFilter = { 0.307801, 0.384396, 0.307801 };
            for (int i = 1; i < sizesOfImage[1] - 1; i++)
            {
                for (int j = sizesOfImage[2]; j < sizesOfImage[0] - sizesOfImage[2]; j += sizesOfImage[2])
                {
                    int counter = 0;
                    int anotherCounter = 0;
                    byte[] buffer = image
                        .Skip(i - 1)
                        .Take(3)
                        .Select(x => x
                            .Skip(j - sizesOfImage[2])
                            .Take(3 * sizesOfImage[2]))
                        .SelectMany(x => x)
                        .ToArray()
                        .GroupBy(x => anotherCounter++ % sizesOfImage[2])
                        .Select(x => x
                            .Select(y => y * gaussFilter[(counter / 3) % 3] * gaussFilter[counter++ % 3]))
                        .Select(x => x
                            .Select(y => (byte)x.Sum())
                            .ToArray())
                        .SelectMany(x => x)
                        .ToArray();
                    for (int k = 0; k < 3; k++)
                    {
                        image[i][j + k] = buffer.Skip(9 * k).First();
                    }
                }
            }
        }

        public void SobelAxisFilter(int flag) // 1 - xAxis, 0 - yAxis
        {
            GreyFilter();
            const int limit = 73;
            byte[][] bufferImage = new byte[sizesOfImage[1]][]
                .Select(x => new byte[sizesOfImage[0]])
                .ToArray();
            int[] sobelFilter = { -1, 2 * (flag - 1), -1 + 2 * flag, -2 * flag, 0, 2 * flag, 1 - 2 * flag, 2 - 2 * flag, 1 }; // {-1, 0, 1, -2, 0, 2, -1, 0, 1} for flag == 1, {-1, -2, -1, 0, 0, 0, 1, 2, 1} for == 0
            for (int i = 1; i < sizesOfImage[1] - 1; i++)
            {
                for (int j = sizesOfImage[2]; j < sizesOfImage[0] - sizesOfImage[2]; j++)
                {
                    int counter = 0;
                    byte[] buffer = image
                        .Skip(i - 1)
                        .Take(3)
                        .Select(x => x
                            .Skip(j - sizesOfImage[2])
                            .Take(3 * sizesOfImage[2]))
                        .SelectMany(x => x)
                        .ToArray()
                        .Where((x, y) => y % 3 == 0)
                        .ToArray();
                    bufferImage[i][j / sizesOfImage[2]] = (byte)(255 * Convert.ToInt32(Math.Abs(buffer.Select(x => x * sobelFilter[counter++ % 9]).Sum()) > limit)); // 255 for > limit and 0 for <=
                }
            }
            int k = 0;
            int l = 0;
            image = image
                .Select(x => x
                    .Select(y => bufferImage[k++ / sizesOfImage[0]][(l++ % sizesOfImage[0]) / sizesOfImage[2]])
                    .ToArray())
                .ToArray();
        }
    }
}
