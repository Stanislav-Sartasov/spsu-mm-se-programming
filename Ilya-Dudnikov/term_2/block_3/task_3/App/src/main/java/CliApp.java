import view.MainView;

import java.util.Scanner;

public class CliApp {
	private static final String UPDATE_MSG = "update";
	private static final String EXIT_MSG = "exit";

	public static void run(MainView view) {
		System.out.println(
				"Hey there! :)" + System.lineSeparator()
						+ "This application gives information about the weather at the given coordinates" + System.lineSeparator()
						+ "Sources: OpenWeatherMap, Stormglass" + System.lineSeparator()
						+ "To see the info, type \"update\"" + System.lineSeparator()
						+ "To close the application, type \"exit\"" + System.lineSeparator()
		);

		while (true) {
			Scanner scanner = new Scanner(System.in);
			String input = scanner.nextLine();

			if (input.equals(UPDATE_MSG)) {
				view.refreshAction();
				view.outputData();
				System.out.println();
				System.out.println("\"update\" to update the weather info or \"exit\" to close the application");
			} else if (input.equals(EXIT_MSG)) {
				break;
			} else {
				System.out.println("I don't know what you want from me :(");
				System.out.println("Please, type \"update\" to get weather info or \"exit\" to close the application");
			}
		}
	}
}
