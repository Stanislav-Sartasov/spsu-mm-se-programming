using NUnit.Framework;
using Filters;

namespace Filters.UnitTests
{
	public class FilterTests
	{
		[Test]
		public void TestGaussFilter()
		{
			Image image = new Image("../../../tests/input.bmp");
			Program.Gauss(image);
			image.SavePicture("../../../tests/output.bmp");
			FileAssert.AreEqual("../../../tests/output.bmp", "../../../tests/gauss_true.bmp");
			Assert.Pass();
		}

		[Test]
		public void TestGreyFilter()
		{
			Image image = new Image("../../../tests/input.bmp");
			Program.GrayFilter(image);
			image.SavePicture("../../../tests/output.bmp");
			FileAssert.AreEqual("../../../tests/output.bmp", "../../../tests/gray_true.bmp");
			Assert.Pass();
		}

		[Test]
		public void TestSobelX()
		{
			Image image = new Image("../../../tests/input.bmp");
			Program.SobelX(image);
			image.SavePicture("../../../tests/output.bmp");
			FileAssert.AreEqual("../../../tests/output.bmp", "../../../tests/sobelx_true.bmp");
			Assert.Pass();
		}

		[Test]
		public void TestSobelY()
		{
			Image image = new Image("../../../tests/input.bmp");
			Program.SobelY(image);
			image.SavePicture("../../../tests/output.bmp");
			FileAssert.AreEqual("../../../tests/output.bmp", "../../../tests/sobely_true.bmp");
			Assert.Pass();
		}

		[Test]
		public void TestMedian()
		{
			Image exemplar = new Image("../../../tests/input.bmp");
			Program.Median(exemplar);
			exemplar.SavePicture("../../../tests/output.bmp");
			FileAssert.AreEqual("../../../tests/output.bmp", "../../../tests/median_true.bmp");
			Assert.Pass();
		}

		[Test]
		public void TestReadWrite()
		{
			Image exemplar = new Image("../../../tests//input.bmp");
			exemplar.SavePicture("../../../tests/output.bmp");
			FileAssert.AreEqual("../../../tests/input.bmp", "../../../tests/output.bmp");
			Assert.Pass();
		}

		[Test]
		public void TestGetSetPixel()
		{
			Image exemplar = new Image("../../../tests/input.bmp");
			Pixel pixel = new Pixel(50, 50, 50);
			exemplar.SetPixel(pixel, 0, 0);
			Assert.AreEqual(pixel.Red, exemplar.GetPixel(0, 0).Red);
			Assert.AreEqual(pixel.Green, exemplar.GetPixel(0, 0).Green);
			Assert.AreEqual(pixel.Blue, exemplar.GetPixel(0, 0).Blue);
			Assert.Pass();
		}
	}
}