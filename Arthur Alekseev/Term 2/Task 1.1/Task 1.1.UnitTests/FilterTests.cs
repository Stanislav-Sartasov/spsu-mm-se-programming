using System;
using NUnit.Framework;

namespace Task_1._1.UnitTests
{
    internal class FilterTests
    {
        [Test]
        public void SobelXTest24()
        {
            Bitmap bmp = new Bitmap("testfiles/image24.bmp");
            new SobelXFilter().ProcessBitmap(bmp);
            bmp.Save("testfiles/output.bmp");

            FileAssert.AreEqual("testfiles/output.bmp", "testfiles/sobelX24.bmp");

            Assert.Pass();
        }

        [Test]
        public void SobelXTest32()
        {
            Bitmap bmp = new Bitmap("testfiles/image32.bmp");
            new SobelXFilter().ProcessBitmap(bmp);
            bmp.Save("testfiles/output.bmp");

            FileAssert.AreEqual("testfiles/output.bmp", "testfiles/sobelX32.bmp");

            Assert.Pass();
        }

        [Test]
        public void SobelYTest24()
        {
            Bitmap bmp = new Bitmap("testfiles/image24.bmp");
            new SobelYFilter().ProcessBitmap(bmp);
            bmp.Save("testfiles/output.bmp");

            FileAssert.AreEqual("testfiles/output.bmp", "testfiles/sobelY24.bmp");

            Assert.Pass();
        }
        [Test]
        public void SobelYTest32()
        {
            Bitmap bmp = new Bitmap("testfiles/image32.bmp");
            new SobelYFilter().ProcessBitmap(bmp);
            bmp.Save("testfiles/output.bmp");

            FileAssert.AreEqual("testfiles/output.bmp", "testfiles/sobelY32.bmp");

            Assert.Pass();
        }

        [Test]
        public void GrayScaleTest24()
        {
            Bitmap bmp = new Bitmap("testfiles/image24.bmp");
            new GrayScale().ProcessBitmap(bmp);
            bmp.Save("testfiles/output.bmp");

            FileAssert.AreEqual("testfiles/output.bmp", "testfiles/grayscale24.bmp");

            Assert.Pass();
        }

        [Test]
        public void GrayScaleTest32()
        {
            Bitmap bmp = new Bitmap("testfiles/image32.bmp");
            new GrayScale().ProcessBitmap(bmp);
            bmp.Save("testfiles/output.bmp");

            FileAssert.AreEqual("testfiles/output.bmp", "testfiles/grayscale32.bmp");

            Assert.Pass();
        }

        [Test]
        public void MedianTest24()
        {
            Bitmap bmp = new Bitmap("testfiles/image24.bmp");
            new MedianFilter().ProcessBitmap(bmp);
            bmp.Save("testfiles/output.bmp");

            FileAssert.AreEqual("testfiles/output.bmp", "testfiles/median24.bmp");

            Assert.Pass();
        }

        [Test]
        public void MedianTest32()
        {
            Bitmap bmp = new Bitmap("testfiles/image32.bmp");
            new MedianFilter().ProcessBitmap(bmp);
            bmp.Save("testfiles/output.bmp");

            FileAssert.AreEqual("testfiles/output.bmp", "testfiles/median32.bmp");

            Assert.Pass();
        }
        [Test]
        public void GaussTest24()
        {
            Bitmap bmp = new Bitmap("testfiles/image24.bmp");
            new GaussFilter().ProcessBitmap(bmp);
            bmp.Save("testfiles/output.bmp");

            FileAssert.AreEqual("testfiles/output.bmp", "testfiles/gauss24.bmp");

            Assert.Pass();
        }

        [Test]
        public void GaussTest32()
        {
            Bitmap bmp = new Bitmap("testfiles/image32.bmp");
            new GaussFilter().ProcessBitmap(bmp);
            bmp.Save("testfiles/output.bmp");

            FileAssert.AreEqual("testfiles/output.bmp", "testfiles/gauss32.bmp");

            Assert.Pass();
        }
        [Test]
        public void HugeGaussTest24()
        {
            Bitmap bmp = new Bitmap("testfiles/image24.bmp");
            for(int i = 0; i < 20; i++)
                new GaussFilter().ProcessBitmap(bmp);
            bmp.Save("testfiles/output.bmp");

            FileAssert.AreEqual("testfiles/output.bmp", "testfiles/hugegauss24.bmp");

            Assert.Pass();
        }

        [Test]
        public void HugeGaussTest32()
        {
            Bitmap bmp = new Bitmap("testfiles/image32.bmp");
            for (int i = 0; i < 20; i++)
                new GaussFilter().ProcessBitmap(bmp);
            bmp.Save("testfiles/output.bmp");

            FileAssert.AreEqual("testfiles/output.bmp", "testfiles/hugegauss32.bmp");

            Assert.Pass();
        }
    }
}
