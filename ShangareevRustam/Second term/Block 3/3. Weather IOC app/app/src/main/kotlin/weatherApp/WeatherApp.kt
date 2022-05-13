package weatherApp

import weatherUtilities.Location
import weatherUtilities.WeatherCharacteristics
import weatherDataFormatter.WeatherDataFormatter
import weatherWeb.WeatherWeb
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter

object WeatherApp {

	fun run(arrayOfSites: Array<String>, vararg commands: String) {
		println(WELCOME_MESSAGE)

		if (arrayOfSites.isEmpty()) {
			println(NOT_ENOUGH_ARGUMENTS_ERROR)
			return
		}

		println(HELP_MESSAGE)
		var lastRequest = 0L

		val weatherWebList = arrayOfSites.mapNotNull { weatherWebs[it] }

		val listOfCommands = commands.toMutableList()
		while (true) {
			if (listOfCommands.isEmpty() && commands.isNotEmpty()) break

			when (
				if (listOfCommands.isEmpty()) readln()
				else listOfCommands.removeFirst()
			) {
				"r" -> {
					if (System.currentTimeMillis() - lastRequest < PRINT_WEATHER_INTERVAL) {
						println(OFTEN_MESSAGE)
					} else {
						printCurrentWeather(weatherWebList)
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

		for (service in weatherWebs.values) {
			service.httpClient.close()
		}
	}

	private fun printCurrentWeather(weatherWebList: List<WeatherWeb>) {
		val dataList = weatherWebList.map {
			it.getWeatherData(fieldList, Location(ST_PETERSBURG_LAT, ST_PETERSBURG_LON))
		}

		for (data in dataList) {
			if (data.table.all { it.value.data == null }) {
				println(String.format(SERVICE_UNAVAILABLE_MESSAGE, data.serviceName))
			}
		}

		val table = WeatherDataFormatter.makePrintableWeatherData(
			WeatherDataFormatter.formatWeatherData(*dataList.toTypedArray())
		)

		if (table.isNotEmpty()) {
			println()
			println(String.format(WEATHER_PRESENTATION_MESSAGE,
				LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"))))
			for (line in table) println(line)
			println()
		}
		else {
			println()
			println(CONNECT_ERROR_MESSAGE)
			println()
		}
	}

	private const val PRINT_WEATHER_INTERVAL = 60000L

	private const val UNKNOWN_COMMAND_MESSAGE = "Unknown command! Please use the help below"

	private const val WEATHER_PRESENTATION_MESSAGE =
		"Current weather conditions in Saint Petersburg, Russia. Data for %s"

	private const val SERVICE_UNAVAILABLE_MESSAGE =
		"Attention! An error occurred while receiving data from %s." +
				" Most likely the service is not available at the moment"

	private const val CONNECT_ERROR_MESSAGE =
		"An error occurred while connecting to the services. Incorrect URLs may have been given"

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


	private const val ST_PETERSBURG_LAT = 59.9055

	private const val ST_PETERSBURG_LON = 30.3007

	private val fieldList = listOf(
		WeatherCharacteristics("Temperature"),
		WeatherCharacteristics("Cloud cover"),
		WeatherCharacteristics("Humidity"),
		WeatherCharacteristics("Precipitation type"),
		WeatherCharacteristics("Wind direction"),
		WeatherCharacteristics("Wind speed"),
		WeatherCharacteristics("Precipitation")
	)

	private val iocContainer = SpringIoc()

	private val weatherWebs = mapOf(
		"openweathermap.org" to iocContainer.openweathermapOrg(),
		"tomorrow.io" to iocContainer.tomorrowIo()
	)

	private val NOT_ENOUGH_ARGUMENTS_ERROR =
		"Error! For the application to work," +
				" you must pass at least one weather service as an argument.\n" +
				"Available services: ${weatherWebs.keys.map { it -> "\"" + it + "\"" }} (Those that do not match them will be ignored)\n" +
				"Run template: gradle run --args='ARGUMENTS'"
}
