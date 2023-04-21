using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    internal class GrayScale : IFilter
    {
        public void PixelConverse(ref RGB[,] pixels, int height, int width)
        {
            byte gray;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    gray = (byte)(pixels[i, j].ArrayRGB[0] * 0.114 + pixels[i, j].ArrayRGB[1] * 0.587 + pixels[i, j].ArrayRGB[2] * 0.229);
                    pixels[i, j].ArrayRGB[0] = pixels[i, j].ArrayRGB[1] = pixels[i, j].ArrayRGB[2] = gray;
                }
            }
        }
    }
}
