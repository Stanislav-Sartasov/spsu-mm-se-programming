namespace Filters
{
	class SobelX:Filter
	{
		internal override void Filtering(byte[] mas, uint width, uint height)
		{
			double[] core = { -1, -2, -1, 0, 0, 0, 1, 2, 1 };
			ConvMatrix.MultByConvMatrix(core, mas, width, height, 0);
		}
	}
}
