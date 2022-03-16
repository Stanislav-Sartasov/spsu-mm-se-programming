using NUnit.Framework;
using System.IO;

namespace ImageFilters.UnitTests
{
	public class FilterTests
	{
		private byte[] GetFilteredImage(string filterName, byte[] image)
		{
			Bitmap bmp = new();
			using (MemoryStream ms = new MemoryStream(image))
			{
				bmp.ReadBitmap(ms);
			}
			Filters.ApplyFilterByName(filterName, bmp);
			byte[] output = new byte[image.Length];
			using (MemoryStream ms = new MemoryStream(output))
			{
				bmp.WriteBitmap(ms);
			}
			return output;
		}

		[Test]
		public void MedianFilter24Test()
		{
			Assert.AreEqual(ImageResources.Median24bitOld, GetFilteredImage("median", ImageResources.Original24bit));
		}

		[Test]
		public void MedianFilter32Test()
		{
			Assert.AreEqual(ImageResources.Median32bitOld, GetFilteredImage("median", ImageResources.Original32bit));
		}

		[Test]
		public void GaussFilter24Test()
		{
			Assert.AreEqual(ImageResources.Gauss24bitOld, GetFilteredImage("gauss", ImageResources.Original24bit));
		}

		[Test]
		public void GaussFilter32Test()
		{
			Assert.AreEqual(ImageResources.Gauss32bitOld, GetFilteredImage("gauss", ImageResources.Original32bit));
		}

		[Test]
		public void GrayscaleFilter24Test()
		{
			Assert.AreEqual(ImageResources.Grayscale24bitOld, GetFilteredImage("grayscale", ImageResources.Original24bit));
		}

		[Test]
		public void GrayscaleFilter32Test()
		{
			Assert.AreEqual(ImageResources.Grayscale32bitOld, GetFilteredImage("grayscale", ImageResources.Original32bit));
		}

		[Test]
		public void SobelXFilter24Test()
		{
			Assert.AreEqual(ImageResources.SobelX24bitOld, GetFilteredImage("sobelx", ImageResources.Original24bit));
		}

		[Test]
		public void SobelXFilter32Test()
		{
			Assert.AreEqual(ImageResources.SobelX32bitOld, GetFilteredImage("sobelx", ImageResources.Original32bit));
		}

		[Test]
		public void SobelYFilter24Test()
		{
			Assert.AreEqual(ImageResources.SobelY24bitOld, GetFilteredImage("sobely", ImageResources.Original24bit));
		}

		[Test]
		public void SobelYFilter32Test()
		{
			Assert.AreEqual(ImageResources.SobelY32bitOld, GetFilteredImage("sobely", ImageResources.Original32bit));
		}
	}
}