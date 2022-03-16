using Task_1.Core.Filter;

namespace Task_1.Filters
{
	public class Sharpen : AKernelFilter
	{
		protected override int[,] Kernel { get; } =
		{
			{ -1, -1, -1 },
			{ -1, 9, -1 },
			{ -1, -1, -1 },
		};
	}
}
