using NUnit.Framework;

namespace Task1.UnitTests
{
    public class FilterTests
    {
        [Test]
        public void GrayScale24Test()
        {
            Image image = new Image("../../../BmpImages/Rock24.bmp");
            new GrayScale().PixelConvolution(ref image);

            image.SaveFile("../../../BmpImages/Output.bmp");
            FileAssert.AreEqual("../../../BmpImages/Output.bmp", "../../../BmpImages/Rock24GrayScale.bmp");
        }

        [Test]
        public void GrayScale32Test()
        {
            Image image = new Image("../../../BmpImages/Rock32.bmp");
            new GrayScale().PixelConvolution(ref image);

            image.SaveFile("../../../BmpImages/Output.bmp");
            FileAssert.AreEqual("../../../BmpImages/Output.bmp", "../../../BmpImages/Rock32GrayScale.bmp");
        }

        [Test]
        public void SobelX24Test()
        {
            Image image = new Image("../../../BmpImages/Rock24.bmp");
            new SobelX().PixelConvolution(ref image);

            image.SaveFile("../../../BmpImages/Output.bmp");
            FileAssert.AreEqual("../../../BmpImages/Output.bmp", "../../../BmpImages/Rock24SobelX.bmp");
        }

        [Test]
        public void SobelX32Test()
        {
            Image image = new Image("../../../BmpImages/Rock32.bmp");
            new SobelX().PixelConvolution(ref image);

            image.SaveFile("../../../BmpImages/Output.bmp");
            FileAssert.AreEqual("../../../BmpImages/Output.bmp", "../../../BmpImages/Rock32SobelX.bmp");
        }

        [Test]
        public void SobelY24Test()
        {
            Image image = new Image("../../../BmpImages/Rock24.bmp");
            new SobelY().PixelConvolution(ref image);

            image.SaveFile("../../../BmpImages/Output.bmp");
            FileAssert.AreEqual("../../../BmpImages/Output.bmp", "../../../BmpImages/Rock24SobelY.bmp");
        }

        [Test]
        public void SobelY32Test()
        {
            Image image = new Image("../../../BmpImages/Rock32.bmp");
            new SobelY().PixelConvolution(ref image);

            image.SaveFile("../../../BmpImages/Output.bmp");
            FileAssert.AreEqual("../../../BmpImages/Output.bmp", "../../../BmpImages/Rock32SobelY.bmp");
        }

        [Test]
        public void Median24Test()
        {
            Image image = new Image("../../../BmpImages/Rock24.bmp");
            new MedianFilter().PixelConvolution(ref image);

            image.SaveFile("../../../BmpImages/Output.bmp");
            FileAssert.AreEqual("../../../BmpImages/Output.bmp", "../../../BmpImages/Rock24Median.bmp");
        }

        [Test]
        public void Median32Test()
        {
            Image image = new Image("../../../BmpImages/Rock32.bmp");
            new MedianFilter().PixelConvolution(ref image);

            image.SaveFile("../../../BmpImages/Output.bmp");
            FileAssert.AreEqual("../../../BmpImages/Output.bmp", "../../../BmpImages/Rock32Median.bmp");
        }

        [Test]
        public void Gauss24Test()
        {
            Image image = new Image("../../../BmpImages/Rock24.bmp");
            new Gauss().PixelConvolution(ref image);

            image.SaveFile("../../../BmpImages/Output.bmp");
            FileAssert.AreEqual("../../../BmpImages/Output.bmp", "../../../BmpImages/Rock24Gauss.bmp");
        }

        [Test]
        public void Gauss32Test()
        {
            Image image = new Image("../../../BmpImages/Rock32.bmp");
            new Gauss().PixelConvolution(ref image);

            image.SaveFile("../../../BmpImages/Output.bmp");
            FileAssert.AreEqual("../../../BmpImages/Output.bmp", "../../../BmpImages/Rock32Gauss.bmp");
        }
    }
}