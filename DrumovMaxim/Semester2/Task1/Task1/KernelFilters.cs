namespace Task1
{
	public abstract class KernelFilters : IFilter
	{
		protected float[,] kernel;

		public virtual void PixelConvolution(ref Image bmpImage)
		{
			float[] pixels = new float[3];
			
			Pixel[,] copy = new Pixel[bmpImage.BiWidth, bmpImage.BiHeight];

			for (int x = 1; x < bmpImage.BiWidth - 1; x++)
			{
				for (int y = 1; y < bmpImage.BiHeight - 1; y++)
				{
					for (int i = 0; i < 3; i++)
					{
						pixels[i] = 0;
					}

					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							pixels[0] += bmpImage.GetPixel(x + i - 1, y + j - 1).ArrRGB[0] * kernel[i, j];
							pixels[1] += bmpImage.GetPixel(x + i - 1, y + j - 1).ArrRGB[1] * kernel[i, j];
							pixels[2] += bmpImage.GetPixel(x + i - 1, y + j - 1).ArrRGB[2] * kernel[i, j];
						}
					}

					for (int i = 0; i < 3; i++)
					{
						pixels[i] = Math.Abs(pixels[i]);
					}

                    byte[] arr = pixels.Select(i => (byte)i).ToArray();
      
                    copy[x, y] = new Pixel(arr);
				}
			}

			for (int x = 1; x < bmpImage.BiWidth - 1; x++)
			{
				for (int y = 1; y < bmpImage.BiHeight - 1; y++)
				{
					bmpImage.SetPixel(copy[x, y], x, y);
				}
			}

		}
	}
}
