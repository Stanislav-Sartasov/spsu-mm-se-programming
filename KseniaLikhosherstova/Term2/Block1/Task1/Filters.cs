namespace Task1
{
    public class Filters
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp;
            temp = a;
            a = b;
            b = temp;
        }

        public void Median(ref BitmapFileHeader bMPFH, ref BitmapInfoHeader bMPIH, Pixel[,] mas)
        {
            Pixel[] buf = new Pixel[9];

            for (int i = 2; i < bMPIH.Hight + 2; ++i)
            {
                for (int j = 2; j < bMPIH.Width + 2; ++j)
                {
                    int k = 0;
                    for (int h = i - 1; (h < h + 1) && (k < 9); ++h)
                    {
                        for (int j1 = j - 1; j1 <= j + 1; ++j1, ++k)
                        {
                            buf[k] = mas[h, j1];
                        }
                    }

                    for (k = 0; k < 9; ++k)
                    {
                        for (int l = 0; l < 9; ++l)
                        {
                            if ((buf[k].Red * 0.212 + buf[k].Green * 0.715 + buf[k].Blue * 0.072) <
                                (buf[l].Red * 0.212 + buf[l].Green * 0.715 + buf[l].Blue * 0.072))
                            {
                                Swap<Pixel>(ref buf[k], ref buf[l]);
                            }
                        }
                    }

                    mas[i, j] = buf[4];
                }
            }
        }

        public void Mask(ref BitmapFileHeader bMPFH, ref BitmapInfoHeader bMPIH, Pixel[,] mas, int kF, int[] apl)
        {

            int k = 0, green = 0, red = 0, blue = 0;

            for (int i = 2; i < bMPIH.Hight + 2; ++i)
            {
                for (int j = 2; j < bMPIH.Width + 2; ++j)
                {
                    k = 0; green = 0; red = 0; blue = 0;

                    for (int h = i - 1; (h < h + 1) && (k < 9); ++h)
                    {
                        for (int l = j - 1; l <= j + 1; ++l, ++k)
                        {
                            green += mas[h, l].Green * apl[k] / kF;
                            blue += mas[h, l].Blue * apl[k] / kF;
                            red += mas[h, l].Red * apl[k] / kF;
                        }
                    }

                    if (kF == 16)
                    {
                        mas[i, j].Blue = (byte)blue;
                        mas[i, j].Red = (byte)red;
                        mas[i, j].Green = (byte)green;
                    }

                    else
                    {
                        mas[i, j].Blue = (byte)Math.Min(Math.Abs(blue), 255);
                        mas[i, j].Red = (byte)Math.Min(Math.Abs(red), 255);
                        mas[i, j].Green = (byte)Math.Min(Math.Abs(green), 255);
                    }
                }
            }

        }

        public void Gray(ref BitmapFileHeader bMPFH, ref BitmapInfoHeader bMPIH, Pixel[,] mas)
        {
            byte average = 0;

            for (int i = 0; i < bMPIH.Hight + 4; ++i)
            {
                for (int j = 0; j < bMPIH.Width + 4; ++j)
                {
                    average = (byte)((mas[i, j].Red + mas[i, j].Green + mas[i, j].Blue) / 3);
                    mas[i, j].Red = average;
                    mas[i, j].Green = average;
                    mas[i, j].Blue = average;
                }
            }
        }

        public void Gauss(ref BitmapFileHeader bMPFH, ref BitmapInfoHeader bMPIH, Pixel[,] mas)
        {

            int[] apl = { 1, 2, 1, 2, 4, 2, 1, 2, 1 };

            Mask(ref bMPFH, ref bMPIH, mas, 16, apl);

        }

        public void SobelX(ref BitmapFileHeader bMPFH, ref BitmapInfoHeader bMPIH, Pixel[,] mas)
        {

            Gray(ref bMPFH, ref bMPIH, mas);

            int[] apl = { -1, 0, 1, -2, 0, 2, -1, 0, 1 };

            Mask(ref bMPFH, ref bMPIH, mas, 1, apl);
        }

        public void SobelY(ref BitmapFileHeader bMPFH, ref BitmapInfoHeader bMPIH, Pixel[,] mas)
        {

            Gray(ref bMPFH, ref bMPIH, mas);

            int[] apl = { -1, -2, -1, 0, 0, 0, 1, 2, 1 };

            Mask(ref bMPFH, ref bMPIH, mas, 1, apl);
        }
    }
}