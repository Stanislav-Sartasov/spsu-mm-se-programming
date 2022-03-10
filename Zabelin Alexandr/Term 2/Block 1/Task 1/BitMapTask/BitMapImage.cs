using System;
using System.IO;

namespace BitMapTask
{
    public class BitMapImage
    {
        private byte[] BitMap { get; set; }
        public uint Width { get; private set; }
        public uint Height { get; private set; }
        private uint PixelOffset { get; set; }
        private ushort BitsPerPixel { get; set; }

        public BitMapImage(string path)
        {
            this.ReadBitMap(path);
        }

        private void ReadBitMap(string path)
        {
            // Checking successful file opening

            while (!File.Exists(path))
            {
                Console.Write("Something wrong with path. Please, write only path to the initial BMP file, which will be filtereted, without quotes and other additional symbols: ");
                path = Console.ReadLine();
                Console.WriteLine();
            }

            // --------------------------

            this.BitMap = File.ReadAllBytes(path);

            this.PixelOffset = BitConverter.ToUInt32(this.BitMap, 10);
            this.Width = BitConverter.ToUInt32(this.BitMap, 18);
            this.Height = BitConverter.ToUInt32(this.BitMap, 22);
            this.BitsPerPixel = BitConverter.ToUInt16(this.BitMap, 28);
        }

        public void WriteBitMap(string path)
        {
            File.WriteAllBytes(path, this.BitMap);
        }

        // Pixels

        private uint GetPixelPosition(uint x, uint y)
        {
            return this.PixelOffset + (x * this.BitsPerPixel / 8) + (y * BitsPerPixel / 8 * Width) + ((4 - Width * BitsPerPixel / 8 % 4) % 4 * y);
        }

        public Pixel GetPixel(uint x, uint y)
        {
            uint pixelPosition = this.GetPixelPosition(x, y);

            return new Pixel(this.BitMap[pixelPosition], this.BitMap[pixelPosition + 1], this.BitMap[pixelPosition + 2]);
        }

        public void SetPixel(uint x, uint y, Pixel pixel)
        {
            uint pixelPosition = this.GetPixelPosition(x, y);

            this.BitMap[pixelPosition] = (byte)pixel.Red;
            this.BitMap[pixelPosition + 1] = (byte)pixel.Green;
            this.BitMap[pixelPosition + 2] = (byte)pixel.Blue;
        }
    }
}