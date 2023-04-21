﻿using Task_1.Core.Filter.Interfaces;
using Task_1.Core.Image;

namespace Task_1.Filters
{
	public class Grayscale : IFilter
	{
		public Bitmap ApplyTo(Bitmap bitmap)
		{
			Bitmap result = (Bitmap)bitmap.Clone();

			// Going through all of the pixels and setting the average color for each pixel
			for (int i = 0; i < bitmap.Height; i++)
			{
				for (int j = 0; j < bitmap.Width; j++)
				{
					(byte red, byte green, byte blue) = bitmap[i, j];
					byte average = (byte)((red + green + blue) / 3);

					result[i, j] = new Pixel(average, average, average);
				}
			}

			return result;
		}
	}
}
