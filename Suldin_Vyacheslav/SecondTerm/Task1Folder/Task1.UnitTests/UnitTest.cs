using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Task1.UnitTests
{
    public class UnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]

        public void ImageOperatorTest()
        {
            string folder = "../../../TestFiles/";
            
            Image image = new Image(folder + "test.bmp");
            Image image1 = new Image(folder + "Yestest.bmp");
            Image image0 = new Image(folder + "Nottest.bmp");

            if (image==image1 && image!=image0)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void ApplyFilterTest()
        {
            string folder = "../../../TestFiles/";

            Image image = new Image(folder + "test.bmp");

            Filter filterPack = new Filter();

            string[] filtersForTest = new string[] {"median" , "gray","sobelX",
            "sobelY","sobelXY", "scharrX", "scharrY", "scharrXY",
                "gauss3", "gauss5","gauss7"};

            foreach (string filter in filtersForTest)
            {
                image.ApplyFilter(filterPack[filter], filter + ".bmp");

                if (new Image(folder + filter + ".test.bmp") != new Image(filter + ".bmp"))
                    Assert.Fail(filter);
                File.Delete(filter + ".bmp");
            }

            Assert.Pass();


        }
    }
}