using Task_1.Core.Filter;
using Task_1.Core.Image;

namespace Task_1.Filters
{
	public class SobelX : AKernelFilter
	{
		protected override int[,] Kernel { get; } =
		{
			{ -1, 0, 1 },
			{ -2, 0, 2 },
			{ -1, 0, 1 },
		};

		public override void ApplyTo(Bitmap bitmap)
		{
			base.ApplyTo(bitmap);
			new Grayscale().ApplyTo(bitmap);
		}
	}
}
