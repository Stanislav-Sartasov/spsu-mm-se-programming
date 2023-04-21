namespace Task1
{
    public class MedianFilter : IFilter
    {
		public virtual void PixelConvolution(ref Image bmpImage)
		{
			byte[] red = new byte[9];
			byte[] green = new byte[9];
			byte[] blue = new byte[9];

			Pixel[,] copy = new Pixel[bmpImage.BiWidth, bmpImage.BiHeight];

			for (int x = 1; x < bmpImage.BiWidth - 1; x++)
			{
				for (int y = 1; y < bmpImage.BiHeight - 1; y++)
				{
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							red[3 * i + j] = bmpImage.GetPixel(x + i - 1, y + j - 1).ArrRGB[0];
							green[3 * i + j] = bmpImage.GetPixel(x + i - 1, y + j - 1).ArrRGB[1];
							blue[3 * i + j] = bmpImage.GetPixel(x + i - 1, y + j - 1).ArrRGB[2];
						}
					}
						
					Array.Sort(red);
					Array.Sort(green);
					Array.Sort(blue);

					byte[] arr = new byte[3];

					arr[0] = red[4];
					arr[1] = green[4];
					arr[2] = blue[4];

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
