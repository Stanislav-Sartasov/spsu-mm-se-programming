import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Test
import weatherUtilities.WeatherCharacteristics
import weatherUtilities.WeatherCharacteristicsData
import weatherUtilities.WeatherData
import weatherUtilities.WeatherDataField
import weatherDataFormatter.WeatherDataFormatter

internal class WeatherDataFormatterTests {

	@Test
	fun formatWeatherDataTest() {

		val weatherDataTable = mapOf(
			WeatherCharacteristics("Weather description") to
					WeatherCharacteristicsData("light intensity shower rain", ""),
			WeatherCharacteristics("Feels like temperature") to
					WeatherCharacteristicsData("9.95", " deg. C"),
			WeatherCharacteristics("Weather icon") to
					WeatherCharacteristicsData("09d", ".png"),
			WeatherCharacteristics("Humidity") to
					WeatherCharacteristicsData("85", " percent"),
			WeatherCharacteristics("Wind speed") to
					WeatherCharacteristicsData("15", " m/s"),
			WeatherCharacteristics("Wind direction") to
					WeatherCharacteristicsData("150", " deg."),
			WeatherCharacteristics("Max temperature") to
					WeatherCharacteristicsData("12.1", " deg. F"),
			WeatherCharacteristics("Non-existent in json characteristic") to
					WeatherCharacteristicsData(null, ""),
			WeatherCharacteristics("Non-existent characteristic") to
					WeatherCharacteristicsData(null, ""),
			WeatherCharacteristics("Precipitation type") to
					WeatherCharacteristicsData("3438", "")
		)
		val weatherData = WeatherData("site", weatherDataTable)

		val expectedFormattedWeatherData = mapOf(
			WeatherCharacteristics("Weather description") to
					WeatherDataField("light intensity shower rain"),
			WeatherCharacteristics("Feels like temperature") to
					WeatherDataField("9.95 deg. C"),
			WeatherCharacteristics("Weather icon") to
					WeatherDataField("09d.png"),
			WeatherCharacteristics("Humidity") to
					WeatherDataField("85 percent"),
			WeatherCharacteristics("Max temperature") to
					WeatherDataField("12.1 deg. F"),
			WeatherCharacteristics("Non-existent in json characteristic") to
					WeatherDataField("NO DATA"),
			WeatherCharacteristics("Non-existent characteristic") to
					WeatherDataField("NO DATA"),
			WeatherCharacteristics("Wind") to
					WeatherDataField("15 m/s, SE"),
			WeatherCharacteristics("Precipitation type") to
					WeatherDataField("800")
		)

		assertEquals(
			expectedFormattedWeatherData,
			WeatherDataFormatter.formatWeatherData(weatherData)
		)
	}

}