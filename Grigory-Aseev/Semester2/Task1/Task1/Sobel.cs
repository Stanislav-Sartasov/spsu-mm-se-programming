using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    internal class Sobel : MatrixFilter
    {
        public override void PixelConverse(ref RGB[,] pixels, int height, int width)
        {
            RGB[,] pixelsX = new RGB[height, width];

            Array.Copy(pixels, pixelsX, height * width);

            new SobelX().PixelConverse(ref pixelsX, height, width);
            new SobelY().PixelConverse(ref pixels, height, width);

            double mag;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    for (int p = 0; p < 3; p++)
                    {
                        mag = Math.Ceiling(Math.Sqrt(pixelsX[i, j].ArrayRGB[p] * pixelsX[i, j].ArrayRGB[p] + pixels[i, j].ArrayRGB[p] * pixels[i, j].ArrayRGB[p]));
                        mag = Math.Abs(mag) > 255 ? 255 : Math.Abs(mag);
                        pixelsX[i, j].ArrayRGB[p] = (byte)mag;
                    }
                }
            }
            pixels = pixelsX;
        }
    }
}