import openweathermapOrg.OpenweathermapOrg
import org.apache.http.impl.client.CloseableHttpClient
import org.junit.jupiter.api.Test
import tomorrowIo.TomorrowIo
import springIoc.*

class SpringIocTests {
	private companion object {
		private const val OPEN_WEATHER_MAP_API = "ad50b975ff7fc9b646f54287fb5c9033"

		private const val TOMORROW_IO_API = "9v8kjBEVTpjOfrBHtW2rwo6RtLI4SfH4"

		private val testContainer = SpringIoc()
	}

	@Test
	fun getHttpClientTest() {
		val actualClient = try {
			testContainer.client()
		}
		catch (e: Exception) {
			null
		}

		assert(actualClient is CloseableHttpClient)
	}

	@Test
	fun getOpenweathermapOrgTest() {
		val actualOpenweathermapOrg = testContainer.openweathermapOrg()

		assert(
			actualOpenweathermapOrg is OpenweathermapOrg && actualOpenweathermapOrg.appId == OPEN_WEATHER_MAP_API
		)
	}

	@Test
	fun getTomorrowIoTest() {
		val actualTomorrowIo = testContainer.tomorrowIo()

		assert(
			actualTomorrowIo is TomorrowIo && actualTomorrowIo.appId == TOMORROW_IO_API
		)
	}
}