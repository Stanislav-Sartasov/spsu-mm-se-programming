package model.WeatherData;

public record WeatherData(
		Double airTemperature,
		Double cloudCover,
		Double humidity,
		Double precipitation,
		Double windDirection,
		Double windSpeed) {
}
