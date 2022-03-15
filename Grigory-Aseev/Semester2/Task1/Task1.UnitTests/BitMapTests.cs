using NUnit.Framework;
using System;

namespace Task1.UnitTests
{
    public class BitMapTests
    {
        private string pathToFileInImage24 = "../../../TestFiles/InImage24.bmp";
        private string pathToFileInImage32 = "../../../TestFiles/InImage32.bmp";
        private string pathToFileOutImage24 = "../../../TestFiles/OutImage24.bmp";
        private string pathToFileOutImage32 = "../../../TestFiles/OutImage32.bmp";
        private string pathToFileBrokenImage = "../../../TestFiles/BrokenEmptyImage.bmp";
        private string pathToFileInImageEmpty = "../../../TestFiles/InImageEmpty.bmp";
        private string pathToFileOutImageEmpty = "../../../TestFiles/OutImageEmpty.bmp";

        [Test]
        public void ReadingCorrectlyImage24()
        {
            try
            {
                Image image = Image.ReadBmp(pathToFileInImage24);
                Assert.IsNotNull(image);
                if (!image.GeneralSuccess)
                {
                    Assert.Fail("Not a success");
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            Assert.Pass();
        }

        [Test]
        public void ReadingCorrectlyImage32()
        {
            try
            {
                Image image = Image.ReadBmp(pathToFileInImage32);
                Assert.IsNotNull(image);
                if (!image.GeneralSuccess)
                {
                    Assert.Fail("Not a success");
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            Assert.Pass();
        }

        [Test]
        public void ReadingEmptyImage()
        {
            try
            {
                Image image = Image.ReadBmp(pathToFileBrokenImage);
                Assert.IsNotNull(image);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            Assert.Pass();
        }

        [Test]
        public void WritingCorrectlyImage24()
        {
            try
            {
                Image image = Image.ReadBmp(pathToFileInImage24);
                image.WriteBmp(pathToFileOutImage24);
                FileAssert.AreEqual(pathToFileInImage24, pathToFileOutImage24);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            Assert.Pass();
        }

        [Test]
        public void WritingCorrectlyImage32()
        {
            try
            {
                Image image = Image.ReadBmp(pathToFileInImage32);
                image.WriteBmp(pathToFileOutImage32);
                FileAssert.AreEqual(pathToFileInImage32, pathToFileOutImage32);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            Assert.Pass();
        }

        [Test]
        public void WritingCorrectlyEmptyImage()
        {
            try
            {
                Image image = Image.ReadBmp(pathToFileBrokenImage);
                image.WriteBmp(pathToFileOutImageEmpty);
                FileAssert.AreEqual(pathToFileOutImageEmpty, pathToFileInImageEmpty);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            Assert.Pass();
        }

        [Test]
        public void ReadingNullFile()
        {
            Image image = Image.ReadBmp(null);
            Assert.IsInstanceOf<EmptyImage>(image);
        }

        [Test]
        public void ReadingNotAFile()
        {
            Image image = Image.ReadBmp("zxcghoul");
            Assert.IsInstanceOf<EmptyImage>(image);
        }

        [Test]
        public void WritingNullFile()
        {
            Image image = Image.ReadBmp(pathToFileInImage24);
            Assert.IsNull(image.WriteBmp(null));
        }

        [Test]
        public void WritingNotAFile()
        {
            string pattern = @"([a-zA-Z]://)((/w+//)+|(/w+./w+))";
            Image image = Image.ReadBmp(pathToFileInImage24);
            Assert.IsNull(image.WriteBmp("zxcghoul"));
            Assert.IsNull(image.WriteBmp(pattern));
        }
    }
}