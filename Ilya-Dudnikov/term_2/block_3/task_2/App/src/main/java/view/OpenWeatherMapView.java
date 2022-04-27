package view;

import controller.StateController;
import model.WeatherData.WeatherData;

public class OpenWeatherMapView extends WeatherView {
	public OpenWeatherMapView(WeatherData data) {
		super(data);
	}

	public void outputData() {
		System.out.println("OpenWeatherMap");
		super.outputData();
	}
}
