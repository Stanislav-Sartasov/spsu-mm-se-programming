using NUnit.Framework;

namespace Core.Image.UnitTests
{
	public class PixelTests
	{

		[Test]
		public void ConstructorTest()
		{
			(byte red, byte green, byte blue) = (255, 127, 0);

			Pixel pixel = new Pixel(red, green, blue);

			Assert.AreEqual(pixel.Red, red);
			Assert.AreEqual(pixel.Green, green);
			Assert.AreEqual(pixel.Blue, blue);

			Assert.Pass();
		}

		[Test]
		public void DestructorTest()
		{
			(byte red, byte green, byte blue) = (228, 15, 36);

			Pixel pixel = new Pixel(red, green, blue);

			(byte newRed, byte newGreen, byte newBlue) = pixel;

			Assert.AreEqual(newRed, red);
			Assert.AreEqual(newGreen, green);
			Assert.AreEqual(newBlue, blue);

			Assert.Pass();
		}

		[Test]
		public void EqualsTest()
		{
			(byte red, byte green, byte blue) = (133, 74, 20);

			Pixel pixel1 = new Pixel(red, green, blue);
			Pixel pixel2 = new Pixel(red, green, blue);

			Assert.AreEqual(pixel1, pixel2);

			Assert.Pass();
		}

		[Test]
		public void MultiplicationTest()
		{
			(byte red, byte green, byte blue) = (120, 33, 10);

			Pixel pixel1 = new Pixel(red, green, blue);
			Pixel pixel2 = new Pixel(red * 2, green * 2, blue * 2);

			Assert.AreEqual(pixel1 * 2, pixel2);

			Assert.Pass();
		}

		[Test]
		public void DivisionTest()
		{
			(byte red, byte green, byte blue) = (66, 33, 50);

			Pixel pixel1 = new Pixel(red, green, blue);
			Pixel pixel2 = new Pixel(red / 3, green / 3, blue / 3);

			Assert.AreEqual(pixel1 / 3, pixel2);

			Assert.Pass();
		}

		[Test]
		public void AddTest()
		{
			(byte red1, byte green1, byte blue1) = (56, 233, 14);
			(byte red2, byte green2, byte blue2) = (66, 10, 77);

			Pixel pixel1 = new Pixel(red1, green1, blue1);
			Pixel pixel2 = new Pixel(red2, green2, blue2);
			Pixel pixel3 = new Pixel(red1 + red2, green1 + green2, blue1 + blue2);

			Assert.AreEqual(pixel1 + pixel2, pixel3);

			Assert.Pass();
		}
	}
}