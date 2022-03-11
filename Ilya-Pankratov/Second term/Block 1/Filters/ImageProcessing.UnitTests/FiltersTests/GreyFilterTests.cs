using NUnit.Framework;
using System.IO;

namespace ImageProcessing.UnitTests
{
    public class GreyFilterTests
    {
        [Test]
        public void ApplyGreyFilterFromFileTest()
        {
            // preperation 

            FileStream inputFile = new FileStream("..\\..\\..\\Resources\\OriginalCat.bmp", FileMode.Open, FileAccess.Read);
            FileStream expectedOutputFile = new FileStream("..\\..\\..\\Resources\\GreyFilterCat.bmp", FileMode.Open, FileAccess.Read);

            byte[] temp = new byte[54];
            inputFile.Read(temp, 0, 54);
            BMPFileHeader bmpInfo = new BMPFileHeader(temp);
            GreyFilter image = new GreyFilter(bmpInfo, inputFile);

            // checking

            image.ApplyGreyFilter();
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
