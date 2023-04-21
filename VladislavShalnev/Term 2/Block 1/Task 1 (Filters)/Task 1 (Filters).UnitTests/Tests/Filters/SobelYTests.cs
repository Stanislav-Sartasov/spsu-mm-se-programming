using NUnit.Framework;
using Task_1.Filters;

namespace Task_1.UnitTests.Filters
{
	public class SobelYTests
	{
		[Test]
		public void SobelY24Test()
		{
			TestFilter.Test(new SobelY(), Config.INPUT24_DIR, Config.OUTPUT_DIR);

			FileAssert.AreEqual(Config.OUTPUT_DIR, Config.SOBELY24_DIR);

			Assert.Pass();
		}

		[Test]
		public void SobelY32Test()
		{
			TestFilter.Test(new SobelY(), Config.INPUT32_DIR, Config.OUTPUT_DIR);

			FileAssert.AreEqual(Config.OUTPUT_DIR, Config.SOBELY32_DIR);

			Assert.Pass();
		}
	}
}