using NUnit.Framework;

namespace Filters.UnitTests
{
	public class GrayscaleTests
	{
		[Test]
		public void Grayscale24Test()
		{
			TestFilter.Test(new Grayscale(), "input24.bmp", "grayscale24.bmp");

			Assert.Pass();
		}

		[Test]
		public void Grayscale32Test()
		{
			TestFilter.Test(new Grayscale(), "input32.bmp", "grayscale32.bmp");

			Assert.Pass();
		}
	}
}