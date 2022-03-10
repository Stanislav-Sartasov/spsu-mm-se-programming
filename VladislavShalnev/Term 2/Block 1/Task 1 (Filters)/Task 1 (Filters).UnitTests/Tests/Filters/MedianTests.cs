using NUnit.Framework;
using Task_1.UnitTests;

namespace Filters.UnitTests
{
	public class MedianTests
	{
		[Test]
		public void Median24Test()
		{
			TestFilter.Test(new Median(), Config.INPUT24_DIR, Config.OUTPUT_DIR);

			FileAssert.AreEqual(Config.OUTPUT_DIR, Config.MEDIAN24_DIR);

			Assert.Pass();
		}

		[Test]
		public void Median32Test()
		{
			TestFilter.Test(new Median(), Config.INPUT32_DIR, Config.OUTPUT_DIR);

			FileAssert.AreEqual(Config.OUTPUT_DIR, Config.MEDIAN32_DIR);

			Assert.Pass();
		}
	}
}