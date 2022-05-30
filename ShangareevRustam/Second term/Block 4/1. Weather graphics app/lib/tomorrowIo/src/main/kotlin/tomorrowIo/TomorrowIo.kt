package tomorrowIo

import org.apache.http.impl.client.CloseableHttpClient
import weatherUtilities.Location
import weatherUtilities.UnassembledWeatherDataField
import weatherUtilities.WeatherCharacteristics
import weatherWeb.WeatherWeb

class TomorrowIo(
	override val appId: String,
	override val httpClient: CloseableHttpClient,
) : WeatherWeb() {

	override val url = "tomorrow.io"

	override val dataJSONInformation: Map<WeatherCharacteristics, UnassembledWeatherDataField> =
		mapOf(WeatherCharacteristics("Precipitation type") to UnassembledWeatherDataField(units = "",
			path = listOf("data", "timelines", "intervals", "values", "weatherCode")),
			WeatherCharacteristics("Precipitation") to UnassembledWeatherDataField(units = " mm/h",
				path = listOf("data", "timelines", "intervals", "values", "precipitationIntensity")),
			WeatherCharacteristics("Temperature") to UnassembledWeatherDataField(units = " °C",
				path = listOf("data", "timelines", "intervals", "values", "temperature")),
			WeatherCharacteristics("Cloud cover") to UnassembledWeatherDataField(units = " %",
				path = listOf("data", "timelines", "intervals", "values", "cloudCover")),
			WeatherCharacteristics("Humidity") to UnassembledWeatherDataField(units = " %",
				path = listOf("data", "timelines", "intervals", "values", "humidity")),
			WeatherCharacteristics("Wind direction") to UnassembledWeatherDataField(units = " °",
				path = listOf("data", "timelines", "intervals", "values", "windDirection")),
			WeatherCharacteristics("Wind speed") to UnassembledWeatherDataField(units = " m/s",
				path = listOf("data", "timelines", "intervals", "values", "windSpeed")))

	override fun getRequestUrl(location: Location) =
		"https://api." + url + "/v4/timelines?&timesteps=current&" +
				"fields=temperature,cloudCover,humidity,weatherCode," +
				"precipitationIntensity,windDirection,windSpeed&" +
				"location=${location.lat},${location.lon}" + "&units=metric" +
				"&apikey=$appId"
}