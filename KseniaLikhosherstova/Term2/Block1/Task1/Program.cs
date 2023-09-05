namespace Task1
{
    public class Program
    {
        static void Main(string[] args)
        {
            BitmapFileHeader BMPFH = new BitmapFileHeader();
            BitmapInfoHeader BMPIH = new BitmapInfoHeader();
            FileOperations image = new FileOperations();
            Filters filters = new Filters();

            Console.WriteLine("The program processes files allowing the user to change the image filter.");
            Console.WriteLine("Available filters: grayscale, median, gaussian, sobelX, sobelY.");

            if (args.Length != 3)
            {
                Console.WriteLine("You need to enter three arguments.");
                return;
            }

            FileStream fS;

            try
            {
                fS = new FileStream(args[0], FileMode.Open); 
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not find file.");
                return;
            }

            BinaryReader bR = new BinaryReader(fS, System.Text.Encoding.Default);

            image.ReadFile(ref BMPFH, ref BMPIH, ref bR);

            Pixel[,] mas = new Pixel[BMPIH.Hight + 4, BMPIH.Width + 4];

            image.Rat(ref BMPFH, ref BMPIH, mas);

            image.ReadPixel(ref BMPFH, ref BMPIH, mas, ref bR);

            bR.Close();

            string filter = args[1];


            switch (filter)
            {
                case "grayscale":
                    filters.Gray(ref BMPFH, ref BMPIH, mas);
                    break;
                case "median":
                    filters.Median(ref BMPFH, ref BMPIH, mas);
                    break;
                case "gaussian":
                    filters.Gauss(ref BMPFH, ref BMPIH, mas);
                    break;
                case "sobelX":
                    filters.SobelX(ref BMPFH, ref BMPIH, mas);
                    break;
                case "sobelY":
                    filters.SobelY(ref BMPFH, ref BMPIH, mas);
                    break;
                default:
                    Console.WriteLine("Could not find filter.");
                    Environment.Exit(-123214);
                    break;
            }


            FileStream fO = new FileStream(args[2], FileMode.Create);
            BinaryWriter bW = new BinaryWriter(fO, System.Text.Encoding.Default);
            image.Save(ref BMPFH, ref BMPIH, mas, ref bW);

            Console.WriteLine("Filter has been applied!");
        }
    }
}