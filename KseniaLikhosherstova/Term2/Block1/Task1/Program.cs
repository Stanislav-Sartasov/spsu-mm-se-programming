namespace Task1
{
    public class Program
    {
 
        static void Main(string[] args)
        {
            BitmapFile bmp = new BitmapFile();
            BitmapFile.BitmapFileHeader BMPFH = new BitmapFile.BitmapFileHeader();
            BitmapFile.BitmapInfoHeader BMPIH = new BitmapFile.BitmapInfoHeader();
            FileOperations image = new FileOperations();
            Filters filters = new Filters();

            string filePath;
            string fileNewPath;

            Console.WriteLine("The program processes files allowing the user to add image filter.");
            Console.Write("Enter a file path: ");

            filePath = Console.ReadLine();
            FileStream fS;

            try
            {
                fS = new FileStream(filePath, FileMode.Open);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not find file.");
                return;
            }

            BinaryReader bR = new BinaryReader(fS, System.Text.Encoding.Default);

            image.ReadFile(ref BMPFH, ref BMPIH, ref bR);

            BitmapFile.Pixel[,] mas = new BitmapFile.Pixel[BMPIH.Hight + 4, BMPIH.Width + 4];

            image.Rat(ref BMPFH, ref BMPIH, mas);

            image.ReadPixel(ref BMPFH, ref BMPIH, mas, ref bR);

            bR.Close();

            int filter;

            Console.WriteLine("Select filter: ");
            Console.WriteLine("1) greyscale \n2) median 3x3 \n3) gaussian 3х3 \n4) sobelX \n5) sobelY \nInput: ");

            filter = Convert.ToInt32(Console.ReadLine());

            switch (filter)
            {
                case 1:
                    filters.Gray(ref BMPFH, ref BMPIH, mas);
                    break;
                case 2:
                    filters.Median(ref BMPFH, ref BMPIH, mas);
                    break;
                case 3:
                    filters.Gauss(ref BMPFH, ref BMPIH, mas);
                    break;
                case 4:
                    filters.SobelX(ref BMPFH, ref BMPIH, mas);
                    break;
                case 5:
                    filters.SobelY(ref BMPFH, ref BMPIH, mas);
                    break;
                default:
                    Console.WriteLine("Could not find filter.");
                    Environment.Exit(-123214);
                    break;
            }

            Console.Write("Enter a path to save file: ");

            fileNewPath = Console.ReadLine();

            FileStream fO = new FileStream(fileNewPath, FileMode.Create);
            BinaryWriter bW = new BinaryWriter(fO, System.Text.Encoding.Default);

            image.Save(ref BMPFH, ref BMPIH, mas, ref bW);
        }
    }
}