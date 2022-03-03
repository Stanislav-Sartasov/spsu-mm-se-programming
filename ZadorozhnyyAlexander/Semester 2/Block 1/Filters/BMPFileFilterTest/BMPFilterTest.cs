using NUnit.Framework;
using System;
using System.IO;
using BMPFileFilter;
using System.Drawing;

namespace BMPFileFilterTest
{
    public class BMPFilterTest
    {
        private FileStream savingFile;
        private BitMapFile file;

        private Bitmap exampleOne;
        private Bitmap exampleTwo;

        private static bool CheckBitmaps(Bitmap first, Bitmap second)
        {
            bool is_good = first.Size == second.Size;
            if (!is_good)
                return false;

            for (int x = 0; x < first.Width; ++x)
            {
                for (int y = 0; y < first.Height; ++y)
                {
                    if (first.GetPixel(x, y) != second.GetPixel(x, y))
                    {
                        is_good = false;
                        break;
                    }
                }
            }
            return is_good;
        }

        [SetUp]
        public void Setup()
        {
            FileStream openingFile;
            try
            {
                openingFile = new FileStream("../../../TestImages/testing_file.bmp", FileMode.Open, FileAccess.ReadWrite);
            }
            catch (FileNotFoundException)
            {
                throw new Exception("Invalid file path specified");
            }

            try
            {
                savingFile = new FileStream("../../../TestImages/save.bmp", FileMode.Create, FileAccess.ReadWrite);
            }
            catch (Exception)
            {
                openingFile.Close();
                throw new Exception("Failed to open the output file.");
            }
            file = new(openingFile);
            openingFile.Close();
        }

        public void TestingAlgorithm(string path_1, string path_2)
        {
            exampleOne = new(path_1);
            exampleTwo = new(path_2);
            Assert.IsTrue(CheckBitmaps(exampleOne, exampleTwo));
            exampleOne.Dispose();
            exampleTwo.Dispose();
        }

        [Test]
        public void TestSobelXFilter()
        {
            Filters.ApplySobelFilter(file, "X");
            file.WriteNewFile(savingFile);
            savingFile.Close();
            TestingAlgorithm("../../../TestImages/save.bmp", "../../../TestImages/sobelx_result.bmp");
        }

        [Test]
        public void TestSobelYFilter()
        {
            Filters.ApplySobelFilter(file, "Y");
            file.WriteNewFile(savingFile);
            TestingAlgorithm("../../../TestImages/save.bmp", "../../../TestImages/sobely_result.bmp");
        }

        [Test]
        public void TestSobelBothFilter()
        {
            Filters.ApplySobelFilter(file, "Both");
            file.WriteNewFile(savingFile);
            savingFile.Close();
            TestingAlgorithm("../../../TestImages/save.bmp", "../../../TestImages/sobelboth_result.bmp");
        }

        [Test]
        public void TestGaussFilter()
        {
            Filters.ApplyGauss3x3Filter(file);
            file.WriteNewFile(savingFile);
            savingFile.Close();
            TestingAlgorithm("../../../TestImages/save.bmp", "../../../TestImages/gauss_result.bmp");
        }

        [Test]
        public void TestMedianFilter()
        {
            Filters.ApplyMiddleFilter(file);
            file.WriteNewFile(savingFile);
            savingFile.Close();
            TestingAlgorithm("../../../TestImages/save.bmp", "../../../TestImages/middle_result.bmp");
        }

        [Test]
        public void TestGreyFilter()
        {
            Filters.ApplyGreyFilter(file);
            file.WriteNewFile(savingFile);
            savingFile.Close();
            TestingAlgorithm("../../../TestImages/save.bmp", "../../../TestImages/grey_result.bmp");
        }
    }
}