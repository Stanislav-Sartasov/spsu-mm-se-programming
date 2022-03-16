using System;

namespace BitMapTask
{
    public class Program
    {
        static private readonly string[] availableFilters = { "Median", "Gauss", "SobelX", "SobelY", "Gray" };

        static void Main(string[] args)
        {
            if (ProcessRequest(args))
            {
                FarewellMessage();
            }
        }

        static private void PrintAvailableFilters()
        {
            Console.Write("Available filters: ");

            for (uint i = 0; i < availableFilters.Length - 1; i++)
            {
                Console.Write(availableFilters[i] + ", ");
            }

            Console.Write(availableFilters[availableFilters.Length - 1] + ".\n");
        }

        static private bool ProcessRequest(string[] request)
        {
            if (IsRequestCorrect(request))
            {
                BitMapImage image = new BitMapImage(request[0]);

                switch (request[1])
                {
                    case "Median":
                        Filters.Median(image);
                        break;
                    case "Gauss":
                        Filters.Gauss(image);
                        break;
                    case "SobelX":
                        Filters.SobelX(image);
                        break;
                    case "SobelY":
                        Filters.SobelY(image);
                        break;
                    case "Gray":
                        Filters.Gray(image);
                        break;
                }

                image.WriteBitMap(request[2]);         // request[2] is in availableFilters because of successful IsRequestCorrect checking

                return true;
            }
            else
            {
                return false;
            }
        }

        static private bool IsRequestCorrect(string[] request)
        {
            if (request.Length != 3)
            {
                Console.WriteLine("\nSomething wrong with number of arguments: need 3 arguments, you gave " + request.Length);

                return false;
            }

            if (Array.IndexOf(availableFilters, request[1]) == -1)
            {
                Console.WriteLine("\nSomething wrong with filterName or the order of arguments");
                PrintAvailableFilters();

                return false;
            }

            if (request[0].Length < 4 || request[0].Length < 4)
            {
                Console.Write("\nSomething wrong with path\n");

                return false;
            }

            if ((request[0].Substring(request[0].Length - 4) != ".bmp") || (request[2].Substring(request[2].Length - 4) != ".bmp"))      //  checking the extension
            {
                Console.WriteLine("\nSomething wrong with extensions of files: both files have to be .bmp files");

                return false;
            }

            return true;
        }

        static private void FarewellMessage()
        {
            Console.WriteLine("\nYour image was successfully filtered and saved to the path you have chosen\n");
        }
    }
}