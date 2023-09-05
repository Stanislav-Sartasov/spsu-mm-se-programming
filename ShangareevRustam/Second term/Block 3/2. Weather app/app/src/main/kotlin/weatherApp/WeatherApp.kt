package weatherApp

import location.*
import openweathermapOrg.*
import tomorrowIo.*
import weatherCharacteristics.*
import weatherDataFormatter.*
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter

object WeatherApp {

	private val fieldList = listOf(WeatherCharacteristics("Temperature"),
		WeatherCharacteristics("Cloud cover"),
		WeatherCharacteristics("Humidity"),
		WeatherCharacteristics("Precipitation type"),
		WeatherCharacteristics("Wind direction"),
		WeatherCharacteristics("Wind speed"),
		WeatherCharacteristics("Precipitation"))

	fun run(vararg commands: String) {
		println(WELCOME_MESSAGE)
		println(HELP_MESSAGE)
		var lastRequest = 0L

		val listOfCommands = commands.toMutableList()

		while (true) {
			if (listOfCommands.isEmpty() && commands.isNotEmpty()) break

			when (
				if (listOfCommands.isNotEmpty()) listOfCommands.removeFirst()
				else readln()
			) {
				"r" -> {
					if (System.currentTimeMillis() - lastRequest < PRINT_WEATHER_INTERVAL) {
						println(OFTEN_MESSAGE)
					} else {
						printCurrentWeather()
						lastRequest = System.currentTimeMillis()
					}
				}
				"q" -> {
					println(EXIT_MESSAGE)
					break
				}
				"h" -> {
					println(HELP_MESSAGE)
				}
				else -> {
					println(UNKNOWN_COMMAND_MESSAGE)
					println(HELP_MESSAGE)
				}
			}
		}
	}


	private fun printCurrentWeather() {

		val dataList = arrayOf(OpenweathermapOrg(OPEN_WEATHER_MAP_API).getWeatherData(fieldList,
			Location(ST_PETERSBURG_LAT, ST_PETERSBURG_LON)),
			TomorrowIo(TOMORROW_IO_API).getWeatherData(fieldList, Location(ST_PETERSBURG_LAT, ST_PETERSBURG_LON))
		)

		for (data in dataList) {
			if (data.table.all { it.value.data == null }) {
				println(String.format(SERVICE_UNAVAILABLE_MESSAGE, data.serviceName))
			}
		}

		val table = WeatherDataFormatter.makePrintableWeatherData(WeatherDataFormatter.formatWeatherData(*dataList))

		println()
		println(String.format(WEATHER_PRESENTATION_MESSAGE,
			LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"))))
		for (line in table) println(line)
		println()
	}

	private const val PRINT_WEATHER_INTERVAL = 60000L

	private const val UNKNOWN_COMMAND_MESSAGE = "Unknown command! Please use the help below"

	private const val WEATHER_PRESENTATION_MESSAGE =
		"Current weather conditions in Saint Petersburg, Russia. Data for %s"

	private const val SERVICE_UNAVAILABLE_MESSAGE =
		"Attention! An error occurred while receiving data from %s." + " Most likely the service is not available at the moment"

	private const val EXIT_MESSAGE = "Thank you for using the app! Exiting the application is in progress"

	private const val OFTEN_MESSAGE =
		"Please don't make requests so often!" +
				" Use previously obtained data, they are still relevant, or try again later.\n" +
				"Intervals between updates of weather information should be at least" +
				" ${PRINT_WEATHER_INTERVAL / 1000L} seconds!\n"

	private const val WELCOME_MESSAGE =
		"Welcome to the app for viewing weather conditions in St. Petersburg, Russia.\n"

	private const val HELP_MESSAGE =
		"----------------------------------------\n" +
				"List of commands to use the application:\n" +
				"	\"r\" - show/update weather data\n" +
				"	\"q\" - exit the application\n" +
				"	\"h\"(any other string except \"r\" and \"q\") - get help\n" +
				"----------------------------------------\n"

	private const val OPEN_WEATHER_MAP_API = "ad50b975ff7fc9b646f54287fb5c9033"

	private const val TOMORROW_IO_API = "9v8kjBEVTpjOfrBHtW2rwo6RtLI4SfH4"

	private const val ST_PETERSBURG_LAT = 59.9055

	private const val ST_PETERSBURG_LON = 30.3007
}
