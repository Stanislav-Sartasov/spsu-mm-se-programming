using System;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This program applies {0} filter to the {1} file and writes it to the {2} file", args[2], args[0], args[1]);
            if (args.Length != 3)
            {
                Console.WriteLine("Not enough or more than enough args");
                return;
            }
            string fileName = @"../../" + args[0];
            string newName = @"../../" + args[1];
            Image img = new Image();
            if(img.ReadImage(fileName) == 1)
            {
                Console.WriteLine("Please try again.");
            }
            else
                img.ApplyFilters(newName, args[2]);
            Console.WriteLine("Press enter button to exit");
            Console.ReadLine();
        }
    } 
}
