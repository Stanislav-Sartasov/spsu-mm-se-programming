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

        public static void ApplySobelFilter(BitMapFile file, Direction type)
        {
            ApplyGreyFilter(file);
            Rgb[,] newPic = new Rgb[file.ImageRgb.GetLength(0), file.ImageRgb.GetLength(1)];
            CopyList(file.ImageRgb, newPic);

            int edge = 80;
            for (int i = 1; i < file.Height - 1; i++)
            {
                for (int j = 1; j < file.CountRowRgb - 1; j++)
                {
                    int sumX = (file.ImageRgb[i + 1, j - 1].Red + 2 * file.ImageRgb[i + 1, j].Red + file.ImageRgb[i + 1, j + 1].Red) -
                        (file.ImageRgb[i - 1, j - 1].Red + 2 * file.ImageRgb[i - 1, j].Red + file.ImageRgb[i - 1, j + 1].Red);
                    int sumY = (file.ImageRgb[i - 1, j + 1].Red + 2 * file.ImageRgb[i, j + 1].Red + file.ImageRgb[i + 1, j + 1].Red) -
                        (file.ImageRgb[i - 1, j - 1].Red + 2 * file.ImageRgb[i, j - 1].Red + file.ImageRgb[i + 1, j - 1].Red);

                    switch ((int)type)
                    {
                        case 0:
                            newPic[i, j].Red = newPic[i, j].Green = newPic[i, j].Blue = Math.Abs(sumX) <= edge ? (byte)0 : (byte)255;
                            break;
                        case 1:
                            newPic[i, j].Red = newPic[i, j].Green = newPic[i, j].Blue = Math.Sqrt(Math.Pow(sumX, 2) + Math.Pow(sumY, 2)) <= edge ? (byte)0 : (byte)255;
                            break;
                        case 2:
                            newPic[i, j].Red = newPic[i, j].Green = newPic[i, j].Blue = Math.Abs(sumY) <= edge ? (byte)0 : (byte)255;
                            break;
                    }
                }
            }
            CopyList(newPic, file.ImageRgb);
        }

        public static void ApplyMiddleFilter(BitMapFile file)
        {
            for (int i = 1; i < file.Height - 1; i++)
            {
                for (int j = 1; j < file.CountRowRgb - 1; j++)
                {
                    byte[] midArrRed = new byte[9], midArrGreen = new byte[9], midArrBlue = new byte[9];
                    int index = 0;
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int p = -1; p <= 1; p++)
                        {
                            midArrRed[index] = file.ImageRgb[i + k, j + p].Red;
                            midArrGreen[index] = file.ImageRgb[i + k, j + p].Green;
                            midArrBlue[index] = file.ImageRgb[i + k, j + p].Blue;
                            index++;
                        }
                    }

                    Array.Sort(midArrRed);
                    Array.Sort(midArrGreen);
                    Array.Sort(midArrBlue);

                    file.ImageRgb[i, j].Red = midArrRed[4];
                    file.ImageRgb[i, j].Green = midArrGreen[4];
                    file.ImageRgb[i, j].Blue = midArrBlue[4];
                }
            }
        }

        public static void ApplyGauss3x3Filter(BitMapFile file)
        {
            int[,] gaussMatrix = { { 1, 2, 1}, { 2, 4, 2}, { 1, 2, 1} };
            for (int i = 1; i < file.Height - 1; i++)
            {
                for (int j = 1; j < file.CountRowRgb - 1; j++)
                {
                    int redKoef = 0, greenKoef = 0, blueKoef = 0;
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int p = -1; p <= 1; p++)
                        {
                            redKoef += file.ImageRgb[i + k, j + p].Red * gaussMatrix[k + 1, p + 1];
                            greenKoef += file.ImageRgb[i + k, j + p].Green * gaussMatrix[k + 1, p + 1];
                            blueKoef += file.ImageRgb[i + k, j + p].Blue * gaussMatrix[k + 1, p + 1];
                        }
                    }
                    file.ImageRgb[i, j].Red = Convert.ToByte(redKoef / 16);
                    file.ImageRgb[i, j].Green = Convert.ToByte(greenKoef / 16);
                    file.ImageRgb[i, j].Blue = Convert.ToByte(blueKoef / 16);
                }
            }
        }

        public enum Direction
        {
            X,
            Both,
            Y
        }
    }
}
