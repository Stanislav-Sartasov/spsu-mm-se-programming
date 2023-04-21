import io.mockk.every
import io.mockk.mockk
import weatherUtilities.Location
import openweathermapOrg.OpenweathermapOrg
import org.apache.http.HttpEntity
import org.apache.http.client.methods.CloseableHttpResponse
import org.apache.http.impl.client.CloseableHttpClient
import org.apache.http.message.BasicHeader
import org.junit.jupiter.api.Assertions
import org.junit.jupiter.api.Test
import weatherUtilities.WeatherCharacteristics
import weatherUtilities.WeatherCharacteristicsData
import weatherUtilities.WeatherData
import java.io.ByteArrayInputStream

internal class OpenweathermapOrgTests {

	@Test
	fun `WeatherWeb getWeatherData test on openweathermapOrg`() {

		val expectedWeatherDataTable = mapOf(
			WeatherCharacteristics("Temperature") to WeatherCharacteristicsData(
				units = " °C", data = "282.88"
			),
			WeatherCharacteristics("Cloud cover") to WeatherCharacteristicsData(
				units = " %", data = "75"
			),
			WeatherCharacteristics("Humidity") to WeatherCharacteristicsData(
				units = " %", data = "90"
			),
			WeatherCharacteristics("Precipitation type") to WeatherCharacteristicsData(
				units = "", data = "803"
			),
			WeatherCharacteristics("Wind direction") to WeatherCharacteristicsData(
				units = " °", data = "150"
			),
			WeatherCharacteristics("Wind speed") to WeatherCharacteristicsData(
				units = " m/s", data = "2"
			),
			WeatherCharacteristics("Precipitation") to WeatherCharacteristicsData(
				units = "", data = null
			),
		)
		val expectedJSONObjectAsByteArray = testDataFromOpenweathermapOrg.toByteArray()
		val expectedWeatherData = WeatherData(serviceName, expectedWeatherDataTable)

		val entity = mockk<HttpEntity>(relaxUnitFun = true)
		every { entity.contentType } returns BasicHeader("Content-type", "application/json")
		every { entity.content } returns ByteArrayInputStream(expectedJSONObjectAsByteArray)
		every { entity.contentLength } returns expectedJSONObjectAsByteArray.size.toLong()

		val response = mockk<CloseableHttpResponse>(relaxUnitFun = true)
		every { response.entity } returns entity

		val httpClient = mockk<CloseableHttpClient>(relaxUnitFun = true)
		every { httpClient.execute(any()) } returns response

		val testWeatherWeb = OpenweathermapOrg("", httpClient)

		Assertions.assertEquals(
			expectedWeatherData,
			testWeatherWeb.getWeatherData(fieldList, testLocation)
		)
	}

	private companion object {

		private const val serviceName = "openweathermap.org"

		private const val testDataFromOpenweathermapOrg =
			"{\"coord\":{\"lon\":30.3351,\"lat\":59.9343},\"weather\":[{\"id\":803,\"main\":\"Clouds\"," +
					"\"description\":\"broken clouds\",\"icon\":\"04n\"}],\"base\":\"stations\"," +
					"\"main\":{\"temp\":282.88,\"feels_like\":282.03,\"temp_min\":282.59,\"temp_max\":284.21," +
					"\"pressure\":994,\"humidity\":90},\"visibility\":9000,\"wind\":{\"speed\":2,\"deg\":150}," +
					"\"clouds\":{\"all\":75},\"dt\":1653603261,\"sys\":{\"type\":2,\"id\":197864,\"country\":\"RU\"," +
					"\"sunrise\":1653613025,\"sunset\":1653677690},\"timezone\":10800,\"id\":519690," +
					"\"name\":\"Novaya Gollandiya\",\"cod\":200}"

		private val fieldList = listOf(
			WeatherCharacteristics("Temperature"),
			WeatherCharacteristics("Cloud cover"),
			WeatherCharacteristics("Humidity"),
			WeatherCharacteristics("Precipitation type"),
			WeatherCharacteristics("Wind direction"),
			WeatherCharacteristics("Wind speed"),
			WeatherCharacteristics("Precipitation")
		)

		private val testLocation = Location(17.4328, 10.32431)
	}
}