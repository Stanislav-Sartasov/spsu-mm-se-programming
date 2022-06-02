package controller

import entity.Location
import javafx.collections.FXCollections
import javafx.collections.ObservableList
import report.WeatherReport
import service.weather.WeatherService
import tornadofx.*

class ReportController : Controller() {
	val sourcesWithReports: ObservableList<Pair<WeatherService, WeatherReport>> = FXCollections.observableArrayList()

	fun refreshReportsIn(location: Location, services: List<WeatherService>): List<Pair<WeatherService, WeatherReport>> {
		sourcesWithReports.clear()
		sourcesWithReports.addAll(services.map {
			it to try {
				it.getWeatherReportOf(location)
			} catch(ignored: Exception) {
				WeatherReport()
			}
		})
		return sourcesWithReports
	}
}