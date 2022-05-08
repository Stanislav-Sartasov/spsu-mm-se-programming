import entity.Location
import org.json.simple.parser.ParseException
import report.WeatherReport
import service.weather.WeatherService
import java.io.IOException
import java.util.*

class WeatherAppCLI(
	private val location: Location,
	private val weatherServices: List<WeatherService>
) {
	private val greetings =
"""
This app collects and prints weather report in ${location.name}
Type 'quit' to exit of application
Type 'refresh' to refresh the weather data
"""

	private val scanner = Scanner(System.`in`)

	fun run() {
		println(greetings)
		refresh()

		while (true) {
			when (nextCommand().trim().lowercase()) {
				"quit" -> break
				"refresh" -> refresh()
				else -> println("Incorrect command. Try again")
			}
		}
	}

	private fun refresh() = collectReports().forEach {
		println("${it.first.name}:")
		printReport(it.second)
		println()
	}

	private fun collectReports(): List<Pair<WeatherService, WeatherReport>> {
		val result = mutableListOf<Pair<WeatherService, WeatherReport>>()
		weatherServices.forEach {
			try {
				result.add(it to it.getWeatherReportOf(location))
			} catch(pe: ParseException) {
				println("${it.name}: cannot create report of data that service returned")
			} catch (ioe: IOException) {
				println("${it.name}: cannot get report")
			}
		}
		return result
	}

	private fun printReport(report: WeatherReport) {
		println(formatData(report.temperature?.celsius, "Temperature", " °C"))
		println(formatData(report.temperature?.fahrenheit, "Temperature", " °F"))
		println(formatData(report.cloudiness?.percent, "Cloudiness", "%"))
		println(formatData(report.humidity?.percent, "Humidity", "%"))
		println(formatData(report.precipitation?.mmPerHour, "Precipitation", " mm/h"))
		println(formatData(report.windDirection?.degrees, "Wind direction", "°"))
		println(formatData(report.windVelocity?.metersPerSecond, "Wind velocity", " m/s"))
	}

	private fun <T> formatData(data: T?,  quantity: String, units: String): String {
		if (data == null)
			return "$quantity: no data"
		return "$quantity: $data$units"
	}

	private fun nextCommand(): String = scanner.nextLine()
}