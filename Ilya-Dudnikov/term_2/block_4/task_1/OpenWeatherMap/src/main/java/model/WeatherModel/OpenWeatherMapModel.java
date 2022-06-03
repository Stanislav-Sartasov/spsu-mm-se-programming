package model.WeatherModel;

import model.WeatherData.WeatherData;

import java.util.Map;

public class OpenWeatherMapModel extends WeatherModel {
	public OpenWeatherMapModel(WeatherData data) {
		super(data);
	}

	@Override
	public void updateState(Map<String, Double> newStats) {
		if (newStats == null)
			return;

		data = new WeatherData(
				newStats.get("airTemperatureC"),
				newStats.get("airTemperatureF"),
				newStats.get("cloudCover"),
				newStats.get("humidity"),
				newStats.get("precipitation"),
				newStats.get("windDirection"),
				newStats.get("windSpeed")
		);
	}
}
