package service.weather.tomorrow_io

import service.weather.WeatherServiceTest
import service.weather.tomorrow_io.parser.TomorrowIOJSONParser

internal class TomorrowIOServiceTest: WeatherServiceTest() {
	override val referenceName = "tomorrow.io"
	override val service = TomorrowIOService(
		TomorrowIOJSONParser()
	)
}