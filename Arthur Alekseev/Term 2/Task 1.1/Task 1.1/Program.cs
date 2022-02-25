using System;

namespace Task_1._1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length != 3)
			{
				Logger.LogError("Too many / not enough arguments.\nProgram shhould be called like this:\n name.exe input.bmp filter output.bmp\n");
				return;
			}
			Console.WriteLine(args[0]);
			try
			{
				Bitmap bmp = new Bitmap(args[0]);
				switch (args[1])
				{
					case "SobelX":
						new SobelXFilter().ProcessBitmap(bmp);
						break;
					case "SobelY":
						new SobelYFilter().ProcessBitmap(bmp);
						break;
					case "Gauss3":
						new GaussFilter().ProcessBitmap(bmp);
						break;
					case "Gray":
						new GrayScale().ProcessBitmap(bmp);
						break;
					case "Median":
						new MedianFilter().ProcessBitmap(bmp);
						break;
					default:
						Logger.LogError("Filter " + args[1] + " not found.");
						Logger.LogMessage("Available filters: SobelX, SobelY, Gauss3, Gray, Median");
						break;
						return;
				}

				bmp.Save(args[2]);
			}
			catch (FileNotFoundException)
			{
				Logger.LogError("File " + args[0] + "not found");
				return;
			}
			catch (IOException)
			{
				Logger.LogError("Error working with file");
				return;
			}

			Logger.LogSuccess("Successfully applied filter.");
		}
	}
}