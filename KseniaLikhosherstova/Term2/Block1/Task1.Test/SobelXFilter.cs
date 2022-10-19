using NUnit.Framework;
using System.IO;


namespace Task1.Tests
{
    public class SobelXFilter
    {

        [Test]
        public void SobelX()
        {
            FileOperations image = new FileOperations();
            Filters filters = new Filters();
            BitmapFileHeader bitmapFile = new BitmapFileHeader();
            BitmapFileHeader BMPFH = new BitmapFileHeader();
            BitmapInfoHeader BMPIH = new BitmapInfoHeader();



            FileStream fsT = new FileStream(@"..\..\..\..\..\Task1\Task1.Test\TestFiles\sunSX_t.bmp", FileMode.Open);
            BinaryReader brT = new BinaryReader(fsT, System.Text.Encoding.Default);

            image.ReadFile(ref BMPFH, ref BMPIH, ref brT);

            Pixel[,] masT = new Pixel[BMPIH.Hight + 4, BMPIH.Width + 4];

            image.Rat(ref BMPFH, ref BMPIH, masT);

            image.ReadPixel(ref BMPFH, ref BMPIH, masT, ref brT);

            brT.Close();

            FileStream fS = new FileStream(@"..\..\..\..\..\Task1\Task1.Test\TestFiles\sun.bmp", FileMode.Open);
            BinaryReader bR = new BinaryReader(fS, System.Text.Encoding.Default);

            image.ReadFile(ref BMPFH, ref BMPIH, ref bR);

            Pixel[,] mas = new Pixel[BMPIH.Hight + 4, BMPIH.Width + 4];

            image.Rat(ref BMPFH, ref BMPIH, mas);

            image.ReadPixel(ref BMPFH, ref BMPIH, mas, ref bR);

            bR.Close();


            filters.SobelX(ref BMPFH, ref BMPIH, mas);

            FileStream fO = new FileStream(@"..\..\..\..\..\Task1\Task1.Test\TestFiles\sunSX_new.bmp", FileMode.Create);
            BinaryWriter bW = new BinaryWriter(fO, System.Text.Encoding.Default);

            image.Save(ref BMPFH, ref BMPIH, mas, ref bW);

            bW.Close();


            fS = new FileStream(@"..\..\..\..\..\Task1\Task1.Test\TestFiles\sunSX_new.bmp", FileMode.Open);
            bR = new BinaryReader(fS, System.Text.Encoding.Default);

            image.ReadFile(ref BMPFH, ref BMPIH, ref bR);

            mas = new Pixel[BMPIH.Hight + 4, BMPIH.Width + 4];

            image.Rat(ref BMPFH, ref BMPIH, mas);

            image.ReadPixel(ref BMPFH, ref BMPIH, mas, ref bR);

            bR.Close();

            Assert.AreEqual(mas, masT);
        }
    }
}
