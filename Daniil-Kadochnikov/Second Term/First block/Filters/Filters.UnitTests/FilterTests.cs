using NUnit.Framework;

namespace Filters.UnitTests
{
	public class FilterTests
	{
		[Test]
		public void GreyFilterX24Test()
		{
			Image image = new Image();
			image.BitMap("../../../Test/InputX24.bmp");
			new GreyFilter().ApplyGreyFilter(image);
			image.SaveFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectGreyX24.bmp");
			Assert.Pass();
		}

		[Test]
		public void GreyFilterX32Test()
		{
			Image image = new Image();
			image.BitMap("../../../Test/InputX32.bmp");
			new GreyFilter().ApplyGreyFilter(image);
			image.SaveFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectGreyX32.bmp");
			Assert.Pass();
		}

		[Test]
		public void MedianFilterX24Test()
		{
			Image image = new Image();
			image.BitMap("../../../Test/InputX24.bmp");
			new MedianFilter().ApplyMedianFilter(image, 3);
			image.SaveFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectMedianX24.bmp");
			Assert.Pass();
		}

		[Test]
		public void MedianFilterX32Test()
		{
			Image image = new Image();
			image.BitMap("../../../Test/InputX32.bmp");
			new MedianFilter().ApplyMedianFilter(image, 3);
			image.SaveFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectMedianX32.bmp");
			Assert.Pass();
		}

		[Test]
		public void Gauss3FilterX24Test()
		{
			Image image = new Image();
			image.BitMap("../../../Test/InputX24.bmp");
			Gauss3Filter filter = new Gauss3Filter();
			filter.FormalizeMatrix(image, 3, filter.Kernel, filter.Divider);
			image.SaveFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectGauss3X24.bmp");
			Assert.Pass();
		}

		[Test]
		public void Gauss3FilterX32Test()
		{
			Image image = new Image();
			image.BitMap("../../../Test/InputX32.bmp");
			Gauss3Filter filter = new Gauss3Filter();
			filter.FormalizeMatrix(image, 3, filter.Kernel, filter.Divider);
			image.SaveFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectGauss3X32.bmp");
			Assert.Pass();
		}

		[Test]
		public void Gauss5FilterX24Test()
		{
			Image image = new Image();
			image.BitMap("../../../Test/InputX24.bmp");
			Gauss5Filter filter = new Gauss5Filter();
			filter.FormalizeMatrix(image, 5, filter.Kernel, filter.Divider);
			image.SaveFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectGauss5X24.bmp");
			Assert.Pass();
		}

		[Test]
		public void Gauss5FilterX32Test()
		{
			Image image = new Image();
			image.BitMap("../../../Test/InputX32.bmp");
			Gauss5Filter filter = new Gauss5Filter();
			filter.FormalizeMatrix(image, 5, filter.Kernel, filter.Divider);
			image.SaveFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectGauss5X32.bmp");
			Assert.Pass();
		}

		[Test]
		public void SobelXFilterX24Test()
		{
			Image image = new Image();
			image.BitMap("../../../Test/InputX24.bmp");
			new GreyFilter().ApplyGreyFilter(image);
			SobelXFilter filter = new SobelXFilter();
			filter.FormalizeMatrix(image, 3, filter.Kernel, filter.Divider);
			image.SaveFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectSobelXX24.bmp");
			Assert.Pass();
		}

		[Test]
		public void SobelXFilterX32Test()
		{
			Image image = new Image();
			image.BitMap("../../../Test/InputX32.bmp");
			new GreyFilter().ApplyGreyFilter(image);
			SobelXFilter filter = new SobelXFilter();
			filter.FormalizeMatrix(image, 3, filter.Kernel, filter.Divider);
			image.SaveFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectSobelXX32.bmp");
			Assert.Pass();
		}

		[Test]
		public void SobelYFilterX24Test()
		{
			Image image = new Image();
			image.BitMap("../../../Test/InputX24.bmp");
			new GreyFilter().ApplyGreyFilter(image);
			SobelYFilter filter = new SobelYFilter();
			filter.FormalizeMatrix(image, 3, filter.Kernel, filter.Divider);
			image.SaveFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectSobelYX24.bmp");
			Assert.Pass();
		}

		[Test]
		public void SobelYFilterX32Test()
		{
			Image image = new Image();
			image.BitMap("../../../Test/InputX32.bmp");
			new GreyFilter().ApplyGreyFilter(image);
			SobelYFilter filter = new SobelYFilter();
			filter.FormalizeMatrix(image, 3, filter.Kernel, filter.Divider);
			image.SaveFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectSobelYX32.bmp");
			Assert.Pass();
		}
	}
}