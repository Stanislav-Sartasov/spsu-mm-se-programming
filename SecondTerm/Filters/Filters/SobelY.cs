namespace Filters
{
	class SobelY:Filter
	{
		internal override void Filtering(byte[] mas, uint width, uint height)
		{
			double[] core = { -1, 0, 1, -2, 0, 2, -1, 0, 1 };
			ConvMatrix.MultByConvMatrix(core, mas, width, height, 0);
		}
	}
}
