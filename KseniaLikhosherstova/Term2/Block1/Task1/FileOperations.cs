namespace Task1
{
    public class FileOperations
    {

        public void Rat(ref BitmapFileHeader BMPFH, ref BitmapInfoHeader BMPIH, Pixel[,] mas)
        {
            mas[0, 0] = mas[2, 2];
            mas[1, 1] = mas[2, 2];
            mas[0, BMPIH.Width + 3] = mas[2, BMPIH.Width + 1];
            mas[1, BMPIH.Width + 2] = mas[2, BMPIH.Width + 1];
            mas[BMPIH.Hight + 3, 0] = mas[BMPIH.Hight + 1, 2];
            mas[BMPIH.Hight + 2, 1] = mas[BMPIH.Hight + 1, 2];
            mas[BMPIH.Hight + 3, BMPIH.Width + 3] = mas[BMPIH.Hight + 1, BMPIH.Width + 1];
            mas[BMPIH.Hight + 2, BMPIH.Width + 2] = mas[BMPIH.Hight + 1, BMPIH.Width + 1];

            for (int i = 2; i < BMPIH.Hight + 2; ++i)
            {
                mas[i, 1] = mas[i, 2];
            }
            for (int i = 1; i < BMPIH.Hight + 3; ++i)
            {
                mas[i, 0] = mas[i, 2];
            }

            for (int i = 2; i < BMPIH.Hight + 2; ++i)
            {
                mas[i, BMPIH.Width + 2] = mas[i, BMPIH.Width + 1];
            }
            for (int i = 1; i < BMPIH.Hight + 3; ++i)
            {
                mas[i, BMPIH.Width + 3] = mas[i, BMPIH.Width + 1];
            }

            for (int j = 2; j < BMPIH.Width + 2; ++j)
            {
                mas[1, j] = mas[2, j];
            }
            for (int j = 1; j < BMPIH.Width + 3; ++j)
            {
                mas[0, j] = mas[2, j];
            }

            for (int j = 2; j < BMPIH.Width + 2; ++j)
            {
                mas[BMPIH.Hight + 2, j] = mas[BMPIH.Hight + 1, j];
            }
            for (int j = 1; j < BMPIH.Width + 3; ++j)
            {
                mas[BMPIH.Hight + 3, j] = mas[BMPIH.Hight + 1, j];
            }

        }

        public void ReadFile(ref BitmapFileHeader BMPFH, ref BitmapInfoHeader BMPIH, ref BinaryReader bR)
        {
            BMPFH.TypeFile = bR.ReadByte();
            BMPFH.TypeSize = bR.ReadByte();
            BMPFH.SizeFile = bR.ReadUInt32();
            BMPFH.ReservedOne = bR.ReadUInt16();
            BMPFH.ReservedTwo = bR.ReadUInt16();
            BMPFH.OffsetBits = bR.ReadUInt32();

            BMPIH.Size = bR.ReadUInt32();
            BMPIH.Width = bR.ReadUInt32();
            BMPIH.Hight = bR.ReadUInt32();
            BMPIH.Planes = bR.ReadUInt16();
            BMPIH.BitCount = bR.ReadUInt16();
            BMPIH.Compression = bR.ReadUInt16();
            BMPIH.SizeImage = bR.ReadUInt32();
            BMPIH.XPelsPerMeter = bR.ReadUInt32();
            BMPIH.YPelsPerMeter = bR.ReadUInt32();
            BMPIH.ColorUsed = bR.ReadUInt32();
            BMPIH.ColorImportant = bR.ReadUInt32();
        }

        public void ReadPixel(ref BitmapFileHeader BMPFH, ref BitmapInfoHeader BMPIH, Pixel[,] mas, ref BinaryReader bR)
        {

            bR.BaseStream.Position = BMPFH.OffsetBits;

            if (BMPIH.BitCount == 32)
            {
                for (int i = 0; i < BMPIH.Hight + 4; ++i)
                {
                    for (int j = 0; j < BMPIH.Width + 4; ++j)
                    {
                        if (i <= 1 || j <= 1 || i >= BMPIH.Hight + 2 || j >= BMPIH.Width + 2)
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
                for (int i = 0; i < BMPIH.Hight + 4; ++i)
                {
                    for (int j = 0; j < BMPIH.Width + 4; ++j)
                    {
                        if (i <= 1 || j <= 1 || i >= BMPIH.Hight + 2 || j >= BMPIH.Width + 2)
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
            Rat(ref BMPFH, ref BMPIH, mas);
        }

        public void Save(ref BitmapFileHeader BMPFH, ref BitmapInfoHeader BMPIH, Pixel[,] mas, ref BinaryWriter bW)
        {
            bW.Write(BMPFH.TypeFile);
            bW.Write(BMPFH.TypeSize);
            bW.Write(BMPFH.SizeFile);
            bW.Write(BMPFH.ReservedOne);
            bW.Write(BMPFH.ReservedTwo);
            bW.Write(BMPFH.OffsetBits);



            bW.Write(BMPIH.Size);
            bW.Write(BMPIH.Width);
            bW.Write(BMPIH.Hight);
            bW.Write(BMPIH.Planes);
            bW.Write(BMPIH.BitCount);
            bW.Write(BMPIH.Compression);
            bW.Write(BMPIH.SizeImage);
            bW.Write(BMPIH.XPelsPerMeter);
            bW.Write(BMPIH.YPelsPerMeter);
            bW.Write(BMPIH.ColorUsed);
            bW.Write(BMPIH.ColorImportant);



            if (BMPIH.BitCount == 32)
            {
                for (int i = 2; i < BMPIH.Hight + 2; ++i)
                {
                    for (int j = 2; j < BMPIH.Width + 2; ++j)
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
                for (int i = 2; i < BMPIH.Hight + 2; ++i)
                {
                    for (int j = 2; j < BMPIH.Width + 2; ++j)
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
