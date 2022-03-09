using Core.Filter;

namespace Filters
{
	public class Sharpen : KernelFilter
	{
		protected override int[,] Kernel { get; } =
		{
			{ -1, -1, -1 },
			{ -1, 9, -1 },
			{ -1, -1, -1 },
		};
	}
}
