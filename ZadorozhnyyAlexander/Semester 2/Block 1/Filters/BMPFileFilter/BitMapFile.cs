using System;
using System.IO;

namespace BMPFileFilter
{
    public class BitMapFile
    {
		private Int16 bitsPerPixel;
		private readonly byte[] header;
		private int channels;

		public int CountRowRgb { get; }
		public int Width { get; }
		public int Height { get; }
		public Rgb[,] ImageRgb;

		public BitMapFile(FileStream file)
        {
			BinaryReader readerMachine = new(file);
			header = readerMachine.ReadBytes(54);

			MemoryStream savedData = new(header, 0, 54);
			BinaryReader dataReader = new(savedData);

			dataReader.ReadBytes(18); // we skip unnecessary information
			Width = dataReader.ReadInt32();
			Height = dataReader.ReadInt32();
			dataReader.ReadBytes(2); // we skip unnecessary information
			bitsPerPixel = dataReader.ReadInt16();

			savedData.Close();
			dataReader.Close();

			if (bitsPerPixel == 24)
			{
				CountRowRgb = (3 * Width + Width % 4) / 3;
				channels = 3;
			}
			else if (bitsPerPixel == 32)
			{
				CountRowRgb = Width;
				channels = 4;
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
					ImageRgb[i, j] = new Rgb(readerMachine.ReadBytes(channels));
                }
			}
			readerMachine.Close();
		}

		public void WriteNewFile(FileStream savingFile)
		{
			if (savingFile == null)
				throw new Exception("Null file");
			BinaryWriter writeMachine = new(savingFile);
			writeMachine.Write(header);

			for (int i = Height - 1; i >= 0; i--)
			{
				for (int j = CountRowRgb - 1; j >= 0; j--)
				{
					writeMachine.Write(ImageRgb[i, j].GetListBytes());
				}		
			}
			writeMachine.Close();
			savingFile.Close();
		}
	}
}
