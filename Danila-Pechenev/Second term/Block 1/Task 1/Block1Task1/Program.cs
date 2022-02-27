namespace Block1Task1
{
    /// <summary>
    /// Console application for image processing.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point to the program.
        /// </summary>
        /// <param name="args">Сommand line arguments.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("This program processes the input image.");
            Console.WriteLine("Filters and grayscale conversion are available.");
            Console.WriteLine("The command line must include three arguments:");
            Console.WriteLine("1) Path to the input file;");
            Console.WriteLine("2) Type of processing:");
            Console.WriteLine("\taveraging [averaging filter]");
            Console.WriteLine("\tgaussian [gaussian filter]");
            Console.WriteLine("\tsobelx [sobel filter on the X axis]");
            Console.WriteLine("\tsobely [sobel filter on the Y axis]");
            Console.WriteLine("\tgrayscale [convertion to grayscale];");
            Console.WriteLine("3) Path to the output file.");

            if (args.Length != 3)
            {
                Console.WriteLine("ERROR: Incorrect arguments of the command line.");
                return;
            }

            string filterType = args[1];
            int code;
            if (filterType == "grayscale")
            {
                code = ImageProcessor.ReadGrayscaleAndWriteImage(args[0], args[2]);
            }
            else
            {
                double[] kernel;
                switch (filterType)
                {
                    case "averaging":
                        kernel = new double[9] { 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9 };
                        break;
                    case "gaussian":
                        kernel = new double[9] { 1.0 / 16, 1.0 / 8, 1.0 / 16, 1.0 / 8, 1.0 / 4, 1.0 / 8, 1.0 / 16, 1.0 / 8, 1.0 / 16 };
                        break;
                    case "sobelx":
                        kernel = new double[9] { -1.0, -2.0, -1.0, 0.0, 0.0, 0.0, 1.0, 2.0, 1.0 };
                        break;
                    case "sobely":
                        kernel = new double[9] { -1.0, 0.0, 1.0, -2.0, 0.0, 2.0, -1.0, 0.0, 1.0 };
                        break;
                    default:
                        Console.WriteLine("ERROR: Nonexistent type of filter.");
                        return;
                }

                code = ImageProcessor.Filter(args[0], args[2], kernel);
            }

            if (code == 1)
            {
                Console.WriteLine("ERROR: File wasn't found.");
                return;
            }
            else
            {
                Console.WriteLine("The program has worked successfully.");
                return;
            }
        }
    }
}