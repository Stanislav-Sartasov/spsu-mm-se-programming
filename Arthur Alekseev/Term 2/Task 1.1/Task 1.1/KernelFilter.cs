using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1._1
{
    public abstract class KernelFilter : IFilter
    {
        protected int[] kernel;
        protected int divisor;
        public void ProcessBitmap(Bitmap bmp)
        {
            Pixel[] copy = new Pixel[bmp.Width * bmp.Height];

            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                {
                    copy[x + y * bmp.Width] = new Pixel(0, 0, 0);
                }

            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                {
                    // Collectiong all pixel data
                    for (int i = -1; i < 2; i++)
                        for (int j = -1; j < 2; j++)                            
                            copy[x + y * bmp.Width] += bmp.GetPixel(Normalize(x + i, bmp.Width - 1), Normalize(y + j, bmp.Height - 1)) * kernel[i + 3 * j + 4];

                    copy[x + y * bmp.Width] = copy[x + y * bmp.Width].Absolute() / divisor;
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
