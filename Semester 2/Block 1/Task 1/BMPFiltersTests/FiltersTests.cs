using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using BMPFilters;

namespace BMPFiltersTests
{
    [TestClass]
    public class FiltersTests
    {
		private BMPImage ResultImage;
		private BMPImage TargetImage;

		private static bool AreEqual(BMPImage ResultImage, BMPImage TargetImage)
		{
			if (ResultImage.Height != TargetImage.Height || ResultImage.Width != TargetImage.Width)
			{
				return false;
			}

			for (int i = 0; i < ResultImage.Height; i++)
			{
				for (int j = 0; j < ResultImage.Width; j++)
				{
					for (int c = 0; c < ResultImage.Channels; c++)
					{
						if (ResultImage.Bytes[i][j][c] != ResultImage.Bytes[i][j][c])
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

			ResultImage = new BMPImage(input);
			input.Close();
		}

		[TestMethod]
		public void ApplyMedianTest()
		{
			Filters.ApplyMedian(ResultImage);
			TargetImage = new BMPImage(new FileStream("../../../pictures/picture_median.bmp", FileMode.Open, FileAccess.Read));

			Assert.IsTrue(AreEqual(ResultImage, TargetImage));
		}

		[TestMethod]
		public void ApplyGaussTest()
		{
			Filters.ApplyGauss(ResultImage);
			TargetImage = new BMPImage(new FileStream("../../../pictures/picture_gauss.bmp", FileMode.Open, FileAccess.Read));

			Assert.IsTrue(AreEqual(ResultImage, TargetImage));
		}

		[TestMethod]
		public void ApplySobelXTest()
		{
			Filters.ApplySobelX(ResultImage);
			TargetImage = new BMPImage(new FileStream("../../../pictures/picture_sobelX.bmp", FileMode.Open, FileAccess.Read));

			Assert.IsTrue(AreEqual(ResultImage, TargetImage));
		}

		[TestMethod]
		public void ApplySobelYTest()
		{
			Filters.ApplySobelY(ResultImage);
			TargetImage = new BMPImage(new FileStream("../../../pictures/picture_sobelY.bmp", FileMode.Open, FileAccess.Read));

			Assert.IsTrue(AreEqual(ResultImage, TargetImage));
		}

		[TestMethod]
		public void ApplyGrayscaleTest()
		{
			Filters.ApplyGrayscale(ResultImage);
			TargetImage = new BMPImage(new FileStream("../../../pictures/picture_grayscale.bmp", FileMode.Open, FileAccess.Read));

			Assert.IsTrue(AreEqual(ResultImage, TargetImage));
		}
	}
}
