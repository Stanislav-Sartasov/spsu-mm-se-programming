using NUnit.Framework;
using Task_1.Filters;

namespace Task_1.UnitTests.Filters
{
	public class GaussTests
	{
		[Test]
		public void Gauss24Test()
		{
			TestFilter.Test(new Gauss(), Config.INPUT24_DIR, Config.OUTPUT_DIR);

			FileAssert.AreEqual(Config.OUTPUT_DIR, Config.GAUSS24_DIR);

			Assert.Pass();
		}

		[Test]
		public void Gauss32Test()
		{
			TestFilter.Test(new Gauss(), Config.INPUT32_DIR, Config.OUTPUT_DIR);

			FileAssert.AreEqual(Config.OUTPUT_DIR, Config.GAUSS32_DIR);

			Assert.Pass();
		}
	}
}