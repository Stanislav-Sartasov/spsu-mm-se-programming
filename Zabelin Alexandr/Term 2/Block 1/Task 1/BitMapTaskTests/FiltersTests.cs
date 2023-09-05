using NUnit.Framework;
using BitMapTask;

namespace BitMapTaskTests
{
    class FiltersTests
    {
        private string path = @"../../../testFiles/input.bmp";

        [Test]
        public void MedianTest()
        {
            BitMapImage image = new BitMapImage(path);

            Filters.Median(image);
            image.WriteBitMap(@"../../../testFiles/MedianTest.bmp");

            FileAssert.AreEqual(@"../../../testFiles/MedianTrue.bmp", @"../../../testFiles/MedianTest.bmp");
        }

        [Test]
        public void GaussTest()
        {
            BitMapImage image = new BitMapImage(path);

            Filters.Gauss(image);
            image.WriteBitMap(@"../../../testFiles/GaussTest.bmp");

            FileAssert.AreEqual(@"../../../testFiles/GaussTrue.bmp", @"../../../testFiles/GaussTest.bmp");
        }

        [Test]
        public void SobelXTest()
        {
            BitMapImage image = new BitMapImage(path);

            Filters.SobelX(image);
            image.WriteBitMap(@"../../../testFiles/SobelXTest.bmp");

            FileAssert.AreEqual(@"../../../testFiles/SobelXTrue.bmp", @"../../../testFiles/SobelXTest.bmp");
        }

        [Test]
        public void SobelYTest()
        {
            BitMapImage image = new BitMapImage(path);

            Filters.SobelY(image);
            image.WriteBitMap(@"../../../testFiles/SobelYTest.bmp");

            FileAssert.AreEqual(@"../../../testFiles/SobelYTrue.bmp", @"../../../testFiles/SobelYTest.bmp");
        }

        [Test]
        public void GrayTest()
        {
            BitMapImage image = new BitMapImage(path);

            Filters.Gray(image);
            image.WriteBitMap(@"../../../testFiles/GrayTest.bmp");

            FileAssert.AreEqual(@"../../../testFiles/GrayTrue.bmp", @"../../../testFiles/GrayTest.bmp");
        }
    }
}
