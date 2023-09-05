package model.WeatherData;

public record WeatherData(
		Double airTemperatureC,
		Double airTemperatureF,
		Double cloudCover,
		Double humidity,
		Double precipitation,
		Double windDirection,
		Double windSpeed) {
}
