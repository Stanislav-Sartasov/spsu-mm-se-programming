using NUnit.Framework;
using Task1;
using System.IO;
using System;


namespace Task1.Tests
{
    public class GaussFilter
    {

        [Test]

        public void Gauss()
        {
            FileOperations image = new FileOperations();
            Filters filters = new Filters();
            BitmapFile bitmapFile = new BitmapFile();
            BitmapFile.BitmapFileHeader BMPFH = new BitmapFile.BitmapFileHeader();
            BitmapFile.BitmapInfoHeader BMPIH = new BitmapFile.BitmapInfoHeader();

            FileStream fsT = new FileStream(@"..\..\..\..\Task1.Test\TestFiles\sunG3_t.bmp", FileMode.Open);
            BinaryReader brT = new BinaryReader(fsT, System.Text.Encoding.Default);

            image.ReadFile(ref BMPFH, ref BMPIH, ref brT);

            BitmapFile.Pixel[,] masT = new BitmapFile.Pixel[BMPIH.Hight + 4, BMPIH.Width + 4];

            image.Rat(ref BMPFH, ref BMPIH, masT);

            image.ReadPixel(ref BMPFH, ref BMPIH, masT, ref brT);

            brT.Close();

            FileStream fS = new FileStream(@"..\..\..\..\Task1.Test\TestFiles\sun.bmp", FileMode.Open);
            BinaryReader bR = new BinaryReader(fS, System.Text.Encoding.Default);

            image.ReadFile(ref BMPFH, ref BMPIH, ref bR);

            BitmapFile.Pixel[,] mas = new BitmapFile.Pixel[BMPIH.Hight + 4, BMPIH.Width + 4];

            image.Rat(ref BMPFH, ref BMPIH, mas);

            image.ReadPixel(ref BMPFH, ref BMPIH, mas, ref bR);

            bR.Close();


            filters.Gauss(ref BMPFH, ref BMPIH, mas);

            FileStream fO = new FileStream(@"..\..\..\..\Task1.Test\TestFiles\sunG3_new.bmp", FileMode.Create);
            BinaryWriter bW = new BinaryWriter(fO, System.Text.Encoding.Default);

            image.Save(ref BMPFH, ref BMPIH, mas, ref bW);

            bW.Close();


            fS = new FileStream(@"..\..\..\..\Task1.Test\TestFiles\sunG3_new.bmp", FileMode.Open);
            bR = new BinaryReader(fS, System.Text.Encoding.Default);

            image.ReadFile(ref BMPFH, ref BMPIH, ref bR);

            mas = new BitmapFile.Pixel[BMPIH.Hight + 4, BMPIH.Width + 4];

            image.Rat(ref BMPFH, ref BMPIH, mas);

            image.ReadPixel(ref BMPFH, ref BMPIH, mas, ref bR);

            bR.Close();

            Assert.AreEqual(mas, masT);
        }
    }
}
