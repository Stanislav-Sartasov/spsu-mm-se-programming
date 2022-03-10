using Task_1.Core.Filter.Interfaces;
using Task_1.Core.Image;

namespace Task_1.Core.Filter
{
	public abstract class AKernelFilter : IFilter
	{
		protected abstract int[,] Kernel { get; }
		protected virtual int Divider { get; } = 1;

		protected virtual Pixel ProcessMatrix(Pixel[,] matrix, int height, int width)
		{
			Pixel result = new Pixel(0, 0, 0);

			for (int i = 0; i < height; i++)
				for (int j = 0; j < width; j++)
					result += matrix[i, j] * Kernel[i, j] / Divider;

			var normalize = (int value) => Math.Min(Math.Abs(value), 255);

			return new Pixel(normalize(result.Red), normalize(result.Green), normalize(result.Blue));
		}

		public virtual void ApplyTo(Bitmap bitmap)
		{
			Pixel[,] buffer = new Pixel[bitmap.Height, bitmap.Width];

			int kernelHeight = Kernel.GetUpperBound(0) + 1;
			int kernelWidth = Kernel.Length / kernelHeight;

			Pixel[,] matrix = new Pixel[kernelWidth, kernelHeight];

			// Going through all of the pixels
			for (int i = 0; i < bitmap.Height; i++)
			{
				for (int j = 0; j < bitmap.Width; j++)
				{
					// Making up the processing matrix
					for (int k = 0; k < kernelHeight; k++)
					{
						for (int l = 0; l < kernelWidth; l++)
						{
							int m = Math.Min(Math.Max(i + k - kernelHeight / 2, 0), bitmap.Height - 1);
							int n = Math.Min(Math.Max(j + l - kernelWidth / 2, 0), bitmap.Width - 1);

							matrix[k, l] = bitmap.Pixels[m, n];
						}
					}

					buffer[i, j] = ProcessMatrix(matrix, kernelHeight, kernelWidth);
				}
			}

			bitmap.Pixels = buffer;
		}
	}
}
