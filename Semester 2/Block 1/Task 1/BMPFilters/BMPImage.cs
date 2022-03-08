using System;
using System.IO;

namespace BMPFilters
{
    public class BMPImage
    {
		private byte[] Header = new byte[54];
		public int Width { get; }
		public int Height { get; }
		public ushort Bitness { get; }
		public int Channels { get; }
		public byte[][][] Bytes { get; }

		public BMPImage(FileStream input)
		{
			byte[] arrayInt = new byte[sizeof(int)];
			byte[] arrayUshort = new byte[sizeof(ushort)];

			input.Read(Header, 0, 54);
			
			input.Seek(18, SeekOrigin.Begin);
			input.Read(arrayInt, 0, sizeof(int));
			Width = BitConverter.ToInt32(arrayInt);

			input.Seek(22, SeekOrigin.Begin);
			input.Read(arrayInt, 0, sizeof(int));
			Height = BitConverter.ToInt32(arrayInt);

			input.Seek(28, SeekOrigin.Begin);
			input.Read(arrayUshort, 0, sizeof(ushort));
			Bitness = BitConverter.ToUInt16(arrayUshort);

			if (Bitness != 32 && Bitness != 24)
            {
				throw new Exception("Unsupported bitness.");
            }

			if (Bitness == 24)
			{
				Width += Width % 4;
				Channels = 3;
			}
			else
			{
				Channels = 4;
			}

			input.Seek(54, SeekOrigin.Begin);
			Bytes = new byte[Height][][];
			for (int i = 0; i < Height; i++)
			{
				Bytes[i] = new byte[Width][];
				for (int j = 0; j < Width; j++)
				{
					Bytes[i][j] = new byte[Channels];
					input.Read(Bytes[i][j], 0, Channels);
				}
			}
		}

		public void WriteToFile(FileStream file)
        {
			file.Write(Header);
			
			for (int i = 0; i < Height; i++)
			{
				for (int j = 0; j < Width; j++)
				{
					file.Write(Bytes[i][j], 0, Channels);
				}
			}
		}
	}
}
