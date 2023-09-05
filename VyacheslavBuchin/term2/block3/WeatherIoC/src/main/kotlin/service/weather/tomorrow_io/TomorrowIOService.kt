package service.weather.tomorrow_io

import entity.Location
import report.WeatherReport
import service.weather.WeatherService
import service.weather.tomorrow_io.parser.TomorrowIOJSONParser
import service.weather.tomorrow_io.provider.TomorrowIOHTTPRequest

class TomorrowIOService(private val parser: TomorrowIOJSONParser) : WeatherService {
	override val name = "tomorrow.io"

	override fun getWeatherReportOf(location: Location): WeatherReport {
		val rawJson = TomorrowIOHTTPRequest(location).provideJSON()
		return parser.parse(rawJson)
	}
}