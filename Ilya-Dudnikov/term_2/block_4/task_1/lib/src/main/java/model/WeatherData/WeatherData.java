package model.WeatherData;

public record WeatherData(
		Double airTemperatureC,
		Double airTemperatureF,
		Double cloudCover,
		Double humidity,
		Double precipitation,
		Double windDirection,
		Double windSpeed) {
	public boolean isNull() {
		return airTemperatureC == null &&
				airTemperatureF == null &&
				cloudCover == null &&
				humidity == null &&
				precipitation == null &&
				windDirection == null &&
				windSpeed == null;
	}
}
