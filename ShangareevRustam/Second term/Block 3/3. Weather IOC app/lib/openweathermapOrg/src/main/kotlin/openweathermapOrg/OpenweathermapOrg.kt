package openweathermapOrg

import org.apache.http.impl.client.CloseableHttpClient
import org.json.JSONObject
import weatherUtilities.Location
import weatherUtilities.UnassembledWeatherDataField
import weatherUtilities.WeatherCharacteristics
import weatherWeb.WeatherWeb

class OpenweathermapOrg(
	override val appId: String,
	override val httpClient: CloseableHttpClient
) : WeatherWeb() {
	override val url = "openweathermap.org"

	override val dataJSONInformation: Map<WeatherCharacteristics, UnassembledWeatherDataField> =
		mapOf(WeatherCharacteristics("Temperature") to UnassembledWeatherDataField(units = "deg. C",
			path = listOf("main", "temp")),
			WeatherCharacteristics("Cloud cover") to UnassembledWeatherDataField(units = "%",
				path = listOf("clouds", "all")),
			WeatherCharacteristics("Humidity") to UnassembledWeatherDataField(units = "%",
				path = listOf("main", "humidity")),
			WeatherCharacteristics("Wind direction") to UnassembledWeatherDataField(units = "°",
				path = listOf("wind", "deg")),
			WeatherCharacteristics("Wind speed") to UnassembledWeatherDataField(units = "mps",
				path = listOf("wind", "speed")),
			WeatherCharacteristics("Precipitation type") to UnassembledWeatherDataField(units = "",
				path = listOf("weather", "description")))

	override fun getRequestUrl(location: Location): String {
		return "http://api." + url + "/data/2.5/weather?" + "lat=${location.lat}" +
				"&lon=${location.lon}" + "&units=metric" + "&mode=json" + "&appId=$appId"
	}
}