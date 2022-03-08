using NUnit.Framework;
using BmpFilters;

namespace BmpFiltersTest
{
    public class FiltersTest
    {
        [Test]
        public void GrayFilterTest()
        {
            BitMapReading input = new BitMapReading("../../../images/input.bmp");
            ChooseFilter.ApplyFilter(input, "GRAY", "../../../images/testgray.bmp");
            var file1 = new BitMapReading("../../../images/truegray.bmp");
            var file2 = new BitMapReading("../../../images/testgray.bmp");
            Assert.IsTrue(file1 == file2);
        }

        [Test]
        public void MedianFilterTest()
        {
            BitMapReading input = new BitMapReading("../../../images/input.bmp");
            ChooseFilter.ApplyFilter(input, "MEDIAN", "../../../images/testmedian.bmp");
            var file1 = new BitMapReading("../../../images/truemedian.bmp");
            var file2 = new BitMapReading("../../../images/testmedian.bmp");
            Assert.IsTrue(file1 == file2);
        }

        [Test]
        public void GaussFilterTest()
        {
            BitMapReading input = new BitMapReading("../../../images/input.bmp");
            ChooseFilter.ApplyFilter(input, "GAUSS", "../../../images/testgauss.bmp");
            var file1 = new BitMapReading("../../../images/truegauss.bmp");
            var file2 = new BitMapReading("../../../images/testgauss.bmp");
            Assert.IsTrue(file1 == file2);
        }

        [Test]
        public void SobelFilterTest()
        {
            BitMapReading input = new BitMapReading("../../../images/input.bmp");
            ChooseFilter.ApplyFilter(input, "SOBEL", "../../../images/testsobel.bmp");
            var file1 = new BitMapReading("../../../images/truesobel.bmp");
            var file2 = new BitMapReading("../../../images/testsobel.bmp");
            Assert.IsTrue(file1 == file2);
        }

        [Test]
        public void SobelXFilterTest()
        {
            BitMapReading input = new BitMapReading("../../../images/input.bmp");
            ChooseFilter.ApplyFilter(input, "SOBELX", "../../../images/testsobelx.bmp");
            var file1 = new BitMapReading("../../../images/truesobelx.bmp");
            var file2 = new BitMapReading("../../../images/testsobelx.bmp");
            Assert.IsTrue(file1 == file2);
        }

        [Test]
        public void SobelYFilterTest()
        {
            BitMapReading input = new BitMapReading("../../../images/input.bmp");
            ChooseFilter.ApplyFilter(input, "SOBELY", "../../../images/testsobely.bmp");
            var file1 = new BitMapReading("../../../images/truesobely.bmp");
            var file2 = new BitMapReading("../../../images/testsobely.bmp");
            Assert.IsTrue(file1 == file2);
        }
    }
}