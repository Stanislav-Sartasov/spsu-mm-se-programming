using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    internal abstract class MatrixFilter : IFilter
    {
        private protected double[] matrix;

        public virtual void PixelConverse(ref RGB[,] pixels, int height, int width)
        {
            int length = (int)Math.Sqrt(matrix.Length);
            int x, y, point;
            double result;
            RGB[,] resultPixels = new RGB[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    resultPixels[i, j] = new RGB(new byte[] { 0, 0, 0 });
                }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    for (int p = 0; p < 3; p++)
                    {
                        point = 0;
                        result = 0;
                        for (int a = i - length / 2; a < i + length / 2 + 1; a++)
                        {
                            for (int b = j - length / 2; b < j + length / 2 + 1; b++)
                            {
                                if (b < 0 || a < 0 || b >= width || a >= height)
                                {
                                    x = a;
                                    y = b;
                                    if (x < 0)
                                    {
                                        x = 0;
                                    }
                                    else if (x >= height)
                                    {
                                        x = height - 1;
                                    }
                                    if (y < 0)
                                    {
                                        y = 0;
                                    }
                                    else if (y >= width)
                                    {
                                        y = width - 1;
                                    }
                                    result += matrix[point++] * pixels[x, y].ArrayRGB[p];
                                }
                                else
                                {
                                    result += matrix[point++] * pixels[a, b].ArrayRGB[p];
                                }
                            }
                        }
                        result = Math.Abs(result) > 255 ? 255 : Math.Abs(result);
                        resultPixels[i, j].ArrayRGB[p] = (byte)result;
                    }
                }
            }
            pixels = resultPixels;
        }
    }
}
