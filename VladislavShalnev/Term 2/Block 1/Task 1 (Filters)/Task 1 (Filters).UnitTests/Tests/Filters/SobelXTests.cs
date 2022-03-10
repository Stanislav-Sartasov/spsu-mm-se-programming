using NUnit.Framework;
using Task_1.UnitTests;

namespace Filters.UnitTests
{
	public class SobelXTests
	{
		[Test]
		public void SobelX24Test()
		{
			TestFilter.Test(new SobelX(), Config.INPUT24_DIR, Config.OUTPUT_DIR);

			FileAssert.AreEqual(Config.OUTPUT_DIR, Config.SOBELX24_DIR);

			Assert.Pass();
		}

		[Test]
		public void SobelX32Test()
		{
			TestFilter.Test(new SobelX(), Config.INPUT32_DIR, Config.OUTPUT_DIR);

			FileAssert.AreEqual(Config.OUTPUT_DIR, Config.SOBELX32_DIR);

			Assert.Pass();
		}
	}
}