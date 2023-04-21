package service.weather

import entity.Location
import report.WeatherReport

interface WeatherService {

	val name: String

	fun getWeatherReportOf(location: Location): WeatherReport

}