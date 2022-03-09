using NUnit.Framework;

namespace Filters.UnitTests
{
	public class SobelYTests
	{
		[Test]
		public void SobelY24Test()
		{
			TestFilter.Test(new SobelY(), "input24.bmp", "sobely24.bmp");

			Assert.Pass();
		}

		[Test]
		public void SobelY32Test()
		{
			TestFilter.Test(new SobelY(), "input32.bmp", "sobely32.bmp");

			Assert.Pass();
		}
	}
}