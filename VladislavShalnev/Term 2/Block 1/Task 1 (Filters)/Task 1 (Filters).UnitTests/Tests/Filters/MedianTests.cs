using NUnit.Framework;

namespace Filters.UnitTests
{
	public class MedianTests
	{
		[Test]
		public void Median24Test()
		{
			TestFilter.Test(new Median(), "input24.bmp", "median24.bmp");

			Assert.Pass();
		}

		[Test]
		public void Median32Test()
		{
			TestFilter.Test(new Median(), "input32.bmp", "median32.bmp");

			Assert.Pass();
		}
	}
}