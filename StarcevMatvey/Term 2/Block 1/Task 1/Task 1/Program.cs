using System;
using static System.Console;
using System.Linq;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] filters = { "GREY", "MIDDLE", "GAUSS", "SOBELX", "SOBELY" };

            WriteLine("This program applies filters to the .BMP image\nPossible filters:");
            WriteLine("GREY, MIDDLE, GAUSS, SOBELX, SOBELY");
            if (args.Length != 3)
            {
                WriteLine("Incorrect imput: must be 3 arguments");
                return;
            }

            if (!filters.Contains(args[1]))
            {
                WriteLine("Incorrect input: filter must be from list of filters");
                return;
            }
            Image image = new Image(args[0]);
            if (!Convert.ToBoolean(image.Header[0]))
            {
                WriteLine("Unable to open file for reading");
                return;
            }

            if (args[1] == "GREY")
            {
                image.GreyFilter();
            }
            else if (args[1] == "MIDDLE")
            {
                image.MiddleFilter();
            }
            else if (args[1] == "GAUSS")
            {
                image.GaussFilter();
            }
            else if (args[1] == "SOBELX")
            {
                image.SobelAxisFilter(0);
            }
            else
            {
                image.SobelAxisFilter(1);
            }

            if (Convert.ToBoolean(image.MakeNewFile(args[2])))
            {
                WriteLine("Unable to open file for writing");
                return;
            }
            WriteLine("Filtered");
        }
    }
}
