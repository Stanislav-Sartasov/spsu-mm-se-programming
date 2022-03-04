namespace BmpFilters
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This program applies one of the filters to the image.");
            Console.WriteLine("List of available filters: gray, median, gauss, sobelx, sobely, sobelxy.\n");

            if (args.Length != 3)
            {
                Console.WriteLine("Incorrect number of command line arguments");
                Console.WriteLine("Correct format: <program.exe> <input.bmp> <filtername> <output.bmp>");
                return;
            }

            BitMapReading input;
            try
            {
                input = new BitMapReading(args[0]);
            }
            catch (Exception)
            {
                Console.WriteLine("The input file could not be opened");
                return;
            }

            if (!ChooseFilter.ApplyFilter(input, args[1].ToUpper(), args[2]))
            {
                Console.WriteLine("Unknown filter selected");
                Console.WriteLine("List of available filters: gray, median, gauss, sobelx, sobely, sobelxy");
            }
        }
    }
}