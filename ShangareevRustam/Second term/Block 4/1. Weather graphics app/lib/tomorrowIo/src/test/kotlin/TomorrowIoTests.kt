import io.mockk.every
import io.mockk.mockk
import org.apache.http.HttpEntity
import org.apache.http.client.methods.CloseableHttpResponse
import org.apache.http.impl.client.CloseableHttpClient
import org.apache.http.impl.client.HttpClients
import org.apache.http.message.BasicHeader
import org.junit.jupiter.api.Assertions
import org.junit.jupiter.api.Test
import tomorrowIo.TomorrowIo
import weatherUtilities.Location
import weatherUtilities.WeatherCharacteristics
import weatherUtilities.WeatherCharacteristicsData
import weatherUtilities.WeatherData
import java.io.ByteArrayInputStream
import kotlin.random.Random

internal class TomorrowIoTests {

	@Test
	fun `WeatherWeb getWeatherData test on tomorrowIo`() {

		val expectedWeatherDataTable = mapOf(
			WeatherCharacteristics("Temperature") to WeatherCharacteristicsData(
				units = " °C", data = "8.69"
			),
			WeatherCharacteristics("Cloud cover") to WeatherCharacteristicsData(
				units = " %", data = "100"
			),
			WeatherCharacteristics("Humidity") to WeatherCharacteristicsData(
				units = " %", data = "84"
			),
			WeatherCharacteristics("Precipitation type") to WeatherCharacteristicsData(
				units = "", data = "438"
			),
			WeatherCharacteristics("Wind direction") to WeatherCharacteristicsData(
				units = " °", data = "43.88"
			),
			WeatherCharacteristics("Wind speed") to WeatherCharacteristicsData(
				units = " m/s", data = "1.5"
			),
			WeatherCharacteristics("Precipitation") to WeatherCharacteristicsData(
				units = " mm/h", data = "0"
			),
		)
		val expectedJSONObjectAsByteArray = testDataFromTomorrowIo.toByteArray()
		val expectedWeatherData = WeatherData(serviceName, expectedWeatherDataTable)

		val entity = mockk<HttpEntity>(relaxUnitFun = true)
		every { entity.contentType } returns BasicHeader("Content-type", "application/json")
		every { entity.content } returns ByteArrayInputStream(expectedJSONObjectAsByteArray)
		every { entity.contentLength } returns expectedJSONObjectAsByteArray.size.toLong()

		val response = mockk<CloseableHttpResponse>(relaxUnitFun = true)
		every { response.entity } returns entity

		val httpClient = mockk<CloseableHttpClient>(relaxUnitFun = true)
		every { httpClient.execute(any()) } returns response

		val testWeatherWeb = TomorrowIo("", httpClient)

		Assertions.assertEquals(
			expectedWeatherData,
			testWeatherWeb.getWeatherData(fieldList, testLocation)
		)
	}

	private companion object {

		private const val serviceName = "tomorrow.io"

		private const val testDataFromTomorrowIo =
			"{\"data\":{\"timelines\":[{\"timestep\":\"current\",\"endTime\":\"2022-05-26T21:59:00Z\"," +
					"\"startTime\":\"2022-05-26T21:59:00Z\",\"intervals\":[{\"startTime\":\"2022-05-26T21:59:00Z\"," +
					"\"values\":{\"cloudCover\":100,\"humidity\":84,\"precipitationIntensity\":0," +
					"\"precipitationType\":1,\"temperature\":8.69,\"weatherCode\":438," +
					"\"windDirection\":43.88,\"windSpeed\":1.5}}]}]}}"

		private val fieldList = listOf(
			WeatherCharacteristics("Temperature"),
			WeatherCharacteristics("Cloud cover"),
			WeatherCharacteristics("Humidity"),
			WeatherCharacteristics("Precipitation type"),
			WeatherCharacteristics("Wind direction"),
			WeatherCharacteristics("Wind speed"),
			WeatherCharacteristics("Precipitation")
		)

		private val testLocation = Location(18.5832, 15.321)
	}
}