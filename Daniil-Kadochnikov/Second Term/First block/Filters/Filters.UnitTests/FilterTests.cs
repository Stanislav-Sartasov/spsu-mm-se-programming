using NUnit.Framework;

namespace Filters.UnitTests
{
	public class FilterTests
	{
		[Test]
		public void GreyFilterX24Test()
		{
			Image Image = new Image();
			Image.BitMapping("../../../Test/InputX24.bmp");
			new GreyFilter().ApplyingGreyFilter(Image);
			Image.SavingFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectGreyX24.bmp");
			Assert.Pass();
		}

		[Test]
		public void GreyFilterX32Test()
		{
			Image Image = new Image();
			Image.BitMapping("../../../Test/InputX32.bmp");
			new GreyFilter().ApplyingGreyFilter(Image);
			Image.SavingFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectGreyX32.bmp");
			Assert.Pass();
		}

		[Test]
		public void MedianFilterX24Test()
		{
			Image Image = new Image();
			Image.BitMapping("../../../Test/InputX24.bmp");
			new MedianFilter().ApplyingMedianFilter(Image, 3);
			Image.SavingFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectMedianX24.bmp");
			Assert.Pass();
		}

		[Test]
		public void MedianFilterX32Test()
		{
			Image Image = new Image();
			Image.BitMapping("../../../Test/InputX32.bmp");
			new MedianFilter().ApplyingMedianFilter(Image, 3);
			Image.SavingFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectMedianX32.bmp");
			Assert.Pass();
		}

		[Test]
		public void Gauss3FilterX24Test()
		{
			Image Image = new Image();
			Image.BitMapping("../../../Test/InputX24.bmp");
			Gauss3Filter Filter = new Gauss3Filter();
			Filter.MatrixFormalization(Image, 3, Filter.Kernel, Filter.Divider);
			Image.SavingFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectGauss3X24.bmp");
			Assert.Pass();
		}

		[Test]
		public void Gauss3FilterX32Test()
		{
			Image Image = new Image();
			Image.BitMapping("../../../Test/InputX32.bmp");
			Gauss3Filter Filter = new Gauss3Filter();
			Filter.MatrixFormalization(Image, 3, Filter.Kernel, Filter.Divider);
			Image.SavingFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectGauss3X32.bmp");
			Assert.Pass();
		}

		[Test]
		public void Gauss5FilterX24Test()
		{
			Image Image = new Image();
			Image.BitMapping("../../../Test/InputX24.bmp");
			Gauss5Filter Filter = new Gauss5Filter();
			Filter.MatrixFormalization(Image, 5, Filter.Kernel, Filter.Divider);
			Image.SavingFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectGauss5X24.bmp");
			Assert.Pass();
		}

		[Test]
		public void Gauss5FilterX32Test()
		{
			Image Image = new Image();
			Image.BitMapping("../../../Test/InputX32.bmp");
			Gauss5Filter Filter = new Gauss5Filter();
			Filter.MatrixFormalization(Image, 5, Filter.Kernel, Filter.Divider);
			Image.SavingFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectGauss5X32.bmp");
			Assert.Pass();
		}

		[Test]
		public void SobelXFilterX24Test()
		{
			Image Image = new Image();
			Image.BitMapping("../../../Test/InputX24.bmp");
			new GreyFilter().ApplyingGreyFilter(Image);
			SobelXFilter Filter = new SobelXFilter();
			Filter.MatrixFormalization(Image, 3, Filter.Kernel, Filter.Divider);
			Image.SavingFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectSobelXX24.bmp");
			Assert.Pass();
		}

		[Test]
		public void SobelXFilterX32Test()
		{
			Image Image = new Image();
			Image.BitMapping("../../../Test/InputX32.bmp");
			new GreyFilter().ApplyingGreyFilter(Image);
			SobelXFilter Filter = new SobelXFilter();
			Filter.MatrixFormalization(Image, 3, Filter.Kernel, Filter.Divider);
			Image.SavingFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectSobelXX32.bmp");
			Assert.Pass();
		}

		[Test]
		public void SobelYFilterX24Test()
		{
			Image Image = new Image();
			Image.BitMapping("../../../Test/InputX24.bmp");
			new GreyFilter().ApplyingGreyFilter(Image);
			SobelYFilter Filter = new SobelYFilter();
			Filter.MatrixFormalization(Image, 3, Filter.Kernel, Filter.Divider);
			Image.SavingFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectSobelYX24.bmp");
			Assert.Pass();
		}

		[Test]
		public void SobelYFilterX32Test()
		{
			Image Image = new Image();
			Image.BitMapping("../../../Test/InputX32.bmp");
			new GreyFilter().ApplyingGreyFilter(Image);
			SobelYFilter Filter = new SobelYFilter();
			Filter.MatrixFormalization(Image, 3, Filter.Kernel, Filter.Divider);
			Image.SavingFile("../../../Test/output.bmp");
			FileAssert.AreEqual("../../../Test/output.bmp", "../../../Test/CorrectSobelYX32.bmp");
			Assert.Pass();
		}
	}
}