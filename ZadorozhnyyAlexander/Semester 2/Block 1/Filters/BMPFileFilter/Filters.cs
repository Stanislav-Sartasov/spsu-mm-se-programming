using System;

namespace BMPFileFilter
{
    public class Filters
    {
        private static byte GreyScale(Rgb pixel)
        {
            return Convert.ToByte((0.3 * pixel.Blue) + (0.6 * pixel.Green) + (0.1 * pixel.Red));
        }

        public static void ApplyGreyFilter(BitMapFile file)
        {
            for (int i = 0; i < file.Height; i++)
            {
                for (int j = 0; j < file.CountRowRgb; j++)
                {
                    file.ImageRgb[i, j].Red = file.ImageRgb[i, j].Green = file.ImageRgb[i, j].Blue = GreyScale(file.ImageRgb[i, j]);
                }
            }
        }

        private static void CopyList(Rgb[,] first, Rgb[,] second)
        {
            for (int i = 0; i < first.GetLength(0); i++)
            {
                for (int j = 0; j < first.GetLength(1); j++)
                {
                    second[i, j] = new Rgb(first[i, j].GetListBytes());
                }
            }
        }

        public static void ApplySobelFilter(BitMapFile file, string direction)
        {
            ApplyGreyFilter(file);
            Rgb[,] new_pic = new Rgb[file.ImageRgb.GetLength(0), file.ImageRgb.GetLength(1)];
            CopyList(file.ImageRgb, new_pic);

            int edge = 80;
            for (int i = 1; i < file.Height - 1; i++)
            {
                for (int j = 1; j < file.CountRowRgb - 1; j++)
                {
                    int sum_x = (file.ImageRgb[i + 1, j - 1].Red + 2 * file.ImageRgb[i + 1, j].Red + file.ImageRgb[i + 1, j + 1].Red) -
                        (file.ImageRgb[i - 1, j - 1].Red + 2 * file.ImageRgb[i - 1, j].Red + file.ImageRgb[i - 1, j + 1].Red);
                    int sum_y = (file.ImageRgb[i - 1, j + 1].Red + 2 * file.ImageRgb[i, j + 1].Red + file.ImageRgb[i + 1, j + 1].Red) -
                        (file.ImageRgb[i - 1, j - 1].Red + 2 * file.ImageRgb[i, j - 1].Red + file.ImageRgb[i + 1, j - 1].Red);

                    switch (direction)
                    {
                        case "X":
                            new_pic[i, j].Red = new_pic[i, j].Green = new_pic[i, j].Blue = Math.Abs(sum_x) <= edge ? (byte)0 : (byte)255;
                            break;
                        case "Both":
                            new_pic[i, j].Red = new_pic[i, j].Green = new_pic[i, j].Blue = Math.Sqrt(Math.Pow(sum_x, 2) + Math.Pow(sum_y, 2)) <= edge ? (byte)0 : (byte)255;
                            break;
                        case "Y":
                            new_pic[i, j].Red = new_pic[i, j].Green = new_pic[i, j].Blue = Math.Abs(sum_y) <= edge ? (byte)0 : (byte)255;
                            break;
                    }
                }
            }
            CopyList(new_pic, file.ImageRgb);
        }

        public static void ApplyMiddleFilter(BitMapFile file)
        {
            for (int i = 1; i < file.Height - 1; i++)
            {
                for (int j = 1; j < file.CountRowRgb - 1; j++)
                {
                    byte[] mid_arr_Red = new byte[9], mid_arr_Green = new byte[9], mid_arr_Blue = new byte[9];
                    int index = 0;
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int p = -1; p <= 1; p++)
                        {
                            mid_arr_Red[index] = file.ImageRgb[i + k, j + p].Red;
                            mid_arr_Green[index] = file.ImageRgb[i + k, j + p].Green;
                            mid_arr_Blue[index] = file.ImageRgb[i + k, j + p].Blue;
                            index++;
                        }
                    }

                    Array.Sort(mid_arr_Red);
                    Array.Sort(mid_arr_Green);
                    Array.Sort(mid_arr_Blue);

                    file.ImageRgb[i, j].Red = mid_arr_Red[4];
                    file.ImageRgb[i, j].Green = mid_arr_Green[4];
                    file.ImageRgb[i, j].Blue = mid_arr_Blue[4];
                }
            }
        }

        public static void ApplyGauss3x3Filter(BitMapFile file)
        {
            int[,] g = { { 1, 2, 1}, { 2, 4, 2}, { 1, 2, 1} };
            for (int i = 1; i < file.Height - 1; i++)
            {
                for (int j = 1; j < file.CountRowRgb - 1; j++)
                {
                    int r_k = 0, g_k = 0, b_k = 0;
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int p = -1; p <= 1; p++)
                        {
                            r_k += file.ImageRgb[i + k, j + p].Red * g[k + 1, p + 1];
                            g_k += file.ImageRgb[i + k, j + p].Green * g[k + 1, p + 1];
                            b_k += file.ImageRgb[i + k, j + p].Blue * g[k + 1, p + 1];
                        }
                    }
                    file.ImageRgb[i, j].Red = Convert.ToByte(r_k / 16);
                    file.ImageRgb[i, j].Green = Convert.ToByte(g_k / 16);
                    file.ImageRgb[i, j].Blue = Convert.ToByte(b_k / 16);
                }
            }
        }
    }
}
