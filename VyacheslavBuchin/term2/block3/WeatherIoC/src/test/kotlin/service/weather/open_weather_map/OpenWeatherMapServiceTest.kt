package service.weather.open_weather_map

import service.weather.WeatherServiceTest

internal class OpenWeatherMapServiceTest: WeatherServiceTest() {
	override val referenceName = "openweathermap.org"
	override val service = OpenWeatherMapService()
}