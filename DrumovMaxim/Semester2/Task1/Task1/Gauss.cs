namespace Task1
{
    public class Gauss : KernelFilters
    {
		public Gauss()
		{
			kernel = new float[,]
			{ 
				{ 1f / 16f, 2f / 16f, 1f / 16f }, 
				{ 2f / 16f, 4f / 16f, 2f / 16f }, 
				{ 1f / 16f, 2f / 16f, 1f / 16f } 
			};
		}

		public override void PixelConvolution(ref Image bmpImage)
		{
			base.PixelConvolution(ref bmpImage);
		}
	}
}
