namespace Task1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("This program can convert 24 and 32 bit bmp files. Under different filters such as:");
            Console.WriteLine("GrayScale, Median, Gauss, SobelX, SobelY.");

            if (args.Length != 3)
            {
                Console.WriteLine("Incorrect input");
                Console.WriteLine("The input should look like this: input.bmp filterName output.bmp");
                return;
            }
            
            Image bmpImage = new Image(args[0]);

			switch (args[1])
			{
				case "GrayScale":
					new GrayScale().PixelConvolution(ref bmpImage);
					break;
				case "Median":
					new MedianFilter().PixelConvolution(ref bmpImage);
					break;
				case "Gauss":
					new Gauss().PixelConvolution(ref bmpImage);
					break;
				case "SobelX":
					new SobelX().PixelConvolution(ref bmpImage);
					break;
				case "SobelY":
					new SobelY().PixelConvolution(ref bmpImage);
					break;
				default:
					Console.WriteLine("Unknown filter selected. Available names: GrayScale, Median, Gauss, SobelX, SobelY.");
					return;
			}

			bmpImage.SaveFile(args[2]);

			Console.WriteLine("The filter has been applied.");
		}
    }
}

