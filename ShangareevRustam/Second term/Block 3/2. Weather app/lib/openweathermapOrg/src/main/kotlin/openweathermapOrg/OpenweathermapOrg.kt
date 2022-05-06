package openweathermapOrg

import location.Location
import unassembledWeatherDataField.UnassembledWeatherDataField
import weatherCharacteristics.WeatherCharacteristics
import weatherWeb.*

class OpenweathermapOrg(override val appId: String) : WeatherWeb {
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
		return "https://api." + url + "/data/2.5/weather?" + "lat=${location.lat}" +
				"&lon=${location.lon}" + "&units=metric" + "&mode=json" + "&appId=$appId"
	}
}