namespace Task1
{
    public class FileOperations
    {

        public void Rat(ref BitmapFileHeader bMPFH, ref BitmapInfoHeader bMPIH, Pixel[,] mas)
        {
            mas[0, 0] = mas[2, 2];
            mas[1, 1] = mas[2, 2];
            mas[0, bMPIH.Width + 3] = mas[2, bMPIH.Width + 1];
            mas[1, bMPIH.Width + 2] = mas[2, bMPIH.Width + 1];
            mas[bMPIH.Hight + 3, 0] = mas[bMPIH.Hight + 1, 2];
            mas[bMPIH.Hight + 2, 1] = mas[bMPIH.Hight + 1, 2];
            mas[bMPIH.Hight + 3, bMPIH.Width + 3] = mas[bMPIH.Hight + 1, bMPIH.Width + 1];
            mas[bMPIH.Hight + 2, bMPIH.Width + 2] = mas[bMPIH.Hight + 1, bMPIH.Width + 1];

            for (int i = 2; i < bMPIH.Hight + 2; ++i)
            {
                mas[i, 1] = mas[i, 2];
            }

            for (int i = 1; i < bMPIH.Hight + 3; ++i)
            {
                mas[i, 0] = mas[i, 2];
            }

            for (int i = 2; i < bMPIH.Hight + 2; ++i)
            {
                mas[i, bMPIH.Width + 2] = mas[i, bMPIH.Width + 1];
            }

            for (int i = 1; i < bMPIH.Hight + 3; ++i)
            {
                mas[i, bMPIH.Width + 3] = mas[i, bMPIH.Width + 1];
            }

            for (int j = 2; j < bMPIH.Width + 2; ++j)
            {
                mas[1, j] = mas[2, j];
            }

            for (int j = 1; j < bMPIH.Width + 3; ++j)
            {
                mas[0, j] = mas[2, j];
            }

            for (int j = 2; j < bMPIH.Width + 2; ++j)
            {
                mas[bMPIH.Hight + 2, j] = mas[bMPIH.Hight + 1, j];
            }

            for (int j = 1; j < bMPIH.Width + 3; ++j)
            {
                mas[bMPIH.Hight + 3, j] = mas[bMPIH.Hight + 1, j];
            }

        }

        public void ReadFile(ref BitmapFileHeader bMPFH, ref BitmapInfoHeader bMPIH, ref BinaryReader bR)
        {
            bMPFH.TypeFile = bR.ReadByte();
            bMPFH.TypeSize = bR.ReadByte();
            bMPFH.SizeFile = bR.ReadUInt32();
            bMPFH.ReservedOne = bR.ReadUInt16();
            bMPFH.ReservedTwo = bR.ReadUInt16();
            bMPFH.OffsetBits = bR.ReadUInt32();

            bMPIH.Size = bR.ReadUInt32();
            bMPIH.Width = bR.ReadUInt32();
            bMPIH.Hight = bR.ReadUInt32();
            bMPIH.Planes = bR.ReadUInt16();
            bMPIH.BitCount = bR.ReadUInt16();
            bMPIH.Compression = bR.ReadUInt16();
            bMPIH.SizeImage = bR.ReadUInt32();
            bMPIH.XPelsPerMeter = bR.ReadUInt32();
            bMPIH.YPelsPerMeter = bR.ReadUInt32();
            bMPIH.ColorUsed = bR.ReadUInt32();
            bMPIH.ColorImportant = bR.ReadUInt32();
        }

        public void ReadPixel(ref BitmapFileHeader bMPFH, ref BitmapInfoHeader bMPIH, Pixel[,] mas, ref BinaryReader bR)
        {

            bR.BaseStream.Position = bMPFH.OffsetBits;

            if (bMPIH.BitCount == 32)
            {
                for (int i = 0; i < bMPIH.Hight + 4; ++i)
                {
                    for (int j = 0; j < bMPIH.Width + 4; ++j)
                    {
                        if (i <= 1 || j <= 1 || i >= bMPIH.Hight + 2 || j >= bMPIH.Width + 2)
                        {
                            mas[i, j].Blue = 0;
                            mas[i, j].Green = 0;
                            mas[i, j].Red = 0;
                        }

                        else
                        {
                            mas[i, j].Blue = bR.ReadByte();
                            mas[i, j].Green = bR.ReadByte();
                            mas[i, j].Red = bR.ReadByte();
                            mas[i, j].Reserved = bR.ReadByte();
                        }
                    }
                }
            }

            else
            {
                for (int i = 0; i < bMPIH.Hight + 4; ++i)
                {
                    for (int j = 0; j < bMPIH.Width + 4; ++j)
                    {
                        if (i <= 1 || j <= 1 || i >= bMPIH.Hight + 2 || j >= bMPIH.Width + 2)
                        {
                            mas[i, j].Blue = 0;
                            mas[i, j].Green = 0;
                            mas[i, j].Red = 0;
                        }

                        else
                        {
                            mas[i, j].Blue = bR.ReadByte();
                            mas[i, j].Green = bR.ReadByte();
                            mas[i, j].Red = bR.ReadByte();
                        }
                    }
                }
            }

            Rat(ref bMPFH, ref bMPIH, mas);
        }

        public void Save(ref BitmapFileHeader bMPFH, ref BitmapInfoHeader bMPIH, Pixel[,] mas, ref BinaryWriter bW)
        {
            bW.Write(bMPFH.TypeFile);
            bW.Write(bMPFH.TypeSize);
            bW.Write(bMPFH.SizeFile);
            bW.Write(bMPFH.ReservedOne);
            bW.Write(bMPFH.ReservedTwo);
            bW.Write(bMPFH.OffsetBits);



            bW.Write(bMPIH.Size);
            bW.Write(bMPIH.Width);
            bW.Write(bMPIH.Hight);
            bW.Write(bMPIH.Planes);
            bW.Write(bMPIH.BitCount);
            bW.Write(bMPIH.Compression);
            bW.Write(bMPIH.SizeImage);
            bW.Write(bMPIH.XPelsPerMeter);
            bW.Write(bMPIH.YPelsPerMeter);
            bW.Write(bMPIH.ColorUsed);
            bW.Write(bMPIH.ColorImportant);



            if (bMPIH.BitCount == 32)
            {
                for (int i = 2; i < bMPIH.Hight + 2; ++i)
                {
                    for (int j = 2; j < bMPIH.Width + 2; ++j)
                    {
                        bW.Write(mas[i, j].Blue);
                        bW.Write(mas[i, j].Green);
                        bW.Write(mas[i, j].Red);
                        bW.Write(mas[i, j].Reserved);
                    }
                }
            }

            else
            {
                for (int i = 2; i < bMPIH.Hight + 2; ++i)
                {
                    for (int j = 2; j < bMPIH.Width + 2; ++j)
                    {
                        bW.Write(mas[i, j].Blue);
                        bW.Write(mas[i, j].Green);
                        bW.Write(mas[i, j].Red);
                    }
                }
            }

            bW.Close();
        }
    }
}
