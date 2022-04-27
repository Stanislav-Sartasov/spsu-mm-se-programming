package view;

import model.WeatherModel.WeatherModel;

public class OpenWeatherMapView extends WeatherView {
	public OpenWeatherMapView(WeatherModel weatherModel) {
		super(weatherModel);
	}

	public void outputData() {
		System.out.println("OpenWeatherMap");
		super.outputData();
	}
}
