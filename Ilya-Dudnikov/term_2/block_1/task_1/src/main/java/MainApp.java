import bmp.BMPFile;
import filters.Filters;

import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;

public class MainApp {
	public static void run(String[] args) {
		System.out.println("This app applies filter to a .bmp file");

		if (args.length == 1 && (args[0].equals("-h") || args[0].equals("--help"))) {
			System.out.println(
					"Usage: gradlew clean build run <filter name> <input file> <output file>\n" +
					"Available filters: grayscale, average, gaussian, sobelX, sobelY"
			);
			return;
		}

		if (args.length != 3) {
			System.err.println("Got " + (args.length < 3 ? "less" : "more") + " arguments than expected: use -h or --help for more information");
			return;
		}

		try (FileInputStream fileInputStream = new FileInputStream(args[1])) {
			BMPFile bmpFile = BMPFile.readFromInputStream(fileInputStream);
			Filters filter = new Filters();

			switch (args[0]) {
				case "grayscale":
					filter.applyGrayscaleFilter(bmpFile.getBmpPixelStorage());
					break;
				case "average":
					filter.applyAverageFilter(bmpFile.getBmpPixelStorage());
					break;
				case "gaussian":
					filter.applyGaussianFilter(bmpFile.getBmpPixelStorage());
					break;
				case "sobelX":
					filter.applySobelXFilter(bmpFile.getBmpPixelStorage());
					break;
				case "sobelY":
					filter.applySobelYFilter(bmpFile.getBmpPixelStorage());
					break;
				default:
					System.err.println("No such filter provided, use -h or --help to see more information");
					break;
			}

			FileOutputStream fileOutputStream = new FileOutputStream(args[2]);
			bmpFile.writeToOutputStream(fileOutputStream);
		} catch (IOException e) {
			System.err.println(e.getMessage());
		}
	}
}
