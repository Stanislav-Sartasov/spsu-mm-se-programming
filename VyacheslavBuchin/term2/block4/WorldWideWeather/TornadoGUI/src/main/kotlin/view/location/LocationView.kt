package view.location

import controller.ReportController
import entity.Location
import javafx.collections.FXCollections
import javafx.collections.ObservableList
import report.WeatherReport
import service.weather.WeatherService
import tornadofx.*
import view.report.WeatherReportView

class LocationView(
	private val location: Location,
	reportController: ReportController
) : View() {

	private val sourcesWithReports: ObservableList<Pair<WeatherService, WeatherReport>> = FXCollections.observableArrayList()

	init {
		sourcesWithReports.bind(reportController.sourcesWithReports) { it }
	}

	override val root = vbox {
		text(location.name)
		listview(sourcesWithReports) {
			cellFormat {
				graphic = vbox { add(WeatherReportView(it.first, it.second)) }
			}
			isFocusTraversable = false
		}
	}
}