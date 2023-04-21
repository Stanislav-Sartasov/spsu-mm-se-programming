package service.weather.open_weather_map

import service.weather.WeatherServiceTest
import service.weather.open_weather_map.parser.OpenWeatherMapJSONParser

internal class OpenWeatherMapServiceTest: WeatherServiceTest() {
	override val referenceName = "openweathermap.org"
	override val service = OpenWeatherMapService(
		OpenWeatherMapJSONParser()
	)
}