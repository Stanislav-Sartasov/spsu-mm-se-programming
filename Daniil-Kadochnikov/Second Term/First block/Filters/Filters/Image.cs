using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
	public class Image
	{
		private byte[] HeaderInfo;
		public Pixel[,] Pixels;
		public int Width;
		public int Height;
		private int Size;
		private short BitCount;
		private int OffBits;

		public void BitMap(string name)
		{
			int adds, position;
			byte[] data = File.ReadAllBytes(name);

			OffBits = BitConverter.ToInt32(data, 10);
			HeaderInfo = new byte[OffBits];
			Array.Copy(data, HeaderInfo, OffBits);
			BitCount = BitConverter.ToInt16(HeaderInfo, 28);
			Width = BitConverter.ToInt32(HeaderInfo, 18);
			Height = BitConverter.ToInt32(HeaderInfo, 22);
			Size = BitConverter.ToInt32(HeaderInfo, 2);

			Pixels = new Pixel[Height, Width];

			adds = (4 - (Width * (BitCount / 8) % 4)) % 4;
			for (int x = 0; x < Height; x++)
			{
				for (int y = 0; y < Width; y++)
				{
					Pixels[x, y] = new Pixel();
					position = OffBits + ((BitCount / 8) * Width + adds) * x + y * (BitCount / 8);
					Pixels[x, y].B = data[position];
					Pixels[x, y].G = data[position + 1];
					Pixels[x, y].R = data[position + 2];
				}
			}
		}

		public void SaveFile(string Name)
		{
			int adds, position = OffBits;

			byte[] data = new byte[Size];
			Array.Copy(HeaderInfo, data, OffBits);

			if (BitCount == 24)
			{
				adds = (4 - (Width * (BitCount / 8) % 4)) % 4;
				for (int x = 0; x < Height; x++)
				{
					for (int y = 0; y < Width; y++)
					{
						data[position] = Pixels[x, y].B;
						data[position + 1] = Pixels[x, y].G;
						data[position + 2] = Pixels[x, y].R;
						position += 3;
					}
					for (int y = 0; y < adds; y++)
					{
						data[position + 3 + y] = (byte)'\0';
					}
					position += adds;
				}
			}
			else if (BitCount == 32)
			{
				for (int x = 0; x < Height; x++)
				{
					for (int y = 0; y < Width; y++)
					{
						data[position] = Pixels[x, y].B;
						data[position + 1] = Pixels[x, y].G;
						data[position + 2] = Pixels[x, y].R;
						data[position + 3] = (byte)'\0';
						position += 4;
					}
				}
			}
			File.WriteAllBytes(Name, data);
		}
	}
}
