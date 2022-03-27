namespace Task1
{
    public class BitmapFile
    {

        public struct BitmapFileHeader
        {
            public byte TypeFile, TypeSize;
            public uint SizeFile;
            public ushort ReservedOne;
            public ushort ReservedTwo;
            public uint OffsetBits;
        }

        public struct BitmapInfoHeader
        {
            public uint Size;
            public uint Width;
            public uint Hight;
            public ushort Planes;
            public ushort BitCount;
            public uint Compression;
            public uint SizeImage;
            public uint XPelsPerMeter;
            public uint YPelsPerMeter;
            public uint ColorUsed;
            public uint ColorImportant;
        }

        public struct Pixel
        {
            public byte Red;
            public byte Green;
            public byte Blue;
            public byte Reserved;
        }

        public void InitialData(BitmapFile bmp)
        {
            BitmapFileHeader BMPFH;
            BitmapInfoHeader BMPIH;

            BMPFH.OffsetBits = 0;
            BMPFH.ReservedOne = 1;
            BMPFH.ReservedTwo = 1;
            BMPFH.SizeFile = 0;
            BMPFH.TypeFile = 0;
            BMPFH.TypeSize = 0;

            BMPIH.BitCount = 0;
            BMPIH.ColorImportant = 0;
            BMPIH.Compression = 0;
            BMPIH.Hight = 0;
            BMPIH.Planes = 0;
            BMPIH.Size = 0;
            BMPIH.SizeImage = 0;
            BMPIH.Width = 0;
            BMPIH.XPelsPerMeter = 0;
            BMPIH.YPelsPerMeter = 0;
            BMPIH.ColorUsed = 0;
        }

    }
}
