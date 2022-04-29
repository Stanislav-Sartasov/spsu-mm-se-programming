import controller.StateController;
import model.JSONParser.OpenWeatherMapParser;
import model.JSONParser.StormglassParser;
import model.WeatherModel.OpenWeatherMapModel;
import model.WeatherModel.StormglassModel;
import model.WeatherService.OpenWeatherMap;
import model.WeatherService.StormglassWeather;
import org.springframework.beans.BeansException;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;
import view.MainView;
import view.OpenWeatherMapView;
import view.StormglassView;
import view.WeatherView;


import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Locale;
import java.util.Scanner;

public class App {

	public static void main(String[] args) {
		MainView view = new MainView();

		AnnotationConfigApplicationContext context = new AnnotationConfigApplicationContext(DIConfiguration.class);

		for (var serviceName : args) {
			try {
				WeatherView weatherView = (WeatherView) context.getBean(
						serviceName.substring(0, 1).toLowerCase(Locale.ROOT)
								+ serviceName.substring(1)
								+ "View"
				);

				StateController controller = (StateController) context.getBean(
						serviceName.substring(0, 1).toLowerCase(Locale.ROOT)
								+ serviceName.substring(1)
								+ "StateController"
				);

				view.addService(weatherView, controller);
			} catch (BeansException e) {
				System.out.println(serviceName + ": Unknown Service");
			}
		}

		System.out.println();
		CliApp.run(view);
	}
}
