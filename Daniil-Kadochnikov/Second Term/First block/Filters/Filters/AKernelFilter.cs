using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
	public class AKernelFilter
	{
		private byte MultiplyMatrix(byte[] massive, int[] kernel, int divider)
		{
			int result = 0;
			for(int counter = 0; counter < massive.Length; counter++)
			{
				result += massive[counter] * kernel[counter] / divider;
			}
			return (byte)Math.Min(255, Math.Abs(result));
		}

		public void FormalizeMatrix(Image image, int scale, int[] kernel, int divider)
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
					buffer[imageX, imageY] = new Pixel();
					buffer[imageX, imageY].B = MultiplyMatrix(blue, kernel, divider);
					buffer[imageX, imageY].G = MultiplyMatrix(green, kernel, divider);
					buffer[imageX, imageY].R = MultiplyMatrix(red, kernel, divider);
				}
			}
			Array.Copy(buffer, image.Pixels, image.Height * image.Width);
		}
	}
}
