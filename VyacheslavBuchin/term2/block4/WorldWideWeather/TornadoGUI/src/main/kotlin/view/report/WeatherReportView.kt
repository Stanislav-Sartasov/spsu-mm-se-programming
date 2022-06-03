package view.report

import report.WeatherReport
import service.weather.WeatherService
import tornadofx.*
import java.math.RoundingMode
import java.text.NumberFormat
import java.util.*

class WeatherReportView(
	private val source: WeatherService,
	private val report: WeatherReport
) : View() {
	override val root = vbox {
		text(source.name)
		text("Temperature: ${report.temperature?.celsius.view("°C")} (${report.temperature?.fahrenheit.view("°F")})")
		text("Cloudiness: ${report.cloudiness?.percent.view("%")}")
		text("Humidity: ${report.humidity?.percent.view("%")}")
		text("Precipitation: ${report.precipitation?.mmPerHour.view("mm/h")}")
		text("Wind Direction: ${report.windDirection?.degrees.view("°")}")
		text("Wind Velocity: ${report.windVelocity?.metersPerSecond.view("m/s")}")
	}

}

private fun Number?.view(measurementUnits: String): String {
	if (this == null)
		return "no data"
	val numberFormatter = NumberFormat.getNumberInstance(Locale.ENGLISH)
	numberFormatter.roundingMode = RoundingMode.HALF_UP
	numberFormatter.maximumFractionDigits = 1
	val result = numberFormatter.format(this)
	return "$result$measurementUnits"
}