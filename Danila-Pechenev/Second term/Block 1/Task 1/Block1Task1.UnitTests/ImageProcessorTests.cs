using NUnit.Framework;

namespace Block1Task1.UnitTests
{
    public class ImageProcessorTests
    {
        public string WorkingDirectory = "../../../TestFiles/";

        [Test]
        public void AveragingFilter24BitTest()
        {
            double[] kernel = { 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9 };
            ImageProcessor.Filter(WorkingDirectory + "test24.bmp", WorkingDirectory + "ResultsOfNewProgram/averaging24.bmp", kernel);
            FileAssert.AreEqual(WorkingDirectory + "ResultsOfNewProgram/averaging24.bmp", WorkingDirectory + "ResultsOfLastProgram/averaging24.bmp");
        }

        [Test]
        public void AveragingFilter32BitTest()
        {
            double[] kernel = { 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9 };
            ImageProcessor.Filter(WorkingDirectory + "test32.bmp", WorkingDirectory + "ResultsOfNewProgram/averaging32.bmp", kernel);
            FileAssert.AreEqual(WorkingDirectory + "ResultsOfNewProgram/averaging32.bmp", WorkingDirectory + "ResultsOfLastProgram/averaging32.bmp");
        }

        [Test]
        public void GaussianFilter24BitTest()
        {
            double[] kernel = { 1.0 / 16, 1.0 / 8, 1.0 / 16, 1.0 / 8, 1.0 / 4, 1.0 / 8, 1.0 / 16, 1.0 / 8, 1.0 / 16 };
            ImageProcessor.Filter(WorkingDirectory + "test24.bmp", WorkingDirectory + "ResultsOfNewProgram/gaussian24.bmp", kernel);
            FileAssert.AreEqual(WorkingDirectory + "ResultsOfNewProgram/gaussian24.bmp", WorkingDirectory + "ResultsOfLastProgram/gaussian24.bmp");
        }

        [Test]
        public void GaussianFilter32BitTest()
        {
            double[] kernel = { 1.0 / 16, 1.0 / 8, 1.0 / 16, 1.0 / 8, 1.0 / 4, 1.0 / 8, 1.0 / 16, 1.0 / 8, 1.0 / 16 };
            ImageProcessor.Filter(WorkingDirectory + "test32.bmp", WorkingDirectory + "ResultsOfNewProgram/gaussian32.bmp", kernel);
            FileAssert.AreEqual(WorkingDirectory + "ResultsOfNewProgram/gaussian32.bmp", WorkingDirectory + "ResultsOfLastProgram/gaussian32.bmp");
        }

        [Test]
        public void SobelXFilter24BitTest()
        {
            double[] kernel = { -1.0, -2.0, -1.0, 0.0, 0.0, 0.0, 1.0, 2.0, 1.0 };
            ImageProcessor.Filter(WorkingDirectory + "test24.bmp", WorkingDirectory + "ResultsOfNewProgram/sobelx24.bmp", kernel);
            FileAssert.AreEqual(WorkingDirectory + "ResultsOfNewProgram/sobelx24.bmp", WorkingDirectory + "ResultsOfLastProgram/sobelx24.bmp");
        }

        [Test]
        public void SobelXFilter32BitTest()
        {
            double[] kernel = { -1.0, -2.0, -1.0, 0.0, 0.0, 0.0, 1.0, 2.0, 1.0 };
            ImageProcessor.Filter(WorkingDirectory + "test32.bmp", WorkingDirectory + "ResultsOfNewProgram/sobelx32.bmp", kernel);
            FileAssert.AreEqual(WorkingDirectory + "ResultsOfNewProgram/sobelx32.bmp", WorkingDirectory + "ResultsOfLastProgram/sobelx32.bmp");
        }

        [Test]
        public void SobelYFilter24BitTest()
        {
            double[] kernel = { -1.0, 0.0, 1.0, -2.0, 0.0, 2.0, -1.0, 0.0, 1.0 };
            ImageProcessor.Filter(WorkingDirectory + "test24.bmp", WorkingDirectory + "ResultsOfNewProgram/sobely24.bmp", kernel);
            FileAssert.AreEqual(WorkingDirectory + "ResultsOfNewProgram/sobely24.bmp", WorkingDirectory + "ResultsOfLastProgram/sobely24.bmp");
        }

        [Test]
        public void SobelYFilter32BitTest()
        {
            double[] kernel = { -1.0, 0.0, 1.0, -2.0, 0.0, 2.0, -1.0, 0.0, 1.0 };
            ImageProcessor.Filter(WorkingDirectory + "test32.bmp", WorkingDirectory + "ResultsOfNewProgram/sobely32.bmp", kernel);
            FileAssert.AreEqual(WorkingDirectory + "ResultsOfNewProgram/sobely32.bmp", WorkingDirectory + "ResultsOfLastProgram/sobely32.bmp");
        }

        [Test]
        public void Grayscale24BitTest()
        {
            ImageProcessor.ReadGrayscaleAndWriteImage(WorkingDirectory + "test24.bmp", WorkingDirectory + "ResultsOfNewProgram/grayscale24.bmp");
            FileAssert.AreEqual(WorkingDirectory + "ResultsOfNewProgram/grayscale24.bmp", WorkingDirectory + "ResultsOfLastProgram/grayscale24.bmp");
        }

        [Test]
        public void Grayscale32BitTest()
        {
            ImageProcessor.ReadGrayscaleAndWriteImage(WorkingDirectory + "test32.bmp", WorkingDirectory + "ResultsOfNewProgram/grayscale32.bmp");
            FileAssert.AreEqual(WorkingDirectory + "ResultsOfNewProgram/grayscale32.bmp", WorkingDirectory + "ResultsOfLastProgram/grayscale32.bmp");
        }
    }
}