using NUnit.Framework;

namespace Filters.UnitTests
{
	public class SobelXTests
	{
		[Test]
		public void SobelX24Test()
		{
			TestFilter.Test(new SobelX(), "input24.bmp", "sobelx24.bmp");

			Assert.Pass();
		}

		[Test]
		public void SobelX32Test()
		{
			TestFilter.Test(new SobelX(), "input32.bmp", "sobelx32.bmp");

			Assert.Pass();
		}
	}
}