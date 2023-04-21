using NUnit.Framework;
using BMPFilter;
using System.IO;
using System.Drawing;
using System;

namespace BMPFilterTests
{
    public class FilterTests
    {
        private FileStream fileOut;
        private BitMapFile file;

        private Bitmap result;
        private Bitmap target;

        private static bool CompareBitmaps(Bitmap first, Bitmap second)
        {
            if (first.Size != second.Size)
            {
                return false;
            }

            for (int x = 0; x < first.Width; ++x)
            {
                for (int y = 0; y < first.Height; ++y)
                {
                    if (first.GetPixel(x, y) != second.GetPixel(x, y))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        [SetUp]
        public void Setup()
        {
            FileStream fileIn;
            try
            {
                fileIn = new FileStream("../../../images/in.bmp", FileMode.Open, FileAccess.ReadWrite);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Ошибка! Входной файл не был найден!");
            }
            try
            {
                fileOut = new FileStream("../../../images/out.bmp", FileMode.Create, FileAccess.ReadWrite);
            }
            catch (Exception)
            {
                fileIn.Close();
                throw new Exception("Ошибка! Возникла проблема с открытием/созданием выходного файла!");
            }
            file = new(fileIn);
            fileIn.Close();
        }

        [TearDown]
        public void TearDown()
        {
            result.Dispose();
            target.Dispose();
        }

        [Test]
        public void TestGaussFilter()
        {
            BMPFilter.Filters.GaussFilter.ApplyFilter(file);
            file.WriteResult(fileOut);
            fileOut.Close();
            result = new("../../../images/out.bmp");
            target = new("../../../images/gauss.bmp");
            Assert.IsTrue(CompareBitmaps(result, target));
        }

        [Test]
        public void TestMedianFilter()
        {
            BMPFilter.Filters.MedianFilter.ApplyFilter(file);
            file.WriteResult(fileOut);
            fileOut.Close();
            result = new("../../../images/out.bmp");
            target = new("../../../images/median.bmp");
            Assert.IsTrue(CompareBitmaps(result, target));
        }

        [Test]
        public void TestGrayFilter()
        {
            BMPFilter.Filters.GrayFilter.ApplyFilter(file);
            file.WriteResult(fileOut);
            fileOut.Close();
            result = new("../../../images/out.bmp");
            target = new("../../../images/gray.bmp");
            Assert.IsTrue(CompareBitmaps(result, target));
        }

        [Test]
        public void TestSobelXFilter()
        {
            BMPFilter.Filters.SobelFilter.ApplyFilter(file, BMPFilter.Filters.SobelFilter.Type.X);
            file.WriteResult(fileOut);
            fileOut.Close();
            result = new("../../../images/out.bmp");
            target = new("../../../images/sobelx.bmp");
            Assert.IsTrue(CompareBitmaps(result, target));
        }

        [Test]
        public void TestSobelYFilter()
        {
            BMPFilter.Filters.SobelFilter.ApplyFilter(file, BMPFilter.Filters.SobelFilter.Type.Y);
            file.WriteResult(fileOut);
            fileOut.Close();
            result = new("../../../images/out.bmp");
            target = new("../../../images/sobely.bmp");
            Assert.IsTrue(CompareBitmaps(result, target));
        }
    }
}