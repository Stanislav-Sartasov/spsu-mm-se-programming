using NUnit.Framework;
using System.IO;

namespace ImageProcessing.UnitTests
{
    public class MedianFilterTests
    {
        [Test]
        public void ApplyMedianFilterFromFileTest()
        {
            // preperation 

            FileStream inputFile = new FileStream("..\\..\\..\\Resources\\OriginalCat.bmp", FileMode.Open, FileAccess.Read);
            FileStream expectedOutputFile = new FileStream("..\\..\\..\\Resources\\MedianFilterCat.bmp", FileMode.Open, FileAccess.Read);

            byte[] temp = new byte[54];
            inputFile.Read(temp, 0, 54);
            BMPFile bmpInfo = new BMPFile(temp);
            MedianFilter image = new MedianFilter(bmpInfo, inputFile);

            // checking

            image.ApplyMedianFilter();
            byte[] outputFile = new byte[bmpInfo.FileSize];
            image.WriteImage(outputFile);

            for (uint i = 0; i < bmpInfo.FileSize; i++)
            {
                Assert.IsTrue(outputFile[i] == (byte)expectedOutputFile.ReadByte());
            }

            inputFile.Close();
            expectedOutputFile.Close();
        }

        [Test]
        public void ApplyMedianFilterFromByteArrayTest()
        {
            // preperation 

            byte[] expectedFile = Resource.MedianFilterCat;
            BMPFile header = new BMPFile(Resource.OriginalCat);
            MedianFilter image = new MedianFilter(header, Resource.OriginalCat);

            // checking MedianFilter

            image.ApplyMedianFilter();
            byte[] outputFile = new byte[header.FileSize];
            image.WriteImage(outputFile);

            for (uint i = 0; i < header.FileSize; i++)
            {
                Assert.IsTrue(outputFile[i] == expectedFile[i]);
            }
        }
    }
}
