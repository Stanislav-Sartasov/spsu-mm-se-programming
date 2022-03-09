using Core.Filter;
using Core.Image;

namespace Filters
{
	public class SobelX : KernelFilter
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
