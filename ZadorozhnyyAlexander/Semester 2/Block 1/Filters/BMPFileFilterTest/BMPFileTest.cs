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
            FileStream OpenigFile;
            bool IsFoundException = false;
            try
            {
                OpenigFile = new FileStream("../../../TestImages/error.bmp", FileMode.Open, FileAccess.ReadWrite);
            }
            catch
            {
                throw new Exception("Invalid file path specified");
            }

            try
            {
                BitMapFile file = new BitMapFile(OpenigFile);
            }
            catch (Exception exception)
            {
                IsFoundException = exception.Message.Contains("Unsupported bitness of file.");
            }
            Assert.IsTrue(IsFoundException);
        }
    }
}