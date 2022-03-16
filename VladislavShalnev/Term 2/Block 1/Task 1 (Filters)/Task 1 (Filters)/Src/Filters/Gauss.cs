using Task_1.Core.Filter;

namespace Task_1.Filters
{
	public class Gauss : AKernelFilter
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
