namespace ImageFilters
{
	public class Bitmap
	{
		private bool isLoaded = false;

		private byte[] header = new byte[54];
		private byte[] palette = new byte[0];
		public BitmapImage Image = new();

		private int lineWidth;

		private int offBits;
		private int bitCount;
		private int bmpBiSize;
		private int compression;
		private int clrUsed;

		public bool CheckIfLoaded()
		{
			return isLoaded;
		}

		private bool ValidateHeader()
		{
			return header[0] == 0x42
				&& header[1] == 0x4d
				&& bmpBiSize == 40
				&& compression == 0
				&& (bitCount == 24 || bitCount == 32)
				&& offBits == (54 + clrUsed * 4);
		}

		private void DecodeHeader()
		{
			offBits = BitConverter.ToInt32(header, 10);
			bmpBiSize = BitConverter.ToInt32(header, 14);
			int width = BitConverter.ToInt32(header, 18);
			int height = BitConverter.ToInt32(header, 22);
			bitCount = BitConverter.ToInt16(header, 28);
			compression = BitConverter.ToInt32(header, 30);
			clrUsed = BitConverter.ToInt32(header, 46);

			lineWidth = ((bitCount * width + 31) / 32) * 4;
			Image = new BitmapImage(height, lineWidth, bitCount / 8);
		}

		public int ReadBitmap(Stream inputFile)
		{
			isLoaded = false;

			try
			{
				inputFile.Read(header, 0, 54);
				DecodeHeader();
				palette = new byte[4 * clrUsed];
				inputFile.Read(palette, 0, 4 * clrUsed);
				for (int i = 0; i < Image.Height; i++)
				{
					for (int j = 0; j < Image.Width; j++)
					{
						Image.Data[i, j] = (byte)inputFile.ReadByte();
					}
				}
			}
			catch
			{
				return -1;
			}

			if (!ValidateHeader())
				return 1;

			isLoaded = true;
			return 0;
		}

		public int WriteBitmap(Stream outputFile)
		{
			if (!isLoaded)
			{
				return 1;
			}

			try
			{
				outputFile.Write(header);
				outputFile.Write(palette);
				for (int i = 0; i < Image.Height; i++)
				{
					for (int j = 0; j < Image.Width; j++)
					{
						outputFile.WriteByte(Image.Data[i, j]);
					}
				}
			}
			catch
			{
				return -1;
			}

			return 0;
		}
	}
}
