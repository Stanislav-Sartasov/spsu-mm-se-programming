using NUnit.Framework;
using System;
using System.IO;
using Task_1._1;

namespace Task_1._1.UnitTests
{
    public class BitmapTests
    {
        private string TestFilenameIn = "testfiles/in.bmp";
        private string TestFilenameOut = "testfiles/out.bmp";

        private Bitmap? bitmap;

        [Test]
        public void Reading()
        {
            // Reading bitmap from path
            try
            {
                bitmap = new Bitmap(TestFilenameIn);
            }
            catch (Exception exception) 
            {
                Assert.Fail(exception.Message);
            }
            Assert.Pass();
        }

        [Test]
        public void Writing()
        {
            // Reading bitmap from path
            try
            {
                bitmap = new Bitmap(TestFilenameIn);
                bitmap.Save(TestFilenameOut);
                FileAssert.AreEqual(TestFilenameOut, TestFilenameIn);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }

            Assert.Pass();
        }

        [Test]
        public void PixelGet()
        {
            Bitmap bitmap = new Bitmap(TestFilenameIn);
            Pixel result = bitmap.GetPixel(0, 0);
            Assert.IsTrue(result == new Pixel(17, 15, 15));

            Assert.Pass();
        }

        [Test]
        public void PixelSet()
        {
            Bitmap bitmap = new Bitmap(TestFilenameIn);
            bitmap.SetPixel(0, 0, new Pixel(255, 255, 255));
            Pixel result = bitmap.GetPixel(0, 0);
            Assert.IsTrue(result == new Pixel(255, 255, 255));

            Assert.Pass();
        }
    }
}