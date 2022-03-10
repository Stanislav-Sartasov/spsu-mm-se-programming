namespace BmpFilters
{
    public class BitMapReading
    {
        public readonly byte[] Header; // 0-53 bytes

        public readonly char[] BmId; // pos 0
        public uint Width { get; } // pos 18
        public uint Height { get; } // pos 22
        public ushort BitsPerPixel { get; } // pos 28

        public readonly byte[][] Image; // pos 54

        public BitMapReading(string fileName)
        {
            using (var fstream = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                Header = fstream.ReadBytes(54);

                fstream.BaseStream.Seek(0 - fstream.BaseStream.Position, SeekOrigin.Current);
                BmId = fstream.ReadChars(2);
                if (BmId[0] != 'B' || BmId[1] != 'M')
                {
                    Console.WriteLine("The file is corrupted or is not a bmp");
                    Environment.Exit(1);
                }

                fstream.BaseStream.Seek(28 - fstream.BaseStream.Position, SeekOrigin.Current);
                BitsPerPixel = fstream.ReadUInt16();
                if (BitsPerPixel != 24 && BitsPerPixel != 32)
                {
                    Console.WriteLine("The file is neither 24 bit nor 32 bit");
                    Environment.Exit(2);
                }

                fstream.BaseStream.Seek(18 - fstream.BaseStream.Position, SeekOrigin.Current);
                Width = fstream.ReadUInt32() * BitsPerPixel / 8;
                Width += BitsPerPixel == 24 ? Width % 4 : 0;

                fstream.BaseStream.Seek(22 - fstream.BaseStream.Position, SeekOrigin.Current);
                Height = fstream.ReadUInt32();

                fstream.BaseStream.Seek(54 - fstream.BaseStream.Position, SeekOrigin.Current);
                Image = new byte[Height][];
                for (int row = 0; row < Height; row++)
                {
                    Image[row] = fstream.ReadBytes((int)Width);
                }
            }
        }

        public static bool operator ==(BitMapReading first, BitMapReading second)
        {
            for (int i = 0; i < first.Header.Length && i < second.Header.Length; i++)
                if (first.Header[i] != second.Header[i])
                    return false;
            for (int i = 0; i < first.Height && i < second.Height; i++)
                for (int j = 0; j < first.Width && j < second.Width; j++)
                    if (first.Image[i][j] != second.Image[i][j])
                        return false;
            return true;
        }
        public static bool operator !=(BitMapReading first, BitMapReading second)
        {
            for (int i = 0; i < first.Header.Length && i < second.Header.Length; i++)
                if (first.Header[i] != second.Header[i])
                    return true;
            for (int i = 0; i < first.Height && i < second.Height; i++)
                for (int j = 0; j < first.Width && j < second.Width; j++)
                    if (first.Image[i][j] != second.Image[i][j])
                        return true;
            return false;
        }
    }
}