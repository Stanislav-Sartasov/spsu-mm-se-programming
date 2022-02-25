using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1._1
{
	public class GrayScale : IFilter
	{
		public void ProcessBitmap(Bitmap bmp)
		{
			int avarage;

			for (int i = 0; i < bmp.Width; i++)
			{
				for (int j = 0; j < bmp.Height; j++)
				{
					Pixel pixel = bmp.GetPixel(i, j);
					avarage = (pixel.R + pixel.G + pixel.B) / 3;
					bmp.SetPixel(i, j, new Pixel(avarage, avarage, avarage));
				}
			}
		}
	}
}
