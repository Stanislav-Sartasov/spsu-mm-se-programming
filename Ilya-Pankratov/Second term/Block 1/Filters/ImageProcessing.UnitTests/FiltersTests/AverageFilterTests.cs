using NUnit.Framework;
using System;
using System.IO;

namespace ImageProcessing.UnitTests
{
    public class AverageFilterTests
    {
        [Test]
        public void CreateAverageFilterFromFile()
        {
            // preperation 

            FileStream inputFile = new FileStream("..\\..\\..\\Resources\\OriginalCat.bmp", FileMode.Open, FileAccess.Read);
            FileStream expectedOutputFile = new FileStream("..\\..\\..\\Resources\\AverageFilterCat.bmp", FileMode.Open, FileAccess.Read);

            byte[] temp = new byte[54];
            inputFile.Read(temp, 0, 54);
            BMPFileHeader bmpInfo = new BMPFileHeader(temp);
            AverageFilter image = new AverageFilter(bmpInfo, inputFile);

            // checking

            image.ApplyAverageFilter();
            byte[] outputFile = new byte[bmpInfo.FileSize];
            image.WriteImage(outputFile);

            for (uint i = 0; i < bmpInfo.FileSize; i++)
            {
                Assert.IsTrue(outputFile[i] == (byte)expectedOutputFile.ReadByte());
            }

            inputFile.Close();
            expectedOutputFile.Close();
        }
    }
}
