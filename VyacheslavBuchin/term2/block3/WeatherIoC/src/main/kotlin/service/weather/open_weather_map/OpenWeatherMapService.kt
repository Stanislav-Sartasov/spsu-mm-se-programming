package service.weather.open_weather_map

import entity.Location
import report.WeatherReport
import service.weather.WeatherService
import service.weather.open_weather_map.parser.OpenWeatherMapJSONParser
import service.weather.open_weather_map.provider.OpenWeatherMapHTTPRequest

class OpenWeatherMapService(private val parser: OpenWeatherMapJSONParser) : WeatherService {

	override val name = "openweathermap.org"
	override fun getWeatherReportOf(location: Location): WeatherReport {
		val rawJson = OpenWeatherMapHTTPRequest(location).provideJSON()
		return parser.parse(rawJson)
	}
}