using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
	public class Image
	{
		public byte[] Bitmap;
		public int Width;
		public int Height;
		public int PixelOffset;
		public int BitsPerPixel;

		public Image(string file)
		{
			Bitmap = File.ReadAllBytes(file);
			Width = BitConverter.ToInt32(Bitmap, 18);
			Height = BitConverter.ToInt32(Bitmap, 22);
			PixelOffset = BitConverter.ToInt32(Bitmap, 10);
			BitsPerPixel = BitConverter.ToInt16(Bitmap, 28);
		}

		public Pixel GetPixel(int x, int y)
		{
			int pixelPos = PixelOffset + x * BitsPerPixel / 8 + y * BitsPerPixel / 8 * Width + (4 - Width * BitsPerPixel / 8 % 4) % 4 * y;
			return new Pixel(Bitmap[pixelPos], Bitmap[pixelPos + 1], Bitmap[pixelPos + 2]);
		}

		public void SetPixel(Pixel pixel, int x, int y)
		{
			int pixelPos = PixelOffset + x * BitsPerPixel / 8 + y * BitsPerPixel / 8 * Width + (4 - Width * BitsPerPixel / 8 % 4) % 4 * y;
			Bitmap[pixelPos] = (byte)pixel.Red;
			Bitmap[pixelPos + 1] = (byte)pixel.Green;
			Bitmap[pixelPos + 2] = (byte)pixel.Blue;
		}

		public void SavePicture(string path)
		{
			File.WriteAllBytes(path, Bitmap);
		}
	}
}
