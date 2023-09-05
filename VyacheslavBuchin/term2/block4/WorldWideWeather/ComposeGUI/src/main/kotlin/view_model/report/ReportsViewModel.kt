package view_model.report

import androidx.compose.runtime.mutableStateListOf
import entity.Location
import report.WeatherReport
import service.weather.WeatherService
import kotlin.concurrent.thread

class ReportsViewModel {
	val reports = mutableStateListOf<Pair<String, WeatherReport>>()

	fun updateReports(location: Location, services: List<WeatherService>) = thread {
			reports.clear()
			services.forEach {
				var report = WeatherReport()
				try {
					report = it.getWeatherReportOf(location)
				} catch (ignored: Exception) {
				}
				reports.add(
					it.name to report
				)
			}
		}
}