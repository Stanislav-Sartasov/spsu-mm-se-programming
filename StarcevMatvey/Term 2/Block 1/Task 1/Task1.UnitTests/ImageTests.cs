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
            Assert.AreEqual(testImage.image, normalImage.image);
            File.Delete("../../../testImage.bmp");
            Assert.Pass();
        }

        [Test]
        public void GreyFilterTest()
        {
            Image testImage = new Image("../../../normalImage.bmp");
            Image greyImage = new Image("../../../greyImage.bmp");
            testImage.GreyFilter();
            Assert.AreEqual(testImage.image, greyImage.image);
            Assert.Pass();
        }

        [Test]
        public void MiddleFilterTest()
        {
            Image testImage = new Image("../../../normalImage.bmp");
            Image middleImage = new Image("../../../middleImage.bmp");
            testImage.MiddleFilter();
            Assert.AreEqual(testImage.image, middleImage.image);
            Assert.Pass();
        }

        [Test]
        public void GaussFilterTest()
        {
            Image testImage = new Image("../../../normalImage.bmp");
            Image gaussImage = new Image("../../../gaussImage.bmp");
            testImage.GaussFilter();
            Assert.AreEqual(testImage.image, gaussImage.image);
            Assert.Pass();
        }

        [Test]
        public void SobelXAxisTest()
        {
            Image testImage = new Image("../../../normalImage.bmp");
            Image sobelXAxisImage = new Image("../../../sobelXImage.bmp");
            testImage.SobelAxisFilter(1);
            Assert.AreEqual(testImage.image, sobelXAxisImage.image);
            Assert.Pass();
        }

        [Test]
        public void SobelYAxisTest()
        {
            Image testImage = new Image("../../../normalImage.bmp");
            Image sobelYAxisImage = new Image("../../../sobelYImage.bmp");
            testImage.SobelAxisFilter(0);
            Assert.AreEqual(testImage.image, sobelYAxisImage.image);
            Assert.Pass();
        }
    }
}