using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    public class Argb
    {
        public byte Blue { get; set; }

        public byte Green { get; set; }

        public byte Red { get; set; }

        public byte Alpha { get; set; }

        public Argb()
        {
            Blue = 0;
            Green = 0;
            Red = 0;
            Alpha = 0;
        }

        public void GrayScale()
        {
            Red = Blue = Green = Alpha = (byte)((0.3 * Red + 0.6 * Green + 0.1 * Blue) / 3);
        }

        public void RedactPixels(Argb[][] pic, string filter, int i, int k)
        {
            int firstCounter, secondCounter, dif;
            int reds = 0, greens = 0, blues = 0, alphas = 0;
            for (firstCounter = i - 1; firstCounter < i + 2; ++firstCounter)
            {
                for (secondCounter = k - 1; secondCounter < k + 2; ++secondCounter)
                {
                    if (filter == "Gauss")
                    {
                        int[] coefficients = new int[3] { 4, 3, 2 };
                        dif = coefficients[Math.Abs(firstCounter - i) + Math.Abs(secondCounter - k)];
                    }
                    else if (filter == "SobelX")
                    {
                        int[][] sobelXCoeff = new int[3][];
                        sobelXCoeff[0] = new int[3] { -1, 0, 1 };
                        sobelXCoeff[1] = new int[3] { -2, 0, 2 };
                        sobelXCoeff[2] = new int[3] { -1, 0, 1 };
                        dif = sobelXCoeff[firstCounter - i + 1][secondCounter - k + 1];
                    }
                    else
                    {
                        int[][] sobelYCoeff = new int[3][];
                        sobelYCoeff[0] = new int[3] { -1, -2, -1 };
                        sobelYCoeff[1] = new int[3] { 0, 0, 0 };
                        sobelYCoeff[2] = new int[3] { 1, 2, 1 };
                        dif = sobelYCoeff[firstCounter - i + 1][secondCounter - k + 1];
                    }
                    reds += pic[firstCounter][secondCounter].Red * dif;
                    blues += pic[firstCounter][secondCounter].Blue * dif;
                    greens += pic[firstCounter][secondCounter].Green * dif;
                    alphas += pic[firstCounter][secondCounter].Alpha * dif;
                }
            }
            if (filter == "Gauss")
            {
                int div = 24;
                this.Red = (byte)(reds / div);
                this.Blue = (byte)(blues / div);
                this.Green = (byte)(greens / div);
                this.Alpha = (byte)(alphas / div);
            }
            else
            {
                this.Red = (byte)(Math.Abs(reds) < 255 ? Math.Abs(reds) : 255);
                this.Green = (byte)(Math.Abs(greens) < 255 ? Math.Abs(greens) : 255);
                this.Blue = (byte)(Math.Abs(blues) < 255 ? Math.Abs(blues) : 255);
                this.Alpha = (byte)(Math.Abs(alphas) < 255 ? Math.Abs(alphas) : 255);
            }
        }
    }
}
