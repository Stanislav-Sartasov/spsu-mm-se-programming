package model.WeatherData;

import java.util.Objects;

public record WeatherData(
		int airTemperature,
		int cloudCover,
		int humidity,
		int precipitation,
		int windDirection,
		int windSpeed) {
}
