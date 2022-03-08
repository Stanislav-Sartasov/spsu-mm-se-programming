using NUnit.Framework;
using System.IO;

namespace ImageProcessing.UnitTests
{
    public class SobelFilterTests
    {
        [Test]
        public void ApplySobelFilterFromFileTest()
        {
            // preperation 

            FileStream inputFile = new FileStream("..\\..\\..\\Resources\\OriginalCat.bmp", FileMode.Open, FileAccess.Read);
            FileStream expectedOutputFile = new FileStream("..\\..\\..\\Resources\\SobelFilterCat.bmp", FileMode.Open, FileAccess.Read);

            byte[] temp = new byte[54];
            inputFile.Read(temp, 0, 54);
            BMPFile bmpInfo = new BMPFile(temp);
            SobelFilter image = new SobelFilter(bmpInfo, inputFile);

            // checking SobelFilter

            image.ApplySobelFilter(SobelFilter.FilterType.SobelFilter);
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
        public void ApplySobelXFilterFromFileTest()
        {
            // preperation 

            FileStream inputFile = new FileStream("..\\..\\..\\Resources\\OriginalCat.bmp", FileMode.Open, FileAccess.Read);
            FileStream expectedOutputFile = new FileStream("..\\..\\..\\Resources\\SobelXFilterCat.bmp", FileMode.Open, FileAccess.Read);

            byte[] temp = new byte[54];
            inputFile.Read(temp, 0, 54);
            BMPFile bmpInfo = new BMPFile(temp);
            SobelFilter image = new SobelFilter(bmpInfo, inputFile);

            // checking SobelFilterX

            image.ApplySobelFilter(SobelFilter.FilterType.SobelFilterX);
            byte[] outputFile = new byte[bmpInfo.FileSize];
            image.WriteImage(outputFile);

            for (uint i = 0; i < bmpInfo.FileSize; i++)
            {
                Assert.IsTrue(outputFile[i] == (byte)expectedOutputFile.ReadByte());
            }
        }

        [Test]
        public void ApplySobelYFilterFromFileTest()
        {
            // preperation 

            FileStream inputFile = new FileStream("..\\..\\..\\Resources\\OriginalCat.bmp", FileMode.Open, FileAccess.Read);
            FileStream expectedOutputFile = new FileStream("..\\..\\..\\Resources\\SobelYFilterCat.bmp", FileMode.Open, FileAccess.Read);

            byte[] temp = new byte[54];
            inputFile.Read(temp, 0, 54);
            BMPFile bmpInfo = new BMPFile(temp);
            SobelFilter image = new SobelFilter(bmpInfo, inputFile);

            // checking SobelFilterY

            image.ApplySobelFilter(SobelFilter.FilterType.SobelFilterY);
            byte[] outputFile = new byte[bmpInfo.FileSize];
            image.WriteImage(outputFile);

            for (uint i = 0; i < bmpInfo.FileSize; i++)
            {
                Assert.IsTrue(outputFile[i] == (byte)expectedOutputFile.ReadByte());
            }
        }

        [Test]
        public void ApplySobelFilterFromByteArrayTest()
        {
            // preperation 
            byte[] expectedImage = Resource.SobelFilterCat;
            BMPFile header = new BMPFile(Resource.OriginalCat);
            SobelFilter image = new SobelFilter(header, Resource.OriginalCat);

            // checking SobelFilter

            image.ApplySobelFilter(SobelFilter.FilterType.SobelFilter);
            byte[] outputFile = new byte[header.FileSize];
            image.WriteImage(outputFile);

            for (uint i = 0; i < header.FileSize; i++)
            {
                Assert.IsTrue(outputFile[i] == expectedImage[i]);
            }
        }
    }
}