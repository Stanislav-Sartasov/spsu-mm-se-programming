using NUnit.Framework;
using BitMapTask;

namespace BitMapTaskTests
{
    public class BitMapImageTests
    {
        private string path = @"../../../../BitMapTaskTests/testFiles/input.bmp";

        [Test]
        public void BitMapImageTest()  // Reading test
        {
            uint width = 1280;
            uint height = 1280;

            BitMapImage image = new BitMapImage(path);

            Assert.AreEqual(width, image.Width);
            Assert.AreEqual(height, image.Height);
        }

        [Test]
        public void WriteBitMapTest()
        {
            BitMapImage image = new BitMapImage(path);
            image.WriteBitMap(@"../../../testFiles/WriteBitMapTest.bmp");

            FileAssert.AreEqual(@"../../../testFiles/input.bmp", @"../../../testFiles/WriteBitMapTest.bmp");
        }

        [Test]
        public void GetPixelTest()
        {
            BitMapImage image = new BitMapImage(path);
            Pixel expected = new Pixel(255, 255, 255);

            Pixel actual = image.GetPixel(0, 0);

            Assert.AreEqual(expected.Red, actual.Red);
            Assert.AreEqual(expected.Green, actual.Green);
            Assert.AreEqual(expected.Blue, actual.Blue);
        }

        [Test]
        public void SetPixelTest()
        {
            BitMapImage image = new BitMapImage(path);
            Pixel expected = new Pixel(0, 0, 0);

            image.SetPixel(0, 0, expected);
            Pixel actual = image.GetPixel(0, 0);

            Assert.AreEqual(expected.Red, actual.Red);
            Assert.AreEqual(expected.Green, actual.Green);
            Assert.AreEqual(expected.Blue, actual.Blue);
        }
    }
}