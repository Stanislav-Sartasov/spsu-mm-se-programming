using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
	public class MedianFilter
	{
		public void ApplyMedianFilter(Image image, int scale)
		{
			byte[] red = new byte[scale * scale], green = new byte[scale * scale], blue = new byte[scale * scale];
			Pixel[,] buffer = new Pixel[image.Height, image.Width];
			int offSet = scale / 2, row, column;

			for (int imageX = 0; imageX < image.Height; imageX++)
			{
				for (int imageY = 0; imageY < image.Width; imageY++)
				{
					int counter = 0;
					for (int matrixX = -offSet; matrixX < offSet + 1; matrixX++)
					{
						row = imageX + matrixX;
						for (int matrixY = -offSet; matrixY < offSet + 1; matrixY++)
						{
							column = imageY + matrixY;

							if (row < 0 || row > image.Height - 1)
							{
								row -= matrixX;
							}
							if (column < 0 || column > image.Width - 1)
							{
								column -= matrixY;
							}
							blue[counter] = image.Pixels[row, column].B;
							green[counter] = image.Pixels[row, column].G;
							red[counter] = image.Pixels[row, column].R;
							counter++;
						}
					}
					Array.Sort(blue);
					Array.Sort(green);
					Array.Sort(red);
					buffer[imageX, imageY] = new Pixel();
					buffer[imageX, imageY].B = blue[4];
					buffer[imageX, imageY].G = green[4];
					buffer[imageX, imageY].R = red[4];
				}
			}
			Array.Copy(buffer, image.Pixels, image.Height * image.Width);
		}
	}
}
