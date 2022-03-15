using NUnit.Framework;
using System;

namespace Task1.UnitTests
{
    public class FilterTests
    {
        private string WorkingDirectory = "../../../TestFiles/";

        [Test]
        public void GrayScale24Test()
        {
            Image image = Image.ReadBmp(WorkingDirectory + "InImage24.bmp");
            image.ApplyFilter("GrayScale");
            image.WriteBmp(WorkingDirectory + "OutImage.bmp");
            FileAssert.AreEqual(WorkingDirectory + "OutImage.bmp", WorkingDirectory + "grayscale24.bmp");
            Assert.Pass();
        }

        [Test]
        public void GrayScale32Test()
        {
            Image image = Image.ReadBmp(WorkingDirectory + "InImage32.bmp");
            image.ApplyFilter("GrayScale");
            image.WriteBmp(WorkingDirectory + "OutImage.bmp");
            FileAssert.AreEqual(WorkingDirectory + "OutImage.bmp", WorkingDirectory + "grayscale32.bmp");
            Assert.Pass();
        }

        [Test]
        public void Median24Test()
        {
            Image image = Image.ReadBmp(WorkingDirectory + "InImage24.bmp");
            image.ApplyFilter("Median");
            image.WriteBmp(WorkingDirectory + "OutImage.bmp");
            FileAssert.AreEqual(WorkingDirectory + "OutImage.bmp", WorkingDirectory + "median24.bmp");
            Assert.Pass();
        }

        [Test]
        public void Median32Test()
        {
            Image image = Image.ReadBmp(WorkingDirectory + "InImage32.bmp");
            image.ApplyFilter("Median");
            image.WriteBmp(WorkingDirectory + "OutImage.bmp");
            FileAssert.AreEqual(WorkingDirectory + "OutImage.bmp", WorkingDirectory + "median32.bmp");
            Assert.Pass();
        }

        [Test]
        public void Gauss24Test()
        {
            Image image = Image.ReadBmp(WorkingDirectory + "InImage24.bmp");
            image.ApplyFilter("GaussFive");
            image.WriteBmp(WorkingDirectory + "OutImage.bmp");
            FileAssert.AreEqual(WorkingDirectory + "OutImage.bmp", WorkingDirectory + "gauss24.bmp");
            Assert.Pass();
        }

        [Test]
        public void Gauss32Test()
        {
            Image image = Image.ReadBmp(WorkingDirectory + "InImage32.bmp");
            image.ApplyFilter("GaussFive");
            image.WriteBmp(WorkingDirectory + "OutImage.bmp");
            FileAssert.AreEqual(WorkingDirectory + "OutImage.bmp", WorkingDirectory + "gauss32.bmp");
            Assert.Pass();
        }

        [Test]
        public void Sobel24Test()
        {
            Image image = Image.ReadBmp(WorkingDirectory + "InImage24.bmp");
            image.ApplyFilter("Sobel");
            image.WriteBmp(WorkingDirectory + "OutImage.bmp");
            FileAssert.AreEqual(WorkingDirectory + "OutImage.bmp", WorkingDirectory + "sobel24.bmp");
            Assert.Pass();
        }

        [Test]
        public void Sobel32Test()
        {
            Image image = Image.ReadBmp(WorkingDirectory + "InImage32.bmp");
            image.ApplyFilter("Sobel");
            image.WriteBmp(WorkingDirectory + "OutImage.bmp");
            FileAssert.AreEqual(WorkingDirectory + "OutImage.bmp", WorkingDirectory + "sobel32.bmp");
            Assert.Pass();
        }

        [Test]
        public void SobelX24Test()
        {
            Image image = Image.ReadBmp(WorkingDirectory + "InImage24.bmp");
            image.ApplyFilter("SobelX");
            image.WriteBmp(WorkingDirectory + "OutImage.bmp");
            FileAssert.AreEqual(WorkingDirectory + "OutImage.bmp", WorkingDirectory + "sobelX24.bmp");
            Assert.Pass();
        }

        [Test]
        public void SobelX32Test()
        {
            Image image = Image.ReadBmp(WorkingDirectory + "InImage32.bmp");
            image.ApplyFilter("SobelX");
            image.WriteBmp(WorkingDirectory + "OutImage.bmp");
            FileAssert.AreEqual(WorkingDirectory + "OutImage.bmp", WorkingDirectory + "sobelX32.bmp");
            Assert.Pass();
        }

        [Test]
        public void SobelY24Test()
        {
            Image image = Image.ReadBmp(WorkingDirectory + "InImage24.bmp");
            image.ApplyFilter("SobelY");
            image.WriteBmp(WorkingDirectory + "OutImage.bmp");
            FileAssert.AreEqual(WorkingDirectory + "OutImage.bmp", WorkingDirectory + "sobelY24.bmp");
            Assert.Pass();
        }

        [Test]
        public void SobelY32Test()
        {
            Image image = Image.ReadBmp(WorkingDirectory + "InImage32.bmp");
            image.ApplyFilter("SobelY");
            image.WriteBmp(WorkingDirectory + "OutImage.bmp");
            FileAssert.AreEqual(WorkingDirectory + "OutImage.bmp", WorkingDirectory + "sobelY32.bmp");
            Assert.Pass();
        }
    }
}
