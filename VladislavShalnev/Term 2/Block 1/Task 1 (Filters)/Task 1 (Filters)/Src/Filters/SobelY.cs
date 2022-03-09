using Core.Filter;
using Core.Image;

namespace Filters
{
	public class SobelY : KernelFilter
	{
		protected override int[,] Kernel { get; } =
		{
			{ -1, -2, -1 },
			{ 0, 0, 0 },
			{ 1, 2, 1 },
		};

		public override void ApplyTo(Bitmap bitmap)
		{
			base.ApplyTo(bitmap);
			new Grayscale().ApplyTo(bitmap);
		}
	}
}
