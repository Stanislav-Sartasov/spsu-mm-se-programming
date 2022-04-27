package view;

import model.WeatherModel.WeatherModel;

public class WeatherView {
	protected WeatherModel weatherModel;

	public WeatherView(WeatherModel weatherModel) {
		this.weatherModel = weatherModel;
	}

	private String valueOrMessage(Double val) {
		if (val == null)
			return "Data unavailable :(";
		return val.toString();
	}

	public void outputData() {
		if (weatherModel.getData() == null) {
			System.out.println("Sorry, unable to retrieve data from that service :(");
			return;
		}

		System.out.println(
				"Air Temperature, C\u00B0: " + valueOrMessage(weatherModel.getData().airTemperatureC()) + System.lineSeparator()
				+ "Air Temperature, F\u00B0: " + valueOrMessage(weatherModel.getData().airTemperatureF()) + System.lineSeparator()
				+ "Cloud Cover, %: " + valueOrMessage(weatherModel.getData().cloudCover()) + System.lineSeparator()
				+ "Humidity, %: " + valueOrMessage(weatherModel.getData().humidity()) + System.lineSeparator()
				+ "Precipitation, mm/h: " + valueOrMessage(weatherModel.getData().precipitation()) + System.lineSeparator()
				+ "Wind Direction, \u00B0: " + valueOrMessage(weatherModel.getData().windDirection()) + System.lineSeparator()
				+ "Wind Speed, m/s: " + valueOrMessage(weatherModel.getData().windSpeed())
		);
	}
}
