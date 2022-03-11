using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class BMPFileHeader
    {
        public string FileType { get; private set;}
        public uint FileSize { get; private set; }
        public ushort Reserved1 { get; private set; }
        public ushort Reserved2 { get; private set; }
        public uint ImageOffset { get; private set; }
        public uint HeaderSize { get; private set; }
        public uint Width { get; private set; }
        public uint Height { get; private set; }
        public ushort Planes { get; private set; }
        public ushort BitPerPixel { get; private set; }
        public uint Compression { get; private set; }
        public uint SizeImage { get; private set; }
        public uint XPelsPerMeter { get; private set; }
        public uint YPelsPerMeter { get; private set; }
        public uint ColorsUsed { get; private set; }
        public uint ColorsImportant { get; private set; }

        public byte[] ByteRepresentation { get; private set; } // byte reprezentation of the BMP Header 

        public void ShowInfo()
        {
            Console.WriteLine("FileType: " + FileType +
             "\nFileSize: " + FileSize +
             "\nReserved1: " + Reserved1 +
             "\nReserved2: " + Reserved2 +
             "\nImageOffset: " + ImageOffset +
             "\nHeaderSize: " + HeaderSize +
             "\nWidth: " + Width +
             "\nHeight: " + Height +
             "\nPlanes: " + Planes +
             "\nBitPerPixel: " + BitPerPixel +
             "\nCompression: " + Compression +
             "\nSizeImage: " + SizeImage +
             "\nXPelsPerMeter: " + XPelsPerMeter +
             "\nYPelsPerMeter: " + YPelsPerMeter +
             "\nColorsUsed: " + ColorsUsed +
             "\nColorsImportant: " + ColorsImportant);
        }

        public BMPFileHeader(byte[] input)
        {
            FileType = Encoding.ASCII.GetString(input, 0, 2); ;
            FileSize = BitConverter.ToUInt32(input, 2);
            Reserved1 = BitConverter.ToUInt16(input, 6);
            Reserved2 = BitConverter.ToUInt16(input, 8);
            ImageOffset = BitConverter.ToUInt32(input, 10);
            HeaderSize = BitConverter.ToUInt32(input, 14);
            Width = BitConverter.ToUInt32(input, 18);
            Height = BitConverter.ToUInt32(input, 22);
            Planes = BitConverter.ToUInt16(input, 26);
            BitPerPixel = BitConverter.ToUInt16(input, 28);
            Compression = BitConverter.ToUInt32(input, 30);
            SizeImage = BitConverter.ToUInt32(input, 34);
            XPelsPerMeter = BitConverter.ToUInt32(input, 38);
            YPelsPerMeter = BitConverter.ToUInt32(input, 42);
            ColorsUsed = BitConverter.ToUInt32(input, 46);
            ColorsImportant = BitConverter.ToUInt32(input, 50);
            ByteRepresentation = new byte[ImageOffset];
            Array.Copy(input, ByteRepresentation, ImageOffset);
        }
    }
}