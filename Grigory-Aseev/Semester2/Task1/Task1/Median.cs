using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    internal class Median : IFilter
    {
        public void PixelConverse(ref RGB[,] pixels, int height, int width)
        {
            int[] matrix = new int[9];
            int point, x, y;

            for (int i = 0; i + 2 < height; i += 3)
            {
                for (int j = 0; j + 2 < width; j += 3)
                {
                    for (int p = 0; p < 3; p++)
                    {
                        point = 0;
                        for (int a = i - 1; a < i + 2; a++)
                        {
                            for (int b = j - 1; b < j + 2; b++)
                            {
                                if (b < 0 || a < 0 || b == width || a == height)
                                {
                                    x = a;
                                    y = b;
                                    if (x < 0)
                                    {
                                        x++;
                                    }
                                    else if (x == height)
                                    {
                                        x--;
                                    }
                                    if (y < 0)
                                    {
                                        y++;
                                    }
                                    else if (y == width)
                                    {
                                        y--;
                                    }
                                    matrix[point++] = pixels[x, y].ArrayRGB[p];
                                }
                                else
                                {
                                    matrix[point++] = pixels[i, j].ArrayRGB[p];
                                }
                            }
                        }

                        Array.Sort(matrix);

                        for (int a = i - 1; a < i + 2; a++)
                        {
                            for (int b = j - 1; b < j + 2; b++)
                            {
                                if (a < 0 || b < 0)
                                {
                                    continue;
                                }
                                pixels[a, b].ArrayRGB[p] = (byte)matrix[4];
                            }
                        }
                    }
                }
            }
        }
    }
}
