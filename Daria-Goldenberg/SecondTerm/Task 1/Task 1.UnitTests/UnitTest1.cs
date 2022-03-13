using NUnit.Framework;
using Task_1;

namespace Task_1.UnitTests
{
	public class Task_1_Tests
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
		public void TestPixel()
		{
			BMPImage image = new BMPImage("../../../TestFiles/input24.bmp");
			Pixel pixel = new Pixel(24, 25, 26);
			image.SetPixel(pixel, 0, 0);
			Assert.AreEqual(pixel.Red, image.GetPixel(0, 0).Red);
			Assert.AreEqual(pixel.Green, image.GetPixel(0, 0).Green);
			Assert.AreEqual(pixel.Blue, image.GetPixel(0, 0).Blue);
			Assert.Pass();
		}

		

		[Test]
		public void TestGrayFilter24()
		{
			BMPImage image = new BMPImage("../../../TestFiles/input24.bmp");
			GrayFilter.Gray(image);
			image.WriteToFile("../../../TestFiles/output.bmp");
			FileAssert.AreEqual("../../../TestFiles/output.bmp", "../../../TestFiles/Gray24.bmp");
			Assert.Pass();
		}

		[Test]
		public void TestGrayFilter32()
		{
			BMPImage image = new BMPImage("../../../TestFiles/input32.bmp");
			GrayFilter.Gray(image);
			image.WriteToFile("../../../TestFiles/output.bmp");
			FileAssert.AreEqual("../../../TestFiles/output.bmp", "../../../TestFiles/Gray32.bmp");
			Assert.Pass();
		}

		[Test]
		public void TestMedianFilter24()
		{
			BMPImage image = new BMPImage("../../../TestFiles/input24.bmp");
			MedianFilter.Median(image);
			image.WriteToFile("../../../TestFiles/output.bmp");
			FileAssert.AreEqual("../../../TestFiles/output.bmp", "../../../TestFiles/Median24.bmp");
			Assert.Pass();
		}

		[Test]
		public void TestMedianFilter32()
		{
			BMPImage image = new BMPImage("../../../TestFiles/input32.bmp");
			MedianFilter.Median(image);
			image.WriteToFile("../../../TestFiles/output.bmp");
			FileAssert.AreEqual("../../../TestFiles/output.bmp", "../../../TestFiles/Median32.bmp");
			Assert.Pass();
		}

		[Test]
		public void TestGaussFilter24()
		{
			BMPImage image = new BMPImage("../../../TestFiles/input24.bmp");
			KernelFilters.Gauss(image);
			image.WriteToFile("../../../TestFiles/output.bmp");
			FileAssert.AreEqual("../../../TestFiles/output.bmp", "../../../TestFiles/Gauss24.bmp");
			Assert.Pass();
		}

		[Test]
		public void TestGaussFilter32()
		{
			BMPImage image = new BMPImage("../../../TestFiles/input32.bmp");
			KernelFilters.Gauss(image);
			image.WriteToFile("../../../TestFiles/output.bmp");
			FileAssert.AreEqual("../../../TestFiles/output.bmp", "../../../TestFiles/Gauss32.bmp");
			Assert.Pass();
		}

		[Test]
		public void TestSobelXFilter24()
		{
			BMPImage image = new BMPImage("../../../TestFiles/input24.bmp");
			KernelFilters.SobelX(image);
			image.WriteToFile("../../../TestFiles/output.bmp");
			FileAssert.AreEqual("../../../TestFiles/output.bmp", "../../../TestFiles/SobelX24.bmp");
			Assert.Pass();
		}

		[Test]
		public void TestSobelXFilter32()
		{
			BMPImage image = new BMPImage("../../../TestFiles/input32.bmp");
			KernelFilters.SobelX(image);
			image.WriteToFile("../../../TestFiles/output.bmp");
			FileAssert.AreEqual("../../../TestFiles/output.bmp", "../../../TestFiles/SobelX32.bmp");
			Assert.Pass();
		}
		[Test]
		public void TestSobelYFilter24()
		{
			BMPImage image = new BMPImage("../../../TestFiles/input24.bmp");
			KernelFilters.SobelY(image);
			image.WriteToFile("../../../TestFiles/output.bmp");
			FileAssert.AreEqual("../../../TestFiles/output.bmp", "../../../TestFiles/SobelY24.bmp");
			Assert.Pass();
		}

		[Test]
		public void TestSobelYFilter32()
		{
			BMPImage image = new BMPImage("../../../TestFiles/input32.bmp");
			KernelFilters.SobelY(image);
			image.WriteToFile("../../../TestFiles/output.bmp");
			FileAssert.AreEqual("../../../TestFiles/output.bmp", "../../../TestFiles/SobelY32.bmp");
			Assert.Pass();
		}
	}
}