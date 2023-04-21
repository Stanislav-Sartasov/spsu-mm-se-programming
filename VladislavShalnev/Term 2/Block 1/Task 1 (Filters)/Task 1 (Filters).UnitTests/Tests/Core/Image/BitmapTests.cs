using NUnit.Framework;
using System;
using Task_1.Core.Image;

namespace Task_1.UnitTests.Core.Image
{
	public class BitmapTests
	{
		[Test]
		public void ConstructorTest()
		{
			try
			{
				Bitmap bitmap = new Bitmap(Config.INPUT24_DIR);
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
				Bitmap bitmap = new Bitmap(Config.INPUT24_DIR);

				bitmap.Save(Config.OUTPUT_DIR);

				FileAssert.AreEqual(Config.INPUT24_DIR, Config.OUTPUT_DIR);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}

			Assert.Pass();
		}

		[Test]
		public void CloneTest()
		{
			try
			{
				Bitmap bitmap1 = new Bitmap(Config.INPUT24_DIR);
				Bitmap bitmap2 = (Bitmap)bitmap1.Clone();

				Assert.AreNotEqual(bitmap1, bitmap2);

				for (int i = 0; i < bitmap1.Height; i++)
					for (int j = 0; j < bitmap1.Width; j++)
						Assert.AreEqual(bitmap1[i, j], bitmap2[i, j]);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}

			Assert.Pass();
		}

		[Test]
		public void GetPixelTest()
		{
			try
			{
				Bitmap bitmap = new Bitmap(Config.INPUT24_DIR);

				Pixel pixel1 = bitmap[228, 133];
				Pixel pixel2 = new Pixel(120, 210, 236);

				Assert.AreEqual(pixel1, pixel2);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}

			Assert.Pass();
		}

		[Test]
		public void SetPixelTest()
		{
			try
			{
				Bitmap bitmap = new Bitmap(Config.INPUT24_DIR);

				Pixel pixel1 = new Pixel(255, 10, 15);

				bitmap[66, 420] = pixel1;

				Pixel pixel2 = bitmap[66, 420];

				Assert.AreEqual(pixel1, pixel2);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}

			Assert.Pass();
		}
	}
}