using System;

namespace Task_1._1
{
    public class Bitmap
    {
        private byte[] Headers;
        private int DataBegin;
        private int WidthAddition;
        private byte[] Data;
        private int BitsPerPixel;

        public int Width;
        public int Height;

        public Bitmap(string filename)
        {
            byte[] innerData = File.ReadAllBytes(filename);

            DataBegin = ParseInt(innerData, 10);

            Height = ParseInt(innerData, 22);
            Width = ParseInt(innerData, 18);

            BitsPerPixel = 256 * innerData[29] + innerData[28];
            WidthAddition = (4 - Width * BitsPerPixel / 8 % 4) % 4;

            Data = new byte[innerData.Length - DataBegin + WidthAddition * Height];
            Headers = new byte[DataBegin];

            for (int i = 0; i < innerData.Length - DataBegin; i++)
                Data[i] = innerData[DataBegin + i];

            for (int i = 0; i < DataBegin; i++)
                Headers[i] = innerData[i];
        }

        public void Save(string filename)
        {
            byte[] data = new byte[Headers.Length + Data.Length];

            for (int i = 0; i < Headers.Length; i++)
                data[i] = Headers[i];

            for (int i = 0; i < Data.Length; i++)
                data[i + DataBegin] = Data[i];

            File.WriteAllBytes(filename, data);
        }

        private int ParseInt(byte[] begin, int position)
        {
            return 16777216 * begin[position + 3] + 65536 * begin[position + 2] + 256 * begin[position + 1] + begin[position + 0];
        }

        public Pixel GetPixel(int x, int y)
        {
            int position = Width * (BitsPerPixel / 8) * y + (BitsPerPixel / 8) * x + y * WidthAddition;
            return new Pixel(Data[position + 0], Data[position + 1], Data[position + 2]);
        }

        public void SetPixel(int x, int y, Pixel pixel)
        {
            int position = Width * (BitsPerPixel / 8) * y + (BitsPerPixel / 8) * x + y * WidthAddition;
            Data[position + 0] = (byte)pixel.R;
            Data[position + 1] = (byte)pixel.G;
            Data[position + 2] = (byte)pixel.B;
        }
    }
}
