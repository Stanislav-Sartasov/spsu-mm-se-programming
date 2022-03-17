using System;

namespace Task_1
{
    public class Pixels
    {
        public Pixel[][] Arr { get; set; }

        public Pixels(uint height, uint width)
        {
            Arr = new Pixel[height][];
            for (int i = 0; i < height; i++)
            {
                Arr[i] = new Pixel[width];
            }
        }

        public void MedianFilter(Pixel[][] pic, int i, int k)
        {
            byte[] reds = new byte[9];
            byte[] greens = new byte[9];
            byte[] blues = new byte[9];
            byte[] alphas = new byte[9];
            int count = 0;
            for (int firstCounter = i - 1; firstCounter <= i + 1; firstCounter++)
            {
                for (int secondCounter = k - 1; secondCounter <= k + 1; secondCounter++)
                {
                    reds[count] = pic[firstCounter][secondCounter].Red;
                    blues[count] = pic[firstCounter][secondCounter].Blue;
                    greens[count] = pic[firstCounter][secondCounter].Green;
                    alphas[count] = pic[firstCounter][secondCounter].Alpha;
                    count += 1;
                }
            }
            Array.Sort(reds);
            Array.Sort(greens);
            Array.Sort(blues);
            Array.Sort(alphas);
            Arr[i][k].Red = reds[4];
            Arr[i][k].Blue = blues[4];
            Arr[i][k].Green = greens[4];
            Arr[i][k].Alpha = alphas[4];
        }

        public void GaussFilter3x3(Pixel[][] pic, int i, int k)
        {
            string filter = "Gauss";
            Arr[i][k].RedactPixels(pic, filter, i, k);
        }

        public void SobelX(Pixel[][] pic, int i, int k)
        { 
            string filter = "SobelX";
            this.Arr[i][k].RedactPixels(pic, filter, i, k);
            this.Arr[i][k].GrayScale();
        }

        public void SobelY(Pixel[][] pic, int i, int k)
        {
            string filter = "SobelY";
            Arr[i][k].RedactPixels(pic, filter, i, k);
            Arr[i][k].GrayScale();
        }
    }
}