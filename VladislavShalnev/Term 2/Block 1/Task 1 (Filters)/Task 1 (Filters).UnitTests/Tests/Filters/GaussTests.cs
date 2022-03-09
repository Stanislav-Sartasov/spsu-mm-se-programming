using NUnit.Framework;

namespace Filters.UnitTests
{
	public class GaussTests
	{
		[Test]
		public void Gauss24Test()
		{
			TestFilter.Test(new Gauss(), "input24.bmp", "gauss24.bmp");

			Assert.Pass();
		}

		[Test]
		public void Gauss32Test()
		{
			TestFilter.Test(new Gauss(), "input32.bmp", "gauss32.bmp");

			Assert.Pass();
		}
	}
}