using Task_1.Core.Filter;
using Task_1.Core.Image;

namespace Task_1.Filters
{
	public class Median : AKernelFilter
	{
		protected override int[,] Kernel { get; } = new int[7, 7];

		protected override Pixel ProcessMatrix(Pixel[,] matrix, int height, int width)
		{
			Pixel[] flatMatrix = matrix.Cast<Pixel>().ToArray();
			int[] reds = flatMatrix.Select(pixel => pixel.Red).ToArray();
			int[] greens = flatMatrix.Select(pixel => pixel.Green).ToArray();
			int[] blues = flatMatrix.Select(pixel => pixel.Blue).ToArray();

			Array.Sort(reds);
			Array.Sort(greens);
			Array.Sort(blues);

			int center = height * width / 2;

			return new Pixel(reds[center], greens[center], blues[center]);
		}
	}
}
