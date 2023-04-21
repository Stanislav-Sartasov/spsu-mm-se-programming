using NUnit.Framework;
using System.IO;

namespace Task_1.UnitTests
{
    public class ImageTests
    {
        [Test]
        public void MakeNewFileTest()
        {
            Image testImage = new Image("../../../normalImage.bmp");
            testImage.MakeNewFile("../../../testImage.bmp");
            Image normalImage = new Image("../../../testImage.bmp");
            Assert.AreEqual(testImage.Pixels, normalImage.Pixels);
            File.Delete("../../../testImage.bmp");
            Assert.Pass();
        }

        [Test]
        public void GreyFilterTest()
        {
            Image testImage = new Image("../../../normalImage.bmp");
            Image greyImage = new Image("../../../greyImage.bmp");
            testImage.GreyFilter();
            Assert.AreEqual(testImage.Pixels, greyImage.Pixels);
            Assert.Pass();
        }

        [Test]
        public void MiddleFilterTest()
        {
            Image testImage = new Image("../../../normalImage.bmp");
            Image middleImage = new Image("../../../middleImage.bmp");
            testImage.MiddleFilter();
            Assert.AreEqual(testImage.Pixels, middleImage.Pixels);
            Assert.Pass();
        }

        [Test]
        public void GaussFilterTest()
        {
            Image testImage = new Image("../../../normalImage.bmp");
            Image gaussImage = new Image("../../../gaussImage.bmp");
            testImage.GaussFilter();
            Assert.AreEqual(testImage.Pixels, gaussImage.Pixels);
            Assert.Pass();
        }

        [Test]
        public void SobelXAxisTest()
        {
            Image testImage = new Image("../../../normalImage.bmp");
            Image sobelXAxisImage = new Image("../../../sobelXImage.bmp");
            testImage.SobelAxisFilter(1);
            Assert.AreEqual(testImage.Pixels, sobelXAxisImage.Pixels);
            Assert.Pass();
        }

        [Test]
        public void SobelYAxisTest()
        {
            Image testImage = new Image("../../../normalImage.bmp");
            Image sobelYAxisImage = new Image("../../../sobelYImage.bmp");
            testImage.SobelAxisFilter(0);
            Assert.AreEqual(testImage.Pixels, sobelYAxisImage.Pixels);
            Assert.Pass();
        }
    }
}