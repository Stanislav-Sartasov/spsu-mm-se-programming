using System;
using NUnit.Framework;

namespace Task_1._1.UnitTests
{
    internal class FilterTests
    {
        public string WorkingDir = "../../../testfiles/";

        [Test]
        public void SobelXTest24()
        {
            Bitmap bmp = new Bitmap(WorkingDir + "image24.bmp");
            new SobelXFilter().ProcessBitmap(bmp);
            bmp.Save(WorkingDir + "output.bmp");

            FileAssert.AreEqual(WorkingDir + "output.bmp", WorkingDir + "sobelX24.bmp");

            Assert.Pass();
        }

        [Test]
        public void SobelXTest32()
        {
            Bitmap bmp = new Bitmap(WorkingDir + "image32.bmp");
            new SobelXFilter().ProcessBitmap(bmp);
            bmp.Save(WorkingDir + "output.bmp");

            FileAssert.AreEqual(WorkingDir + "output.bmp", WorkingDir + "sobelX32.bmp");

            Assert.Pass();
        }

        [Test]
        public void SobelYTest24()
        {
            Bitmap bmp = new Bitmap(WorkingDir + "image24.bmp");
            new SobelYFilter().ProcessBitmap(bmp);
            bmp.Save(WorkingDir + "output.bmp");

            FileAssert.AreEqual(WorkingDir + "output.bmp", WorkingDir + "sobelY24.bmp");

            Assert.Pass();
        }
        [Test]
        public void SobelYTest32()
        {
            Bitmap bmp = new Bitmap(WorkingDir + "image32.bmp");
            new SobelYFilter().ProcessBitmap(bmp);
            bmp.Save(WorkingDir + "output.bmp");

            FileAssert.AreEqual(WorkingDir + "output.bmp", WorkingDir + "sobelY32.bmp");

            Assert.Pass();
        }

        [Test]
        public void GrayScaleTest24()
        {
            Bitmap bmp = new Bitmap(WorkingDir + "image24.bmp");
            new GrayScale().ProcessBitmap(bmp);
            bmp.Save(WorkingDir + "output.bmp");

            FileAssert.AreEqual(WorkingDir + "output.bmp", WorkingDir + "grayscale24.bmp");

            Assert.Pass();
        }

        [Test]
        public void GrayScaleTest32()
        {
            Bitmap bmp = new Bitmap(WorkingDir + "image32.bmp");
            new GrayScale().ProcessBitmap(bmp);
            bmp.Save(WorkingDir + "output.bmp");

            FileAssert.AreEqual(WorkingDir + "output.bmp", WorkingDir + "grayscale32.bmp");

            Assert.Pass();
        }

        [Test]
        public void MedianTest24()
        {
            Bitmap bmp = new Bitmap(WorkingDir + "image24.bmp");
            new MedianFilter().ProcessBitmap(bmp);
            bmp.Save(WorkingDir + "output.bmp");

            FileAssert.AreEqual(WorkingDir + "output.bmp", WorkingDir + "median24.bmp");

            Assert.Pass();
        }

        [Test]
        public void MedianTest32()
        {
            Bitmap bmp = new Bitmap(WorkingDir + "image32.bmp");
            new MedianFilter().ProcessBitmap(bmp);
            bmp.Save(WorkingDir + "output.bmp");

            FileAssert.AreEqual(WorkingDir + "output.bmp", WorkingDir + "median32.bmp");

            Assert.Pass();
        }
        [Test]
        public void GaussTest24()
        {
            Bitmap bmp = new Bitmap(WorkingDir + "image24.bmp");
            new GaussFilter().ProcessBitmap(bmp);
            bmp.Save(WorkingDir + "output.bmp");

            FileAssert.AreEqual(WorkingDir + "output.bmp", WorkingDir + "gauss24.bmp");

            Assert.Pass();
        }

        [Test]
        public void GaussTest32()
        {
            Bitmap bmp = new Bitmap(WorkingDir + "image32.bmp");
            new GaussFilter().ProcessBitmap(bmp);
            bmp.Save(WorkingDir + "output.bmp");

            FileAssert.AreEqual(WorkingDir + "output.bmp", WorkingDir + "gauss32.bmp");

            Assert.Pass();
        }
        [Test]
        public void HugeGaussTest24()
        {
            Bitmap bmp = new Bitmap(WorkingDir + "image24.bmp");
            for(int i = 0; i < 20; i++)
                new GaussFilter().ProcessBitmap(bmp);
            bmp.Save(WorkingDir + "output.bmp");

            FileAssert.AreEqual(WorkingDir + "output.bmp", WorkingDir + "hugegauss24.bmp");

            Assert.Pass();
        }

        [Test]
        public void HugeGaussTest32()
        {
            Bitmap bmp = new Bitmap(WorkingDir + "image32.bmp");
            for (int i = 0; i < 20; i++)
                new GaussFilter().ProcessBitmap(bmp);
            bmp.Save(WorkingDir + "output.bmp");

            FileAssert.AreEqual(WorkingDir + "output.bmp", WorkingDir + "hugegauss32.bmp");

            Assert.Pass();
        }
    }
}
