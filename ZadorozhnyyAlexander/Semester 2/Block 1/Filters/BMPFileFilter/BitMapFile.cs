using System;
using System.IO;

namespace BMPFileFilter
{
    public class BitMapFile
    {
		private Int16 BitsPerPixel;
		private readonly byte[] Header;
		private int Channels;

		public int CountRowRgb { get; }
		public int Width { get; }
		public int Height { get; }
		public Rgb[,] ImageRgb;

		public BitMapFile(FileStream file)
        {
			BinaryReader reader_machine = new(file);
			Header = reader_machine.ReadBytes(54);

			MemoryStream saved_data = new(Header, 0, 54);
			BinaryReader data_reader = new(saved_data);

			data_reader.ReadBytes(18); // we skip unnecessary information
			Width = data_reader.ReadInt32();
			Height = data_reader.ReadInt32();
			data_reader.ReadBytes(2); // we skip unnecessary information
			BitsPerPixel = data_reader.ReadInt16();

			saved_data.Close();
			data_reader.Close();

			if (BitsPerPixel == 24)
			{
				CountRowRgb = (3 * Width + Width % 4) / 3;
				Channels = 3;
			}
			else if (BitsPerPixel == 32)
			{
				CountRowRgb = Width;
				Channels = 4;
			}

            else
            {
				throw new Exception("Unsupported bitness of file.");
            }
			
			ImageRgb = new Rgb[Height, CountRowRgb];
			for (int i = Height - 1; i >= 0; i--)
			{
				for (int j = CountRowRgb - 1; j >= 0; j--)
                {
					ImageRgb[i, j] = new Rgb(reader_machine.ReadBytes(Channels));
                }
			}
			reader_machine.Close();
		}

		public void WriteNewFile(FileStream SavingFile)
		{
			if (SavingFile == null)
				throw new Exception("Null file");
			BinaryWriter write_machine = new(SavingFile);
			write_machine.Write(Header);

			for (int i = Height - 1; i >= 0; i--)
			{
				for (int j = CountRowRgb - 1; j >= 0; j--)
				{
					write_machine.Write(ImageRgb[i, j].GetListBytes());
				}		
			}
			write_machine.Close();
			SavingFile.Close();
		}
	}
}
