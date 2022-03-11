using System;
using NUnit.Framework;
using System.Drawing;
using BMPFilter;
using System.IO;

namespace BMPFilterTests
{
    public class BitMapFileTests
    {
        [Test]
        public void TestExceptionForUnsupportedBitMapType()
        {
            FileStream fileIn;
            try
            {
                fileIn = new FileStream("../../../images/error_type.bmp", FileMode.Open, FileAccess.ReadWrite);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Ошибка! Входной файл не был найден!");
            }

            bool isValidFile = true;
            try
            {
                BitMapFile file = new BitMapFile(fileIn);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Неподдерживаемая битность BMP-файла."))
                {
                    isValidFile = false;
                }
            }
            Assert.IsFalse(isValidFile);
        }
    }
}
