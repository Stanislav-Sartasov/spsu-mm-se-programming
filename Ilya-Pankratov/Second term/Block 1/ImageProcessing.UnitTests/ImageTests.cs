using NUnit.Framework;
using System.IO;

namespace ImageProcessing.UnitTests
{
    public class ImageTests
    {
        [Test]
        public void ReadAndWriteImageFromFileTest()
        {
            // preperation 

            FileStream inputAndOutFile = new FileStream("..\\..\\..\\Resources\\OriginalCat.bmp", FileMode.Open, FileAccess.Read);

            // checking that Read and Write methods work correctly

            byte[] temp = new byte[54];
            inputAndOutFile.Read(temp, 0, 54);

            BMPFile fileInfo = new BMPFile(temp);

            Image image = new Image(fileInfo, inputAndOutFile);
            byte[] data = new byte[fileInfo.FileSize];

            image.WriteImage(data);
            inputAndOutFile.Seek(0, SeekOrigin.Begin);

            for (int i = 0; i < fileInfo.FileSize; i++)
            {
                Assert.IsTrue(data[i] == inputAndOutFile.ReadByte());
            }

            inputAndOutFile.Close();
        }

        [Test]
        public void ReadAndWriteImageFromByteArrayTest()
        {
            // preperation 

            byte[] expectedFile = Resource.OriginalCat;

            // checking that Read and Write methods work correctly

            BMPFile fileInfo = new BMPFile(expectedFile);
            Image image = new Image(fileInfo, expectedFile);
            byte[] data = new byte[fileInfo.FileSize];

            image.WriteImage(data);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] == expectedFile[i]);
            }
        }  
    }
}
