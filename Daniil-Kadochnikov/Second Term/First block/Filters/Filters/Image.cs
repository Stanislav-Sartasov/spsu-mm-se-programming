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
		public byte[] HeaderInfo;
		public Pixel[,] Pixels;
		public int Width;
		public int Height;
		public int Size;
		public short BitCount;
		public int OffBits;

		public void BitMapping(string Name)
		{
			int Counter1;
			int Counter2;
			int Adds, Position;
			byte[] Data = File.ReadAllBytes(Name);

			OffBits = BitConverter.ToInt32(Data, 10);
			HeaderInfo = new byte[OffBits];
			Array.Copy(Data, HeaderInfo, OffBits);
			BitCount = BitConverter.ToInt16(HeaderInfo, 28);
			Width = BitConverter.ToInt32(HeaderInfo, 18);
			Height = BitConverter.ToInt32(HeaderInfo, 22);
			Size = BitConverter.ToInt32(HeaderInfo, 2);

			Pixels = new Pixel[Height, Width];

			Adds = (4 - (Width * (BitCount / 8) % 4)) % 4;
			for (Counter1 = 0; Counter1 < Height; Counter1++)
			{
				for (Counter2 = 0; Counter2 < Width; Counter2++)
				{
					Pixels[Counter1, Counter2] = new Pixel();
					Position = OffBits + ((BitCount / 8) * Width + Adds) * Counter1 + Counter2 * (BitCount / 8);
					Pixels[Counter1, Counter2].B = Data[Position];
					Pixels[Counter1, Counter2].G = Data[Position + 1];
					Pixels[Counter1, Counter2].R = Data[Position + 2];
				}
			}
		}

		public void SavingFile(string Name)
		{
			int Counter1, Counter2;
			int Adds, Position = OffBits;

			byte[] Data = new byte[Size];
			Array.Copy(HeaderInfo, Data, OffBits);

			if (BitCount == 24)
			{
				Adds = (4 - (Width * (BitCount / 8) % 4)) % 4;
				for (Counter1 = 0; Counter1 < Height; Counter1++)
				{
					for (Counter2 = 0; Counter2 < Width; Counter2++)
					{
						Data[Position] = Pixels[Counter1, Counter2].B;
						Data[Position + 1] = Pixels[Counter1, Counter2].G;
						Data[Position + 2] = Pixels[Counter1, Counter2].R;
						Position += 3;
					}
					for (Counter2 = 0; Counter2 < Adds; Counter2++)
					{
						Data[Position + 3 + Counter2] = (byte)'\0';
					}
					Position += Adds;
				}
			}
			else if (BitCount == 32)
			{
				for (Counter1 = 0; Counter1 < Height; Counter1++)
				{
					for (Counter2 = 0; Counter2 < Width; Counter2++)
					{
						Data[Position] = Pixels[Counter1, Counter2].B;
						Data[Position + 1] = Pixels[Counter1, Counter2].G;
						Data[Position + 2] = Pixels[Counter1, Counter2].R;
						Data[Position + 3] = (byte)'\0';
						Position += 4;
					}
				}
			}
			File.WriteAllBytes(Name, Data);
		}
	}
}
