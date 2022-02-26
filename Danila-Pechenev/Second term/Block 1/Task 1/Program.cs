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
            Console.WriteLine("3) Path to the output image.");

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
                code = ImageProcessor.Filter(args[0], args[2], filterType);
            }

            switch (code)
            {
                case 1:
                    Console.WriteLine("ERROR: File wasn't found.");
                    break;
                case 2:
                    Console.WriteLine("ERROR: Nonexistent type of filter.");
                    break;
                default:
                    Console.WriteLine("The program has worked successfully.");
                    break;
            }
        }
    }
}