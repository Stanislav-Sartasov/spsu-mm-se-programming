using Core.Filter;

namespace Filters
{
	public class Gauss : KernelFilter
	{
		protected override int[,] Kernel { get; } =
		{
			{ 1, 4, 7, 4, 1 },
			{ 4, 16, 26, 16, 4 },
			{ 7, 26, 41, 26, 7 },
			{ 4, 16, 26, 16, 4 },
			{ 1, 4, 7, 4, 1 },
		};

		protected override int Divider { get; } = 273;
	}
}
