using NUnit.Framework;
using System;
using System.IO;

namespace ImageProcessing.UnitTests
{
    public class BMPFileTests
    {
        [Test]
        public void AssignmentTest()
        {
            // preparation 

            FileStream inputFile = new FileStream("..\\..\\..\\Resources\\OriginalCat.bmp", FileMode.Open, FileAccess.Read);
            byte[] temp = new byte[54];
            inputFile.Read(temp, 0, 54);
            BMPFileHeader file = new BMPFileHeader(temp);

            string expectedFileType = "BM";
            uint expectedFileSize = 4500054;
            ushort expectedReserved1 = 0;
            ushort expectedReserved2 = 0;
            uint expectedImageOffset = 54;
            uint expectedHeaderSize = 40;
            uint expectedWidth = 1000;
            uint expectedHeight = 1500;
            ushort expectedPlanes = 1;
            ushort expectedBitPerPixel = 24;
            uint expectedCompression = 0;
            uint expectedSizeImage = 4500000;
            uint expectedXPelsPerMeter = 2835;
            uint expectedYPelsPerMeter = 2835;
            uint expectedColorsUsed = 0;
            uint expectedColorsImportant = 0;
            byte[] expectedByteReprezentation = new byte[expectedImageOffset];
            Array.Copy(temp, expectedByteReprezentation, expectedImageOffset);

            // checking for properties' value

            Assert.AreEqual(file.FileType, expectedFileType);
            Assert.AreEqual(file.FileSize, expectedFileSize);
            Assert.AreEqual(file.Reserved1, expectedReserved1);
            Assert.AreEqual(file.Reserved2, expectedReserved2); 
            Assert.AreEqual(file.ImageOffset, expectedImageOffset);
            Assert.AreEqual(file.HeaderSize, expectedHeaderSize);
            Assert.AreEqual(file.Width, expectedWidth);
            Assert.AreEqual(file.Height, expectedHeight);
            Assert.AreEqual(file.Planes, expectedPlanes);
            Assert.AreEqual(file.BitPerPixel, expectedBitPerPixel);
            Assert.AreEqual(file.Compression, expectedCompression);
            Assert.AreEqual(file.SizeImage, expectedSizeImage);
            Assert.AreEqual(file.XPelsPerMeter, expectedXPelsPerMeter);
            Assert.AreEqual(file.YPelsPerMeter, expectedYPelsPerMeter); ;
            Assert.AreEqual(file.ColorsUsed, expectedColorsUsed);
            Assert.AreEqual(file.ColorsImportant, expectedColorsImportant);

            for (int i = 0; i < expectedImageOffset; i++)
                Assert.AreEqual(expectedByteReprezentation[i], file.ByteRepresentation[i]);
        }
    } 
}
