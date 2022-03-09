using NUnit.Framework;
using System;

namespace Core.Image.UnitTests
{
	public class BitmapTests
	{
		private const string WORKING_DIR = "../../../Images/";
		private const string INPUT_DIR = WORKING_DIR + "input24.bmp";
		private const string OUTPUT_DIR = WORKING_DIR + "output24.bmp";

		private Bitmap? bitmap;

		[Test]
		public void ConstructorTest()
		{
			try
			{
				bitmap = new Bitmap(INPUT_DIR);
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
				bitmap?.Save(OUTPUT_DIR);

				FileAssert.AreEqual(INPUT_DIR, OUTPUT_DIR);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}

			Assert.Pass();
		}
	}
}