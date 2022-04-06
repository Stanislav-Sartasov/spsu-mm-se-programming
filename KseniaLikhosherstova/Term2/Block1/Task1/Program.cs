namespace Task1
{
    public class Program
    {

        static void Main(string[] args)
        {
            BitmapFileHeader bMPFH = new BitmapFileHeader();
            BitmapInfoHeader bMPIH = new BitmapInfoHeader();
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

            image.ReadFile(ref bMPFH, ref bMPIH, ref bR);

            Pixel[,] mas = new Pixel[bMPIH.Hight + 4, bMPIH.Width + 4];

            image.Rat(ref bMPFH, ref bMPIH, mas);

            image.ReadPixel(ref bMPFH, ref bMPIH, mas, ref bR);

            bR.Close();

            string filter = args[1];


            switch (filter)
            {
                case "grayscale":
                    filters.Gray(ref bMPFH, ref bMPIH, mas);
                    break;

                case "median":
                    filters.Median(ref bMPFH, ref bMPIH, mas);
                    break;

                case "gaussian":
                    filters.Gauss(ref bMPFH, ref bMPIH, mas);
                    break;

                case "sobelX":
                    filters.SobelX(ref bMPFH, ref bMPIH, mas);
                    break;

                case "sobelY":
                    filters.SobelY(ref bMPFH, ref bMPIH, mas);
                    break;

                default:
                    Console.WriteLine("Could not find filter.");
                    Environment.Exit(-123214);
                    break;
            }


            FileStream fO = new FileStream(args[2], FileMode.Create);
            BinaryWriter bW = new BinaryWriter(fO, System.Text.Encoding.Default);
            image.Save(ref bMPFH, ref bMPIH, mas, ref bW);

            Console.WriteLine("Filter has been applied.");
        }
    }
}