import location.Location
import openweathermapOrg.OpenweathermapOrg
import org.junit.jupiter.api.Test
import weatherCharacteristics.WeatherCharacteristics
import kotlin.random.Random

internal class OpenweathermapOrgTests {

	@Test
	fun `getWeatherData test on openweathermapOrg`() {
		val actualData =
			OpenweathermapOrg(OPEN_WEATHER_MAP_API).getWeatherData(fieldList, Location(
				Random.nextDouble(from = 0.0, until = 90.0), Random.nextDouble(from = 0.0, until = 90.0))
			)

		assert(actualData.table.all {
			when (it.key.name) {
				"Precipitation" -> it.value.data == null
				else -> it.value.data is String
			}
		})
	}

	private companion object {
		private val fieldList = listOf(
			WeatherCharacteristics("Temperature"),
			WeatherCharacteristics("Cloud cover"),
			WeatherCharacteristics("Humidity"),
			WeatherCharacteristics("Precipitation type"),
			WeatherCharacteristics("Wind direction"),
			WeatherCharacteristics("Wind speed"),
			WeatherCharacteristics("Precipitation")
		)

		private const val OPEN_WEATHER_MAP_API = "ad50b975ff7fc9b646f54287fb5c9033"
	}
}