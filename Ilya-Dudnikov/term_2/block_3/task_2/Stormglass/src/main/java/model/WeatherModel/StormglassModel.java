package model.WeatherModel;

import model.WeatherData.WeatherData;

import java.util.Map;

public class StormglassModel extends WeatherModel {
	@Override
	public void updateState(Map<String, Double> newStats) {
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
