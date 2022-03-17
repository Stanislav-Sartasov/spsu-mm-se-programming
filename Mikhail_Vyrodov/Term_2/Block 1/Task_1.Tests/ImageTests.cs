using NUnit.Framework;
using System.IO;
using System.Reflection;
namespace Task_1.Tests
{
    public static class TestHelper
    {
        public static string GetTestsPath()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            return new System.Uri(path).LocalPath;
        }
    }
    public class ImageTests
    {
        [Test]
        public void MedianTest()
        {
            FilterTest("Median");
        }

        [Test]
        public void GaussTest()
        {
            FilterTest("Gauss");
        }

        [Test]
        public void SobelXTest()
        {
            FilterTest("SobelX");
        }

        [Test]
        public void SobelYTest()
        {
            FilterTest("SobelY");
        }

        [Test]
        public void GsTest()
        {
            FilterTest("GrayScale");
        }

        public void FilterTest(string filter)
        {
            string autumnImg;
            string normalPath = TestHelper.GetTestsPath();
            if (filter == "Median")
            {
                autumnImg = normalPath + @"\data\autumnMedian.bmp";
            }
            else if (filter == "GrayScale")
            {
                autumnImg = normalPath + @"\data\autumnGS.bmp";
            }
            else if (filter == "SobelX")
            {
                autumnImg = normalPath + @"\data\autumnSobelX.bmp";
            }
            else if (filter == "SobelY")
            {
                autumnImg = normalPath + @"\data\autumnSobelY.bmp";
            }
            else
                autumnImg = normalPath + @"\data\autumnGauss.bmp";
            string fileName = normalPath + @"\data\tiger.bmp";
            System.Console.WriteLine(fileName);
            string newName = normalPath + @"\data\test.bmp";
            Image testImage = new Image();
            Image autumnImage = new Image();
            uint height = 0, width = 0;
            testImage.ReadImage(fileName);
            testImage.ApplyFilters(newName, filter);
            autumnImage.ReadImage(autumnImg);
            testImage.GetAtrs(ref height, ref width);
            Pixel[][] pixArr = new Pixel[height][];
            for (int i = 0; i < height; i++)
            {
                pixArr[i] = new Pixel[width];
            }
            testImage.GetArr(pixArr);
            Pixel[][] autumnArr = new Pixel[height][];
            for (int i = 0; i < height; i++)
            {
                autumnArr[i] = new Pixel[width];
            }
            autumnImage.GetArr(autumnArr);
            for (uint i = 0; i < height; i++)
            {
                for (uint k = 0; k < width; k++)
                {
                    Assert.AreEqual(autumnArr[i][k].Red, pixArr[i][k].Red);
                    Assert.AreEqual(autumnArr[i][k].Blue, pixArr[i][k].Blue);
                    Assert.AreEqual(autumnArr[i][k].Green, pixArr[i][k].Green);
                    Assert.AreEqual(autumnArr[i][k].Alpha, pixArr[i][k].Alpha);
                }
            }
            Assert.Pass();
        }
    }
}