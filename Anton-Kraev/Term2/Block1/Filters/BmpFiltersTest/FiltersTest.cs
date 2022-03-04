using NUnit.Framework;
using BmpFilters;

namespace BmpFiltersTest
{
    public class FiltersTest
    {
        [Test]
        public void GrayFilterTest()
        {
            var input = new BitMapReading("../../../images/input.bmp");
            ChooseFilter.ApplyFilter(input, "GRAY", "../../../images/output.bmp");
            var file1 = new BitMapReading("../../../images/gray.bmp");
            var file2 = new BitMapReading("../../../images/output.bmp");
            Assert.IsTrue(file1 == file2);
        }

        [Test]
        public void MedianFilterTest()
        {
            var input = new BitMapReading("../../../images/input.bmp");
            ChooseFilter.ApplyFilter(input, "MEDIAN", "../../../images/output.bmp");
            var file1 = new BitMapReading("../../../images/median.bmp");
            var file2 = new BitMapReading("../../../images/output.bmp");
            Assert.IsTrue(file1 == file2);
        }

        [Test]
        public void GaussFilterTest()
        {
            var input = new BitMapReading("../../../images/input.bmp");
            ChooseFilter.ApplyFilter(input, "GAUSS", "../../../images/output.bmp");
            var file1 = new BitMapReading("../../../images/gauss.bmp");
            var file2 = new BitMapReading("../../../images/output.bmp");
            Assert.IsTrue(file1 == file2);
        }

        [Test]
        public void SobelFilterTest()
        {
            var input = new BitMapReading("../../../images/input.bmp");
            ChooseFilter.ApplyFilter(input, "SOBEL", "../../../images/output.bmp");
            var file1 = new BitMapReading("../../../images/sobel.bmp");
            var file2 = new BitMapReading("../../../images/output.bmp");
            Assert.IsTrue(file1 == file2);
        }

        [Test]
        public void SobelXFilterTest()
        {
            var input = new BitMapReading("../../../images/input.bmp");
            ChooseFilter.ApplyFilter(input, "SOBELX", "../../../images/output.bmp");
            var file1 = new BitMapReading("../../../images/sobelx.bmp");
            var file2 = new BitMapReading("../../../images/output.bmp");
            Assert.IsTrue(file1 == file2);
        }

        [Test]
        public void SobelYFilterTest()
        {
            var input = new BitMapReading("../../../images/input.bmp");
            ChooseFilter.ApplyFilter(input, "SOBELY", "../../../images/output.bmp");
            var file1 = new BitMapReading("../../../images/sobely.bmp");
            var file2 = new BitMapReading("../../../images/output.bmp");
            Assert.IsTrue(file1 == file2);
        }
    }
}