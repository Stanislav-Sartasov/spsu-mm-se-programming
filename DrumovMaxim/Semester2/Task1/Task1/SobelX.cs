namespace Task1
{
    public class SobelX : KernelFilters
    {
		public SobelX()
		{
			kernel = new float[,]
			{
				{ -1f, 0f, 1f },
				{ -2f, 0f, 2f },
				{ -1f, 0f, 1f }
			};
		}

		public override void PixelConvolution(ref Image bmpImage)
		{
			new GrayScale().PixelConvolution(ref bmpImage);
			base.PixelConvolution(ref bmpImage);
		}
	}
}
