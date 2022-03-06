using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using BMPFilters;

namespace BMPFiltersTests
{
	[TestClass]
	public class FiltersTests
	{
		private BMPImage resultImage;
		private BMPImage targetImage;

		private static bool AreEqual(BMPImage resultImage, BMPImage targetImage)
		{
			if (resultImage.Height != targetImage.Height || resultImage.Width != targetImage.Width)
			{
				return false;
			}

			for (int i = 0; i < resultImage.Height; i++)
			{
				for (int j = 0; j < resultImage.Width; j++)
				{
					for (int c = 0; c < resultImage.Channels; c++)
					{
						if (resultImage.Bytes[i][j][c] != resultImage.Bytes[i][j][c])
						{
							return false;
						}
					}
				}
			}

			return true;
		}

		[TestInitialize]
		public void Initialize()
		{
			FileStream input = new FileStream("../../../pictures/picture.bmp", FileMode.Open, FileAccess.Read);

			resultImage = new BMPImage(input);
			input.Close();
		}

		[TestMethod]
		public void MedianTest()
		{
			Filters.Median(resultImage);
			targetImage = new BMPImage(new FileStream("../../../pictures/picture_median.bmp", FileMode.Open, FileAccess.Read));

			Assert.IsTrue(AreEqual(resultImage, targetImage));
		}

		[TestMethod]
		public void GaussTest()
		{
			Filters.Gauss(resultImage);
			targetImage = new BMPImage(new FileStream("../../../pictures/picture_gauss.bmp", FileMode.Open, FileAccess.Read));

			Assert.IsTrue(AreEqual(resultImage, targetImage));
		}

		[TestMethod]
		public void SobelXTest()
		{
			Filters.SobelX(resultImage);
			targetImage = new BMPImage(new FileStream("../../../pictures/picture_sobelX.bmp", FileMode.Open, FileAccess.Read));

			Assert.IsTrue(AreEqual(resultImage, targetImage));
		}

		[TestMethod]
		public void SobelYTest()
		{
			Filters.SobelY(resultImage);
			targetImage = new BMPImage(new FileStream("../../../pictures/picture_sobelY.bmp", FileMode.Open, FileAccess.Read));

			Assert.IsTrue(AreEqual(resultImage, targetImage));
		}

		[TestMethod]
		public void GrayscaleTest()
		{
			Filters.Grayscale(resultImage);
			targetImage = new BMPImage(new FileStream("../../../pictures/picture_grayscale.bmp", FileMode.Open, FileAccess.Read));

			Assert.IsTrue(AreEqual(resultImage, targetImage));
		}
	}
}
