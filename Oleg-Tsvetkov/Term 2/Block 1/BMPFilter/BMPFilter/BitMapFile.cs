using System;
using System.Collections;
using System.IO;

namespace BMPFilter
{
    public class BitMapFile
    {
        public int Width { get; }
        public int Height { get; }
        public int Channels { get; }
        public int RowByteSize { get; }

        public readonly byte[][] ImageBytes;
        private readonly byte[] header;

        public BitMapFile(FileStream file)
        {
            BinaryReader reader = new(file);

            header = reader.ReadBytes(54);

            MemoryStream headerStream = new(header, 0, 54);

            BinaryReader headerReader = new(headerStream);

            headerReader.ReadBytes(18);
            Width = headerReader.ReadInt32();
            Height = headerReader.ReadInt32();
            headerReader.ReadBytes(2);
            int bitsCount = headerReader.ReadInt16();

            headerReader.Close();
            headerStream.Close();

            if (bitsCount == 24)
            {
                Channels = 3;
                RowByteSize = 3 * Width + Width % 4;
            }
            else if (bitsCount == 32)
            {
                Channels = 4;
                RowByteSize = 4 * Width;
            }
            else
            {
                throw new Exception("Неподдерживаемая битность BMP-файла.");
            }

            ImageBytes = new byte[Height][];

            for (int i = 0; i < Height; ++i)
            {
                ImageBytes[i] = reader.ReadBytes(RowByteSize);
            }

            reader.Close();
        }

        public void WriteResult(FileStream fileOut)
        {
            BinaryWriter writer = new(fileOut);

            writer.Write(header);
            for (int i = 0; i < Height; ++i)
            {
                writer.Write(ImageBytes[i]);
            }

            writer.Close();
        }
    }
}