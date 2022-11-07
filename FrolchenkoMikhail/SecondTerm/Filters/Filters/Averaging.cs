
namespace Filters
{
	class Averaging:Filter
	{
		internal override void Filtering(byte[] mas, uint width, uint height)
		{
			double[] core = { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
			ConvMatrix.MultByConvMatrix(core, mas, width, height, 9);
		}
	}
}
