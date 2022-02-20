using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Task1.UnitTests
{
    public class FilterTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ApplyFilterTest()
        {
            Image image = new Image("test.bmp");

            Filter filterPack = new Filter();

            string[] filtersForTest = new string[] { "sobelX", "gray",
            "sobelY","sobelXY", "scharrX", "scharrY","scharrXY",
                "gauss3", "gauss5","gauss7","median"};

            foreach (string filter in filtersForTest)
            {
                image.ApplyFilter(filterPack[filter], filter + ".bmp");

                if (new Image(filter+".test.bmp") != new Image(filter + ".bmp"))
                    Assert.Fail();
                File.Delete(filter + ".bmp");
            }

            Assert.Pass();

            
        }
    }
}