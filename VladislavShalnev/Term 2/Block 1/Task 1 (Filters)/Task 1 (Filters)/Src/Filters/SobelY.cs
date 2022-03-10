using Task_1.Core.Filter;
using Task_1.Core.Image;

namespace Task_1.Filters
{
	public class SobelY : AKernelFilter
	{
		protected override int[,] Kernel { get; } =
		{
			{ -1, -2, -1 },
			{ 0, 0, 0 },
			{ 1, 2, 1 },
		};

		public override Bitmap ApplyTo(Bitmap bitmap) =>
			new Grayscale().ApplyTo(base.ApplyTo(bitmap));
	}
}
