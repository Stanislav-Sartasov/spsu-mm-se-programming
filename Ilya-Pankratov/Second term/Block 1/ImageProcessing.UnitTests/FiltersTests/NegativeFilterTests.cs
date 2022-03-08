using NUnit.Framework;
using System.IO;

namespace ImageProcessing.UnitTests
{
    public class NegativeFilterTests
    {
        [Test]
        public void ApplyNegativeFilterFromFileTest()
        {
            // preperation 

            FileStream inputFile = new FileStream("..\\..\\..\\Resources\\OriginalCat.bmp", FileMode.Open, FileAccess.Read);
            FileStream expectedOutputFile = new FileStream("..\\..\\..\\Resources\\NegativeFilterCat.bmp", FileMode.Open, FileAccess.Read);

            byte[] temp = new byte[54];
            inputFile.Read(temp, 0, 54);
            BMPFile bmpInfo = new BMPFile(temp);
            NegativeFilter image = new NegativeFilter(bmpInfo, inputFile);

            // checking

            image.ApplyNegativeFilter();
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
        public void ApplyNegativeFilterFromByteArrayTest()
        {
            // preperation 

            byte[] expectedFile = Resource.NegativeFilterCat;
            BMPFile header = new BMPFile(Resource.OriginalCat);
            NegativeFilter image = new NegativeFilter(header, Resource.OriginalCat);
            
            // checking 

            image.ApplyNegativeFilter();
            byte[] outputFile = new byte[header.FileSize];
            image.WriteImage(outputFile);

            for (uint i = 0; i < header.FileSize; i ++)
            { 
                Assert.IsTrue(outputFile[i] == expectedFile[i]);
            }
        }
    }
}
