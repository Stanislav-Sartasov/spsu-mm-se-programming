using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
	public class GreyFilter
	{
		public void ApplyGreyFilter(Image image)
		{
			int sum;
			for (int x = 0; x < image.Height; x++)
			{
				for (int y = 0; y < image.Width; y++)
				{
					sum = (image.Pixels[x, y].B + image.Pixels[x, y].G + image.Pixels[x, y].R) / 3;
					image.Pixels[x, y].B = (byte)sum;
					image.Pixels[x, y].G = (byte)sum;
					image.Pixels[x, y].R = (byte)sum;
				}
			}
		}
	}
}
