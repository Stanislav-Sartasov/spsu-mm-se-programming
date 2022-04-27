package view;

import controller.StateController;
import model.WeatherData.WeatherData;

public class WeatherView {
	private WeatherData data;

	public WeatherView(WeatherData data) {
		this.data = data;
	}

	private String valueOrMessage(Double val) {
		if (val == null)
			return "Data unavailable :(";
		return val.toString();
	}

	public void outputData() {
		System.out.println(
				"Air Temperature, C\u00B0: " + valueOrMessage(data.airTemperatureC()) + System.lineSeparator()
				+ "Air Temperature, F\u00B0: " + valueOrMessage(data.airTemperatureF()) + System.lineSeparator()
				+ "Cloud Cover, %: " + valueOrMessage(data.cloudCover()) + System.lineSeparator()
				+ "Humidity, %: " + valueOrMessage(data.humidity()) + System.lineSeparator()
				+ "Precipitation, mm/h: " + valueOrMessage(data.precipitation()) + System.lineSeparator()
				+ "Wind Direction, \u00B0: " + valueOrMessage(data.windDirection()) + System.lineSeparator()
				+ "Wind Speed, m/s: " + valueOrMessage(data.windSpeed())
		);
	}
}
