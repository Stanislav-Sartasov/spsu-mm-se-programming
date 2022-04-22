package model.WeatherModel;

import model.WeatherData.WeatherData;

import java.util.Map;

public class OpenWeatherMapModel extends WeatherModel {
	@Override
	public void updateState(Map<String, Integer> newStats) {
		data = new WeatherData(
				newStats.get("airTemperature"),
				newStats.get("cloudCover"),
				newStats.get("humidity"),
				newStats.get("precipitation"),
				newStats.get("windDirection"),
				newStats.get("windSpeed")
		);
	}
}
