package service.weather.tomorrow_io

import service.weather.WeatherServiceTest

internal class TomorrowIOServiceTest: WeatherServiceTest() {
	override val referenceName = "tomorrow.io"
	override val service = TomorrowIOService()
}