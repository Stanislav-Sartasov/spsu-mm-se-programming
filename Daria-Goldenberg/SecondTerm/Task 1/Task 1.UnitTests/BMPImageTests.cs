using NUnit.Framework;
using Task_1;

namespace Task_1.UnitTests
{
    public class BMPImageTests
    {
		[Test]
		public void TestReadWriteFile()
		{
			BMPImage image = new BMPImage("../../../TestFiles/input24.bmp");
			image.WriteToFile("../../../TestFiles/output.bmp");
			FileAssert.AreEqual("../../../TestFiles/input24.bmp", "../../../TestFiles/output.bmp");
			Assert.Pass();
		}

		[Test]
		public void TestGetPixel()
		{
			BMPImage image = new BMPImage("../../../TestFiles/input24.bmp");
			Pixel result = image.GetPixel(128, 128);
			Assert.AreEqual(new Pixel(15, 3, 1).Red, result.Red);
			Assert.AreEqual(new Pixel(15, 3, 1).Green, result.Green);
			Assert.AreEqual(new Pixel(15, 3, 1).Blue, result.Blue);
			Assert.Pass();
		}

		[Test]
		public void TestSetPixel()
		{
			BMPImage image = new BMPImage("../../../TestFiles/input24.bmp");
			Pixel pixel = new Pixel(15, 3, 1);
			image.SetPixel(pixel, 0, 0);
			Pixel result = image.GetPixel(128, 128);
			Assert.AreEqual(pixel.Red, result.Red);
			Assert.AreEqual(pixel.Green, result.Green);
			Assert.AreEqual(pixel.Blue, result.Blue);
			Assert.Pass();
		}
	}
}
