import io.mockk.every
import io.mockk.mockk
import org.apache.http.HttpEntity
import org.apache.http.client.methods.CloseableHttpResponse
import org.apache.http.impl.client.CloseableHttpClient
import org.apache.http.message.BasicHeader
import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Test
import weatherUtilities.*
import java.io.ByteArrayInputStream

internal class WeatherWebTests {

	private companion object {

		private const val serviceName = "spbu.com"

		private const val appId = "mockk12345"

		private val dataJSONInformation = mapOf(
			WeatherCharacteristics("Weather description") to UnassembledWeatherDataField(units = "",
				path = listOf("weather", "description")
			),
			WeatherCharacteristics("Feels like temperature") to UnassembledWeatherDataField(units = "deg. C",
				path = listOf("main", "feels_like")
			),
			WeatherCharacteristics("Weather icon") to UnassembledWeatherDataField(units = ".png",
				path = listOf("weather", "icon")
			),
			WeatherCharacteristics("Humidity") to UnassembledWeatherDataField(units = "percent",
				path = listOf("main", "humidity")
			),
			WeatherCharacteristics("Max temperature") to UnassembledWeatherDataField(units = "deg. F",
				path = listOf("main", "temp_max")
			),
			WeatherCharacteristics("Non-existent in json characteristic") to UnassembledWeatherDataField(units = "deg.",
				path = listOf("weather", "id", "temp")
			)
		)

		private val fieldList = listOf(
			WeatherCharacteristics("Weather description"),
			WeatherCharacteristics("Feels like temperature"),
			WeatherCharacteristics("Weather icon"),
			WeatherCharacteristics("Humidity"),
			WeatherCharacteristics("Max temperature"),
			WeatherCharacteristics("Non-existent in json characteristic"),
			WeatherCharacteristics("Non-existent characteristic")
		)

		private val testLocation = Location(30.0, 50.0)

		private val emptyWeatherCharacteristics = WeatherCharacteristicsData(null, "")
	}

	@Test
	fun `WeatherWeb getWeatherData test during service availability`() {

		val expectedWeatherDataTable = mapOf(
			WeatherCharacteristics("Weather description") to
					WeatherCharacteristicsData("light intensity shower rain", ""),
			WeatherCharacteristics("Feels like temperature") to
					WeatherCharacteristicsData("9.95", "deg. C"),
			WeatherCharacteristics("Weather icon") to
					WeatherCharacteristicsData("09d", ".png"),
			WeatherCharacteristics("Humidity") to
					WeatherCharacteristicsData("85", "percent"),
			WeatherCharacteristics("Max temperature") to
					WeatherCharacteristicsData("12.1", "deg. F"),
			WeatherCharacteristics("Non-existent in json characteristic") to
					emptyWeatherCharacteristics,
			WeatherCharacteristics("Non-existent characteristic") to
					emptyWeatherCharacteristics
		)
		val expectedJSONObjectAsString = """{"coord":{"lon":30.3007,"lat":59.9055},""" +
				""""weather":[{"id":520,"main":"Rain","description":"light intensity shower rain","icon":"09d"}],""" +
				""""base":"stations","main":{"temp":10.61,"feels_like":9.95,"temp_min":10.61,"temp_max":12.1,""" +
				""""pressure":997,"humidity":85}}"""
		val expectedJSONObjectAsByteArray = expectedJSONObjectAsString.toByteArray()
		val expectedWeatherData = WeatherData(serviceName, expectedWeatherDataTable)

		val entity = mockk<HttpEntity>(relaxUnitFun = true)
		every { entity.contentType } returns BasicHeader("Content-type", "application/json")
		every { entity.content } returns ByteArrayInputStream(expectedJSONObjectAsByteArray)
		every { entity.contentLength } returns expectedJSONObjectAsByteArray.size.toLong()

		val response = mockk<CloseableHttpResponse>(relaxUnitFun = true)
		every { response.entity } returns entity

		val httpClient = mockk<CloseableHttpClient>(relaxUnitFun = true)
		every { httpClient.execute(any()) } returns response

		val testWeatherWeb = TestWeatherWeb(httpClient, appId, serviceName, dataJSONInformation)

		assertEquals(
			expectedWeatherData,
			testWeatherWeb.getWeatherData(fieldList, testLocation)
		)
	}

	@Test
	fun `WeatherWeb getWeatherData test during service unavailability`() {

		val expectedWeatherDataTable = mapOf(
			WeatherCharacteristics("Weather description") to emptyWeatherCharacteristics,
			WeatherCharacteristics("Feels like temperature") to emptyWeatherCharacteristics,
			WeatherCharacteristics("Weather icon") to emptyWeatherCharacteristics,
			WeatherCharacteristics("Humidity") to emptyWeatherCharacteristics,
			WeatherCharacteristics("Max temperature") to emptyWeatherCharacteristics,
			WeatherCharacteristics("Non-existent characteristic") to emptyWeatherCharacteristics,
			WeatherCharacteristics("Non-existent in json characteristic") to emptyWeatherCharacteristics
		)
		val expectedWeatherData = WeatherData(serviceName, expectedWeatherDataTable)

		val response = mockk<CloseableHttpResponse>(relaxUnitFun = true)
		every { response.entity } returns null

		val httpClient = mockk<CloseableHttpClient>(relaxUnitFun = true)
		every { httpClient.execute(any()) } returns response

		val testWeatherWeb = TestWeatherWeb(httpClient, appId, serviceName, dataJSONInformation)

		assertEquals(
			expectedWeatherData,
			testWeatherWeb.getWeatherData(fieldList, testLocation)
		)
	}
}