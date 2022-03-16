using NUnit.Framework;
using System.IO;

namespace ImageFilters.UnitTests
{
	public class BitmapTests
	{
		[Test]
		public void Read24bitImageTest()
		{
			Bitmap bmp = new();
			using (MemoryStream ms = new MemoryStream(ImageResources.Original24bit))
			{
				bmp.ReadBitmap(ms);
			}
			Assert.IsTrue(bmp.CheckIfLoaded());
		}

		[Test]
		public void Write32bitImageTest()
		{
			Bitmap bmp = new();
			using (MemoryStream ms = new MemoryStream(ImageResources.Original32bit))
			{
				bmp.ReadBitmap(ms);
			}
			byte[] output = new byte[ImageResources.Original32bit.Length];
			using (MemoryStream ms = new MemoryStream(output))
			{
				bmp.WriteBitmap(ms);
			}
			Assert.AreEqual(ImageResources.Original32bit, output);
		}
	}
}
