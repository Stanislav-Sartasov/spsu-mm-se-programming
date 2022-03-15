namespace Task1
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("This program can apply filters such as averaging, Gaussian 5 by 5, Sobel by X, Y, grayscale to bmp (32 bits or 24 bits)");
            Console.WriteLine("The command line should contain the path to the source image,\nthe name of the filter(GrayScale, Median, GaussFive, SobelX, SobelY, Sobel), the path for the output image");

            if (args.Length != 3)
            {
                Console.WriteLine("There should be three arguments");
                return;
            }

            Image image = Image.ReadBmp(args[0]);
            image.ApplyFilter(args[1]);
            image.WriteBmp(args[2]);

            if (image.GeneralSuccess)
            {
                Console.WriteLine("Successfully applied the filter");
            }
            else
            {
                Console.WriteLine("Not successfully applied the filter");
            }
        }
    }
}