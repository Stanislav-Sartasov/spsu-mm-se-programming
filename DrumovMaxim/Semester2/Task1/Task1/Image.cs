namespace Task1
{
    public class Image
    {
		private byte[] HeaderInfo;
		private byte[] Data;
		private int BfOffbits;
		private int BiBitCount;
		private int Addition;
		public int BiWidth;
		public int BiHeight;
		
		public Image(string file)
		{
			byte[] inData = File.ReadAllBytes(file);

			BfOffbits = BitConverter.ToInt32(inData, 10);
			BiWidth = BitConverter.ToInt32(inData, 18);
			BiHeight = BitConverter.ToInt32(inData, 22);
			BiBitCount = BitConverter.ToInt16(inData, 28);

			Addition = (4 - BiWidth * BiBitCount / 8 % 4) % 4;

			Data = new byte[inData.Length - BfOffbits + Addition];
			HeaderInfo = new byte[BfOffbits];

            for (int i = 0; i < inData.Length - BfOffbits; i++)
            {
				Data[i] = inData[BfOffbits + i];
            }
            for (int i = 0; i < BfOffbits; i++)
            {
				HeaderInfo[i] = inData[i];
            }
		}

		public void SaveFile(string file)
		{
			byte[] data = new byte[HeaderInfo.Length + Data.Length];

            for (int i = 0; i < HeaderInfo.Length; i++)
            {
				data[i] = HeaderInfo[i];
            }
            for (int i = 0; i < Data.Length; i++)
            {
				data[BfOffbits + i] = Data[i];
            }

			File.WriteAllBytes(file, data);
		}

		public Pixel GetPixel(int x, int y)
		{
			int position = (BiBitCount / 8) * y * BiWidth + (BiBitCount / 8) * x + y * Addition;
			byte[] data = new byte[3];

            for (int i = 0; i < 3; i++)
            {
				data[i] = Data[position + i]; 
            }

			return new Pixel(data);
		}

		public void SetPixel(Pixel pixel, int x, int y)
		{
			
			int position = (BiBitCount / 8) * y * BiWidth + (BiBitCount / 8) * x + y * Addition;
			Data[position + 0] = pixel.ArrRGB[0];
			Data[position + 1] = pixel.ArrRGB[1];
			Data[position + 2] = pixel.ArrRGB[2];
		}
	}
}
