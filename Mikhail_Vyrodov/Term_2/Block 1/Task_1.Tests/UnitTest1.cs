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
    public class Tests
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
        public void SobelxTest()
        {
            FilterTest("SobelX");
        }

        [Test]
        public void SobelyTest()
        {
            FilterTest("SobelY");
        }

        [Test]
        public void GcTest()
        {
            FilterTest("GrayScale");
        }

        public void FilterTest(string filter)
        {
            string autumnImg;
            string normalPath = TestHelper.GetTestsPath();
            if (filter == "Median")
            {
                autumnImg = normalPath + @"\data\autumn_median.bmp";
            }
            else if (filter == "GrayScale")
            {
                autumnImg = normalPath + @"\data\autumn_gc.bmp";
            }
            else if (filter == "SobelX")
            {
                autumnImg = normalPath + @"\data\autumn_sobelx.bmp";
            }
            else if (filter == "SobelY")
            {
                autumnImg = normalPath + @"\data\autumn_sobely.bmp";
            }
            else
                autumnImg = normalPath + @"\data\autumn_gauss.bmp";
            string fileName = normalPath + @"\data\tiger.bmp";
            string newName = normalPath + @"\data\test.bmp";
            Image test_img = new Image();
            Image autumn_img = new Image();
            uint height = 0, width = 0;
            test_img.ReadImage(fileName);
            test_img.ApplyFilters(newName, filter);
            autumn_img.ReadImage(autumnImg);
            test_img.GetAtrs(ref height, ref width);
            Argb[][] pixArr = new Argb[height][];
            for (int i = 0; i < height; i++)
            {
                pixArr[i] = new Argb[width];
            }
            test_img.GetArr(pixArr);
            Argb[][] autumnArr = new Argb[height][];
            for (int i = 0; i < height; i++)
            {
                autumnArr[i] = new Argb[width];
            }
            autumn_img.GetArr(autumnArr);
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