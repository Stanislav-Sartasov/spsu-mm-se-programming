using NUnit.Framework;
using Task_1.Filters;

namespace Task_1.UnitTests.Filters
{
	public class GrayscaleTests
	{
		[Test]
		public void Grayscale24Test()
		{
			TestFilter.Test(new Grayscale(), Config.INPUT24_DIR, Config.OUTPUT_DIR);

			FileAssert.AreEqual(Config.OUTPUT_DIR, Config.GRAYSCALE24_DIR);

			Assert.Pass();
		}

		[Test]
		public void Grayscale32Test()
		{
			TestFilter.Test(new Grayscale(), Config.INPUT32_DIR, Config.OUTPUT_DIR);

			FileAssert.AreEqual(Config.OUTPUT_DIR, Config.GRAYSCALE32_DIR);

			Assert.Pass();
		}
	}
}