using NUnit.Framework;
using System;
using System.IO;
using BMPFileFilter;
using System.Drawing;

namespace BMPFileFilterTest
{
    public class BMPFilterTest
    {
        private FileStream SavingFile;
        private BitMapFile File;

        private Bitmap ExampleOne;
        private Bitmap ExampleTwo;

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
            FileStream OpenigFile;
            try
            {
                OpenigFile = new FileStream("../../../TestImages/testing_file.bmp", FileMode.Open, FileAccess.ReadWrite);
            }
            catch (FileNotFoundException)
            {
                throw new Exception("Invalid file path specified");
            }

            try
            {
                SavingFile = new FileStream("../../../TestImages/save.bmp", FileMode.Create, FileAccess.ReadWrite);
            }
            catch (Exception)
            {
                OpenigFile.Close();
                throw new Exception("Failed to open the output file.");
            }
            File = new(OpenigFile);
            OpenigFile.Close();
        }

        public void TestingAlgorithm(string path_1, string path_2)
        {
            ExampleOne = new(path_1);
            ExampleTwo = new(path_2);
            Assert.IsTrue(CheckBitmaps(ExampleOne, ExampleTwo));
            ExampleOne.Dispose();
            ExampleTwo.Dispose();
        }

        [Test]
        public void TestSobelXFilter()
        {
            Filters.ApplySobelFilter(File, "X");
            File.WriteNewFile(SavingFile);
            SavingFile.Close();
            TestingAlgorithm("../../../TestImages/save.bmp", "../../../TestImages/sobelx_result.bmp");
        }

        [Test]
        public void TestSobelYFilter()
        {
            Filters.ApplySobelFilter(File, "Y");
            File.WriteNewFile(SavingFile);
            TestingAlgorithm("../../../TestImages/save.bmp", "../../../TestImages/sobely_result.bmp");
        }

        [Test]
        public void TestSobelBothFilter()
        {
            Filters.ApplySobelFilter(File, "Both");
            File.WriteNewFile(SavingFile);
            SavingFile.Close();
            TestingAlgorithm("../../../TestImages/save.bmp", "../../../TestImages/sobelboth_result.bmp");
        }

        [Test]
        public void TestGaussFilter()
        {
            Filters.ApplyGauss3x3Filter(File);
            File.WriteNewFile(SavingFile);
            SavingFile.Close();
            TestingAlgorithm("../../../TestImages/save.bmp", "../../../TestImages/gauss_result.bmp");
        }

        [Test]
        public void TestMedianFilter()
        {
            Filters.ApplyMiddleFilter(File);
            File.WriteNewFile(SavingFile);
            SavingFile.Close();
            TestingAlgorithm("../../../TestImages/save.bmp", "../../../TestImages/middle_result.bmp");
        }

        [Test]
        public void TestGreyFilter()
        {
            Filters.ApplyGreyFilter(File);
            File.WriteNewFile(SavingFile);
            SavingFile.Close();
            TestingAlgorithm("../../../TestImages/save.bmp", "../../../TestImages/grey_result.bmp");
        }
    }
}