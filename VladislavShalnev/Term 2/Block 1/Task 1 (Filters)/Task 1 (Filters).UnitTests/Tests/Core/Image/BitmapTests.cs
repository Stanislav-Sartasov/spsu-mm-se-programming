using NUnit.Framework;
using System;
using Task_1.UnitTests;

namespace Core.Image.UnitTests
{
	public class BitmapTests
	{
		private Bitmap? bitmap;

		[Test]
		public void ConstructorTest()
		{
			try
			{
				bitmap = new Bitmap(Config.INPUT24_DIR);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}

			Assert.Pass();
		}

		[Test]
		public void SaveTest()
		{
			try
			{
				bitmap?.Save(Config.OUTPUT_DIR);

				FileAssert.AreEqual(Config.INPUT24_DIR, Config.OUTPUT_DIR);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}

			Assert.Pass();
		}
	}
}