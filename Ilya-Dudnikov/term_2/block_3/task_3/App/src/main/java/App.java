import controller.StateController;
import model.JSONParser.OpenWeatherMapParser;
import model.JSONParser.StormglassParser;
import model.WeatherModel.OpenWeatherMapModel;
import model.WeatherModel.StormglassModel;
import model.WeatherService.OpenWeatherMap;
import model.WeatherService.StormglassWeather;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;
import view.MainView;
import view.OpenWeatherMapView;
import view.StormglassView;
import view.WeatherView;


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

		AnnotationConfigApplicationContext context = new AnnotationConfigApplicationContext(DIConfiguration.class);

		WeatherView stormglassView = context.getBean(StormglassView.class);
		StateController stormglassController = (StateController) context.getBean("stormglassStateController");
		WeatherView openWeatherMapView = context.getBean(OpenWeatherMapView.class);
		StateController openWeatherMapController = (StateController) context.getBean("openWeatherMapStateController");

		view.addService(stormglassView, stormglassController);
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
