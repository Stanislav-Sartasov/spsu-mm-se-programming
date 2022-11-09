namespace Filters
{
	class Gauss:Filter
	{
		internal override void Filtering(byte[] mas, uint width, uint height)
		{
			double[] core = { 1, 2, 1, 2, 4, 2, 1, 2, 1 };
			ConvMatrix.MultByConvMatrix(core, mas, width, height, 16);
		}
	}
}
