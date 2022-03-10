using Task_1.Utils;
using Task_1.Core.Image;
using Task_1.Filters;

namespace Task_1
{
	public class Program
	{
		public const string HELP_MESSAGE =
			"Params: <source image> <filter name> <output image>\n\n" +
			"Available filters:\n" +
			"Gauss \t\t- gaussian 5x5 blur\n" +
			"Grayscale \t- grayscale filter\n" +
			"Median \t\t- median 7x7 filter\n" +
			"SobelX \t\t- sobelX 3x3 filter\n" +
			"SobelY \t\t- sobelY 3x3 filter\n" +
			"Sharpen \t- sharpening 3x3 filter\n";

		public static void Main(string[] args)
		{
			try
			{
				Logger.Log("This program applies the selected filter to the given bitmap image.\n");

				// Help flag
				if (args.Length == 1 && args[0] == "--help")
				{
					Logger.Log(HELP_MESSAGE);
					return;
				}

				// Checking command line params
				if (args.Length != 3)
					throw new ArgsException("Not enough or too much command line parameters. Use --help to see details.");

				Bitmap bitmap = new Bitmap(args[0]);

				Logger.Log($"Selected the {args[1]} filter.\n");

				// Choosing and applying the filter
				bitmap = args[1] switch
				{
					"Gauss" => new Gauss().ApplyTo(bitmap),
					"Grayscale" => new Grayscale().ApplyTo(bitmap),
					"Median" => new Median().ApplyTo(bitmap),
					"SobelX" => new SobelX().ApplyTo(bitmap),
					"SobelY" => new SobelY().ApplyTo(bitmap),
					"Sharpen" => new Sharpen().ApplyTo(bitmap),
					_ => throw new ArgsException("Invalid filter name. Use --help to see available filters.")
				};

				bitmap.Save(args[2]);

				Logger.Success("Done! Check the output image!");
			}
			catch (ArgsException ex)
			{
				Logger.Error(ex.Message);
			}
			catch (IOException ex)
			{
				Logger.Error(ex.Message);
			}
			
		}
	}
}