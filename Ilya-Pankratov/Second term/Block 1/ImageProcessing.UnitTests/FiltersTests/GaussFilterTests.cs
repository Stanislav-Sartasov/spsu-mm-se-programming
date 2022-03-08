using NUnit.Framework;
using System.IO;

namespace ImageProcessing.UnitTests
{
    public class GaussFilterTests
    {
        [Test]
        public void ApplyGaussFilterFromFileTest()
        {
            // preperation 

            FileStream inputFile = new FileStream("..\\..\\..\\Resources\\OriginalCat.bmp", FileMode.Open, FileAccess.Read);
            FileStream expectedOutputFile = new FileStream("..\\..\\..\\Resources\\GaussFilterCat.bmp", FileMode.Open, FileAccess.Read);

            byte[] temp = new byte[54];
            inputFile.Read(temp, 0, 54);
            BMPFile bmpInfo = new BMPFile(temp);
            GaussFilter image = new GaussFilter(bmpInfo, inputFile);

            // checking

            image.ApplyGaussFilter();
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
        public void ApplyGaussFilterFromByteArrayTest()
        {
            // preperation 

            byte[] expectedFile = Resource.GaussFilterCat;
            BMPFile header = new BMPFile(Resource.OriginalCat);
            GaussFilter image = new GaussFilter(header, Resource.OriginalCat);

            // checking 

            image.ApplyGaussFilter();
            byte[] outputFile = new byte[header.FileSize];
            image.WriteImage(outputFile);

            for (uint i = 0; i < header.FileSize; i++)
            {
                Assert.IsTrue(outputFile[i] == expectedFile[i]);
            }
        }
    }
}
