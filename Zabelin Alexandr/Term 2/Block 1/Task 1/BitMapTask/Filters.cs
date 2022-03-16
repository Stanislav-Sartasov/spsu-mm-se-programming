using System;

namespace BitMapTask
{
    static public class Filters
    {
        static private void ApplyingMatrix(BitMapImage image, float[,] matrix)
        {
            float r, g, b;
            Pixel[,] filteredImage = new Pixel[image.Width, image.Height];

            for (uint x = 1; x < image.Width - 1; x++)
            {
                for (uint y = 1; y < image.Height - 1; y++)
                {
                    r = 0;
                    g = 0;
                    b = 0;

                    for (uint row = 0; row < 3; row++)
                    {
                        for (uint col = 0; col < 3; col++)
                        {
                            r += image.GetPixel(x + col - 1, y + row - 1).Red * matrix[row, col];
                            g += image.GetPixel(x + col - 1, y + row - 1).Green * matrix[row, col];
                            b += image.GetPixel(x + col - 1, y + row - 1).Blue * matrix[row, col];
                        }
                    }

                    Math.Abs(r);
                    Math.Abs(g);
                    Math.Abs(b);
                    filteredImage[x, y] = new Pixel((byte)r, (byte)g, (byte)b);
                }
            }

            RewriteImage(image, filteredImage);
        }

        static private void RewriteImage(BitMapImage image, Pixel[,] filteredImage)
        {
            for (uint x = 1; x < image.Width - 1; x++)
            {
                for (uint y = 1; y < image.Height - 1; y++)
                {
                    image.SetPixel(x, y, filteredImage[x, y]);
                }
            }
        }

        static public void Median(BitMapImage image)
        {
            int[] r = new int[9], g = new int[9], b = new int[9];
            Pixel[,] filteredImage = new Pixel[image.Width, image.Height];

            for (uint x = 1; x < image.Width - 1; x++)
            {
                for (uint y = 1; y < image.Height - 1; y++)
                {
                    for (uint row = 0; row < 3; row++)
                    {
                        for (uint col = 0; col < 3; col++)
                        {
                            r[3 * row + col] = image.GetPixel(x + col - 1, y + row - 1).Red;
                            g[3 * row + col] = image.GetPixel(x + col - 1, y + row - 1).Green;
                            b[3 * row + col] = image.GetPixel(x + col - 1, y + row - 1).Blue;
                        }
                    }

                    Array.Sort(r);
                    Array.Sort(g);
                    Array.Sort(b);
                    filteredImage[x, y] = new Pixel((byte)r[4], (byte)g[4], (byte)b[4]);
                }
            }

            RewriteImage(image, filteredImage);
        }

        static public void Gauss(BitMapImage image)
        {
            float[,] matrix = new float[3, 3] {
                {1, 2, 1},
                {2, 4, 2},
                {1, 2, 1}
            };
            // Transforming float matrix

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i, j] = matrix[i, j] / 16.0f;
                }
            }

            // -------------------------

            ApplyingMatrix(image, matrix);
        }

        static public void SobelX(BitMapImage image)
        {
            float[,] matrix = new float[3, 3] {
                {1, 0, -1},
                {2, 0, -2},
                {1, 0, -1}
            };

            ApplyingMatrix(image, matrix);
        }

        static public void SobelY(BitMapImage image)
        {
            float[,] matrix = new float[3, 3] {
                {1, 2, 1},
                {0, 0, 0},
                {-1, -2, -1}
            };

            ApplyingMatrix(image, matrix);
        }

        static public void Gray(BitMapImage image)
        {
            int mean;
            Pixel pixel;

            for (uint x = 0; x < image.Width; x++)
            {
                for (uint y = 0; y < image.Height; y++)
                {
                    pixel = image.GetPixel(x, y);
                    mean = (pixel.Red + pixel.Green + pixel.Blue) / 3;

                    pixel.Red = (byte)mean;
                    pixel.Green = (byte)mean;
                    pixel.Blue = (byte)mean;

                    image.SetPixel(x, y, pixel);
                }
            }
        }
    }
}