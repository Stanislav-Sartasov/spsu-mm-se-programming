import org.apache.http.impl.client.HttpClients
import weatherUtilities.Location
import org.junit.jupiter.api.Test
import tomorrowIo.TomorrowIo
import weatherUtilities.WeatherCharacteristics
import kotlin.random.Random

internal class TomorrowIoTests {

	@Test
	fun `getWeatherData test on tomorrowIo`() {
		val actualData =
			TomorrowIo(TOMORROW_IO_API, HttpClients.createDefault()).getWeatherData(fieldList, Location(
				Random.nextDouble(from = 0.0, until = 90.0), Random.nextDouble(from = 0.0, until = 90.0))
			)

		assert(actualData.table.all { it.value.data is String })
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

		private const val TOMORROW_IO_API = "9v8kjBEVTpjOfrBHtW2rwo6RtLI4SfH4"
	}
}