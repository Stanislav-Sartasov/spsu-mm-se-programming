import java.io.*;
import java.util.Arrays;

public class Main {

	public static final String HELP_MESSAGE =
			"""
					This program applies following filters to BMP file
					<filter> \t\t| <filter name>
					Grayscale \t\t| grayscale
					Average \t\t| average
					Gaussian \t\t| gaussian
					Sobel X-axis \t\t| sobelX
					Sobel Y-axis \t\t| sobelY
					Minimum \t\t| min
					Maximum \t\t| max
					                
					Args: <source path> <destination path> <filter1>...
					""";

	public static String INCORRECT_ARGS_MESSAGE = "Invalid arguments\nUse --help to see details";

	public static void main(String[] args) {
		if (args.length < 3) {
			if (args.length == 1 && "--help".equals(args[0]))
				System.out.println(HELP_MESSAGE);
			else
				System.err.println(INCORRECT_ARGS_MESSAGE);
			return;
		}

		try (var fileIS = new FileInputStream(args[0]);
			 var fileOS = new FileOutputStream(args[1])) {
			new FilterApplication(fileIS,
					fileOS,
					Arrays.copyOfRange(args, 2, args.length)
			).run();
		} catch (IOException e) {
			System.err.println(e.getMessage());
		}
	}
}
