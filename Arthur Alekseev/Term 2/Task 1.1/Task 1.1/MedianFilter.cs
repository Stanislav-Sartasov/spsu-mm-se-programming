using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1._1
{
    public class MedianFilter : IFilter
    {
        public void ProcessBitmap(Bitmap bmp)
        {
            int[] pixels_r = new int[9];
            int[] pixels_g = new int[9];
            int[] pixels_b = new int[9];

            Pixel[] copy = new Pixel[bmp.Width * bmp.Height];

            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                    copy[x + y * bmp.Width] = new Pixel(0, 0, 0);
                

            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                {
                    // Collectiong all pixel data
                    for (int i = -1; i < 2; i++)
                        for (int j = -1; j < 2; j++)
                        {
                            pixels_r[i + j * 3 + 4] = bmp.GetPixel(Normalize(x + i, bmp.Width - 1), Normalize(y + j, bmp.Height - 1)).R;
                            pixels_g[i + j * 3 + 4] = bmp.GetPixel(Normalize(x + i, bmp.Width - 1), Normalize(y + j, bmp.Height - 1)).G;
                            pixels_b[i + j * 3 + 4] = bmp.GetPixel(Normalize(x + i, bmp.Width - 1), Normalize(y + j, bmp.Height - 1)).B;
                        }

                    Array.Sort(pixels_r);
                    Array.Sort(pixels_g);
                    Array.Sort(pixels_b);

                    copy[x + y * bmp.Width] = new Pixel(pixels_r[4], pixels_g[4], pixels_b[4]);
                }

            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                    bmp.SetPixel(Normalize(x, bmp.Width - 1), Normalize(y, bmp.Height - 1), copy[x + y * bmp.Width]);
        }
        private int Normalize(int number, int max)
        {
            if (number < 0)
                return 0;
            if (number > max)
                return max;
            return number;
        }
    }
}
