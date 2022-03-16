using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
	public class BMPImage
	{
        private byte[] Bitmap;
		private int ImageOffset;
		private int BitsPerPixel;
		private int WidthAddition;
		public int Width;
		public int Height;

		public BMPImage(string file)
		{
			Bitmap = File.ReadAllBytes(file);

			Width = BitConverter.ToInt32(Bitmap, 18);
			Height = BitConverter.ToInt32(Bitmap, 22);
			ImageOffset = BitConverter.ToInt32(Bitmap, 10);
			BitsPerPixel = BitConverter.ToInt16(Bitmap, 28);

			WidthAddition = (4 - Width * BitsPerPixel / 8 % 4) % 4;
		}

		public void WriteToFile(string file)
		{
			File.WriteAllBytes(file, Bitmap);
		}

		public Pixel GetPixel(int x, int y)
		{
			int position = ImageOffset + Width * (BitsPerPixel / 8) * y + (BitsPerPixel / 8) * x + y * WidthAddition;
			return new Pixel(Bitmap[position], Bitmap[position + 1], Bitmap[position + 2]);
		}

		public void SetPixel(Pixel pixel, int x, int y)
		{
			int position = ImageOffset + Width * (BitsPerPixel / 8) * y + (BitsPerPixel / 8) * x + y * WidthAddition;
			Bitmap[position] = pixel.Red;
			Bitmap[position + 1] = pixel.Green;
			Bitmap[position + 2] = pixel.Blue;
		}
	}
}
