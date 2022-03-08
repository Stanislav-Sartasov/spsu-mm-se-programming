using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ImageProcessing
{
    public class ApplyFilter
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This program applies filters to the BMP image.\n" +
                "Before running the program, you must pass the following arguments to it:\n" +
                "1. location of an input file\n" +
                "2. name of a filter\n" +
                "3. location of an output file\n" +
                "You can use such filters: Median, Gauss, SobelX, SobelY, Sobel, Grey, SharrX, SharrY, Sharr, Average, Negative.\n");

            if (args.Length != 3)
            {
                Console.WriteLine("You entered " + args.Length + " arguments(s), but you have to enter 3.\nTry again, please.");
                return;
            }

            string[] filters = { "Median", "Gauss", "SobelX", "SobelY", "Sobel", "Grey", "SharrX", "SharrY", "Sharr", "Average", "Negative" };
            FileStream inputFile;
            FileStream outputFile;
            byte[] temp = new byte[54];
            int cointer = 0;

            for (int i = 0; i < filters.Length; i++)
            {
                if (string.Compare(args[1], filters[i]) != 0)
                {
                    cointer++;
                }
                else
                {
                    break;
                }
            }

            if (cointer == filters.Length)
            {
                Console.WriteLine("The program does not know the fitter. Try another, please.\n");
                return;
            }

            try 
            {
                inputFile = new FileStream(args[0], FileMode.Open, FileAccess.Read);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Program can not open the file. Check the path and try again, please.");
                return;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error occurs. Try again, please.\n" + e.Message);
                return;
            }

            if (inputFile.Read(temp, 0, 54) != 54)
            {
                Console.WriteLine("The file is corrupted. Try another file, please.");
                inputFile.Close();
                return;
            }

            BMPFile bmpInfo = new BMPFile(temp);

            if (bmpInfo.FileType != "BM")
            {
                Console.WriteLine("It is not a BMP file. Try another file, please.");
                inputFile.Close();
                return;
            }

            if (bmpInfo.Compression != 0)
            {
                Console.WriteLine("There is compression in your file. The program does not work with compression. Try another file, please.");
                inputFile.Close();
                return;
            }

            if (!(bmpInfo.BitPerPixel == 24) && !(bmpInfo.BitPerPixel == 32))
            {
                Console.WriteLine("The program handles with only 24 or 32 bits per pixel. In your file " +
                    bmpInfo.BitPerPixel + " bits per pixel. Try another file, please.");
                inputFile.Close();
                return;
            }

            try 
            {
                outputFile = new FileStream(args[args.Length - 1], FileMode.Create, FileAccess.Write);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Program can not create the file. Check the path and try again, please.");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurs. Try again please.\n" + e.Message);
                return;
            }

            if (string.Compare(args[1], "Median") == 0)
            {
                MedianFilter image = new MedianFilter(bmpInfo, inputFile);
                image.ApplyMedianFilter();
                image.WriteImage(outputFile);
            }
            else if(string.Compare(args[1], "Gauss") == 0)
            {
                GaussFilter image = new GaussFilter(bmpInfo, inputFile);
                image.ApplyGaussFilter();
                image.WriteImage(outputFile);
            }
            else if (string.Compare(args[1], "SobelX") == 0)
            {
                SobelFilter image = new SobelFilter(bmpInfo, inputFile);
                image.ApplySobelFilter(SobelFilter.FilterType.SobelFilterX);
                image.WriteImage(outputFile);
            }
            else if (string.Compare(args[1], "SobelY") == 0)
            {
                SobelFilter image = new SobelFilter(bmpInfo, inputFile);
                image.ApplySobelFilter(SobelFilter.FilterType.SobelFilterY);
                image.WriteImage(outputFile);
            }
            else if (string.Compare(args[1], "Sobel") == 0)
            {
                SobelFilter image = new SobelFilter(bmpInfo, inputFile);
                image.ApplySobelFilter(SobelFilter.FilterType.SobelFilter);
                image.WriteImage(outputFile);
            }
            else if (string.Compare(args[1], "SharrX") == 0)
            {
                SharrFilter image = new SharrFilter(bmpInfo, inputFile);
                image.ApplySharrFilter(SharrFilter.FilterType.SharrFilterX);
                image.WriteImage(outputFile);
            }
            else if (string.Compare(args[1], "SharrY") == 0)
            {
                SharrFilter image = new SharrFilter(bmpInfo, inputFile);
                image.ApplySharrFilter(SharrFilter.FilterType.SharrFilterY);
                image.WriteImage(outputFile);
            }
            else if (string.Compare(args[1], "Sharr") == 0)
            {
                SharrFilter image = new SharrFilter(bmpInfo, inputFile);
                image.ApplySharrFilter(SharrFilter.FilterType.SharrFilter);
                image.WriteImage(outputFile);
            }
            else if (string.Compare(args[1], "Average") == 0)
            {
                AverageFilter image = new AverageFilter(bmpInfo, inputFile);
                image.ApplyAverageFilter();
                image.WriteImage(outputFile);
            }
            else if (string.Compare(args[1], "Negative") == 0)
            {
                NegativeFilter image = new NegativeFilter(bmpInfo, inputFile);
                image.ApplyNegativeFilter();
                image.WriteImage(outputFile);
            }
            else if (string.Compare(args[1], "Grey") == 0)
            {
                GreyFilter image = new GreyFilter(bmpInfo, inputFile);
                image.ApplyGreyFilter();
                image.WriteImage(outputFile);
            }

            inputFile.Close();
            outputFile.Close();

            Console.WriteLine("Work has done! You may see the result at " + args[2]);
            return;
        }
    }
}
