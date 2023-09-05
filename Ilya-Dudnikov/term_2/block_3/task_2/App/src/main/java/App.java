import controller.StateController;
import model.JSONParser.OpenWeatherMapParser;
import model.JSONParser.StormglassParser;
import model.WeatherModel.OpenWeatherMapModel;
import model.WeatherModel.StormglassModel;
import model.WeatherService.OpenWeatherMap;
import model.WeatherService.StormglassWeather;
import view.MainView;
import view.OpenWeatherMapView;
import view.StormglassView;


import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Scanner;

public class App {
	private static final String UPDATE_MSG = "update";
	private static final String EXIT_MSG = "exit";

	public static void main(String[] args) {
		System.out.println(
				"Hey there! :)" + System.lineSeparator()
				+ "This application gives information about the weather at the given coordinates" + System.lineSeparator()
				+ "Sources: OpenWeatherMap, Stormglass" + System.lineSeparator()
				+ "To see the info, type \"update\"" + System.lineSeparator()
				+ "To close the application, type \"exit\"" + System.lineSeparator()
		);
		MainView view = new MainView();

		StormglassModel stormglassModel = new StormglassModel();
		StormglassView stormglassView = new StormglassView(stormglassModel);
		StormglassWeather stormglassRequestCreator = new StormglassWeather(new ArrayList<>() {{
			add("airTemperature");
			add("cloudCover");
			add("humidity");
			add("precipitation");
			add("windDirection");
			add("windSpeed");
		}});
		StormglassParser stormglassParser = new StormglassParser();
		StateController stormglassController = new StateController(stormglassModel, stormglassRequestCreator, stormglassParser);
		view.addService(stormglassView, stormglassController);

		OpenWeatherMapModel openWeatherMapModel = new OpenWeatherMapModel();
		OpenWeatherMapView openWeatherMapView = new OpenWeatherMapView(openWeatherMapModel);
		OpenWeatherMap openWeatherMapRequestCreator = new OpenWeatherMap();
		OpenWeatherMapParser openWeatherMapParser = new OpenWeatherMapParser();
		StateController openWeatherMapController = new StateController(openWeatherMapModel, openWeatherMapRequestCreator, openWeatherMapParser);
		view.addService(openWeatherMapView, openWeatherMapController);


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
