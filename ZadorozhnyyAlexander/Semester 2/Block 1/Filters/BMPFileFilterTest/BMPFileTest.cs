using NUnit.Framework;
using System;
using System.IO;
using BMPFileFilter;

namespace BMPFileFilterTest
{
    public class BitMapTest
    {
        [Test]
        public void TestBrokenBitMapFile()
        {
            FileStream openingFile;
            bool isFoundException = false;
            try
            {
                openingFile = new FileStream("../../../TestImages/error.bmp", FileMode.Open, FileAccess.ReadWrite);
            }
            catch
            {
                throw new Exception("Invalid file path specified");
            }

            try
            {
                BitMapFile file = new BitMapFile(openingFile);
            }
            catch (Exception exception)
            {
                isFoundException = exception.Message.Contains("Unsupported bitness of file.");
            }
            Assert.IsTrue(isFoundException);
        }
    }
}