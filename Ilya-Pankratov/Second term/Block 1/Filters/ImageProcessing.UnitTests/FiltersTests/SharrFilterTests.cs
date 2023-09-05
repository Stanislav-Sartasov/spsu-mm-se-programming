﻿using NUnit.Framework;
using System.IO;

namespace ImageProcessing.UnitTests
{
    public class SharrFilterTests
    { 
        [Test]
        public void ApplySharrFilterFromFileTest()
        {
            // preperation 

            FileStream inputFile = new FileStream("..\\..\\..\\Resources\\OriginalCat.bmp", FileMode.Open, FileAccess.Read);
            FileStream expectedOutputFile = new FileStream("..\\..\\..\\Resources\\SharrFilterCat.bmp", FileMode.Open, FileAccess.Read);

            byte[] temp = new byte[54];
            inputFile.Read(temp, 0, 54);
            BMPFileHeader bmpInfo = new BMPFileHeader(temp);
            SharrFilter image = new SharrFilter(bmpInfo, inputFile);

            // checking

            image.ApplySharrFilter(SharrFilter.FilterType.SharrFilter);
            byte[] outputFile = new byte[bmpInfo.FileSize];
            image.WriteImage(outputFile);

            for (uint i = 0; i < bmpInfo.FileSize; i++)
            {
                Assert.IsTrue(outputFile[i] == (byte)expectedOutputFile.ReadByte());
            }
        }

        [Test]
        public void ApplySobelXFilterFromFileTest()
        {
            // preperation 

            FileStream inputFile = new FileStream("..\\..\\..\\Resources\\OriginalCat.bmp", FileMode.Open, FileAccess.Read);
            FileStream expectedOutputFile = new FileStream("..\\..\\..\\Resources\\SharrXFilterCat.bmp", FileMode.Open, FileAccess.Read);

            byte[] temp = new byte[54];
            inputFile.Read(temp, 0, 54);
            BMPFileHeader bmpInfo = new BMPFileHeader(temp);
            SharrFilter image = new SharrFilter(bmpInfo, inputFile);

            // checking

            image.ApplySharrFilter(SharrFilter.FilterType.SharrFilterX);
            byte[] outputFile = new byte[bmpInfo.FileSize];
            image.WriteImage(outputFile);

            for (uint i = 0; i < bmpInfo.FileSize; i++)
            {
                Assert.IsTrue(outputFile[i] == (byte)expectedOutputFile.ReadByte());
            }
        }

        [Test]
        public void ApplySobelYFilterFromFileTest()
        {
            // preperation 

            FileStream inputFile = new FileStream("..\\..\\..\\Resources\\OriginalCat.bmp", FileMode.Open, FileAccess.Read);
            FileStream expectedOutputFile = new FileStream("..\\..\\..\\Resources\\SharrYFilterCat.bmp", FileMode.Open, FileAccess.Read);

            byte[] temp = new byte[54];
            inputFile.Read(temp, 0, 54);
            BMPFileHeader bmpInfo = new BMPFileHeader(temp);
            SharrFilter image = new SharrFilter(bmpInfo, inputFile);

            // checking

            image.ApplySharrFilter(SharrFilter.FilterType.SharrFilterY);
            byte[] outputFile = new byte[bmpInfo.FileSize];
            image.WriteImage(outputFile);

            for (uint i = 0; i < bmpInfo.FileSize; i++)
            {
                Assert.IsTrue(outputFile[i] == (byte)expectedOutputFile.ReadByte());
            }

            inputFile.Close();
            expectedOutputFile.Close();
        }
    }
}
