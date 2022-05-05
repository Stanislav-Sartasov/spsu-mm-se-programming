import entity.*
import org.json.simple.parser.ParseException
import org.junit.jupiter.api.BeforeEach

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.*
import org.junit.jupiter.params.provider.Arguments
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import org.mockito.kotlin.doAnswer
import org.mockito.kotlin.doReturn
import org.mockito.kotlin.mock
import report.WeatherReport
import service.weather.WeatherService
import java.io.ByteArrayOutputStream
import java.io.IOException
import java.io.PrintStream

internal class WeatherAppCLITest {

	private val output = ByteArrayOutputStream()
	private var input = "quit".byteInputStream()
	private val sep = System.lineSeparator()
	private val lat = 59.94
	private val lon = 30.313
	private val location = Location("Saint-Petersburg", lat, lon)
	private val greetings =
"""
This app collects and prints weather report in ${location.name}
Type 'quit' to exit of application
Type 'refresh' to refresh the weather data
"""
	private val serviceName = "chezychez.me"

	companion object {
		@JvmStatic
		fun reportsAndViews(): List<Arguments> {
			val result = mutableListOf<Arguments>()
			for (mask in 0 until (1 shl 6)) {
				var temperature: Temperature? = null
				var temperatureCView = "no data"
				var temperatureFView = "no data"
				if ((mask and 1) == 1) {
					temperature = Temperature.ofCelsius(42.0)
					temperatureCView = "42.0 °C"
					temperatureFView = "${temperature.fahrenheit} °F"
				}

				var cloudiness: Cloudiness? = null
				var cloudinessView = "no data"
				if ((mask shr 1) and 1 == 1) {
					cloudiness = Cloudiness(0)
					cloudinessView = "0%"
				}

				var humidity: Humidity? = null
				var humidityView = "no data"
				if ((mask shr 2) and 1 == 1) {
					humidity = Humidity(0)
					humidityView = "0%"
				}

				var precipitation: Precipitation? = null
				var precipitationView = "no data"
				if ((mask shr 3) and 1 == 1) {
					precipitation = Precipitation(0.0)
					precipitationView = "0.0 mm/h"
				}

				var windDirection: WindDirection? = null
				var windDirectionView = "no data"
				if ((mask shr 4) and 1 == 1) {
					windDirection = WindDirection(228)
					windDirectionView = "228°"
				}

				var windVelocity: Velocity? = null
				var windVelocityView = "no data"
				if ((mask shr 5) and 1 == 1) {
					windVelocity = Velocity(1.337)
					windVelocityView = "1.337 m/s"
				}
				result.add(Arguments.of(
					WeatherReport(temperature, cloudiness, humidity, precipitation, windDirection, windVelocity),
					"""
					Temperature: $temperatureCView
					Temperature: $temperatureFView
					Cloudiness: $cloudinessView
					Humidity: $humidityView
					Precipitation: $precipitationView
					Wind direction: $windDirectionView
					Wind velocity: $windVelocityView
					""".trimIndent()
				))
			}
			return result
		}
	}

	@BeforeEach
	fun setUp() {
		System.setOut(PrintStream(output))
		System.setIn(input)
	}

	@Test
	fun `greetings message should be printed at the beginning of program execution`() {
		val app = WeatherAppCLI(location, listOf())
		app.run()
		assertEquals(greetings.trim(), output.toString().trim())
	}

	@ParameterizedTest
	@MethodSource("reportsAndViews")
	fun `report view should be the same as reference`(report: WeatherReport, reportView: String) {
		val service = mock<WeatherService> {
			on { name } doReturn serviceName
			on { getWeatherReportOf(location) } doReturn report
		}
		val app = WeatherAppCLI(location, listOf(service))

		app.run()

		assertEquals("$greetings$sep$serviceName:$sep$reportView".trim(), output.toString().trim())
	}

	@ParameterizedTest
	@MethodSource("reportsAndViews")
	fun `a new report should be printed if refresh command typed`(report: WeatherReport, reportView: String) {
		input = "refresh${sep}quit".byteInputStream()
		System.setIn(input)
		val service = mock<WeatherService> {
			on { name } doReturn serviceName
			on { getWeatherReportOf(location) } doReturn report
		}
		val app = WeatherAppCLI(location, listOf(service))

		app.run()

		assertEquals("$greetings$sep$serviceName:$sep$reportView$sep$sep$serviceName:$sep$reportView".trim(), output.toString().trim())
	}

	@Test
	fun `incorrect command message should be printed if given command is not quit or refresh`() {
		input = "some incorrect command${sep}quit".byteInputStream()
		System.setIn(input)
		val app = WeatherAppCLI(location, listOf())
		app.run()
		assertEquals("$greetings${sep}Incorrect command. Try again".trim(), output.toString().trim())
	}

	@Test
	fun `no access to service message should be printed if cannot take report of given service`() {
		val service = mock<WeatherService> {
			on { name } doReturn serviceName
			on { getWeatherReportOf(location) } doAnswer { throw IOException() }
		}
		val app = WeatherAppCLI(location, listOf(service))

		app.run()

		assertEquals("$greetings${sep}${serviceName}: cannot get report".trim(), output.toString().trim())
	}

	@Test
	fun `service return incorrect data message should be printed if service returned incorrect data`() {
		val service = mock<WeatherService> {
			on { name } doReturn serviceName
			on { getWeatherReportOf(location) } doAnswer { throw ParseException(ParseException.ERROR_UNEXPECTED_EXCEPTION) }
		}
		val app = WeatherAppCLI(location, listOf(service))

		app.run()

		assertEquals("$greetings${sep}${serviceName}: cannot create report of data that service returned".trim(), output.toString().trim())
	}

}