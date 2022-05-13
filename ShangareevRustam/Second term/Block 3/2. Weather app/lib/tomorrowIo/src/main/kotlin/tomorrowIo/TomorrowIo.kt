package tomorrowIo

import location.Location
import unassembledWeatherDataField.UnassembledWeatherDataField
import weatherCharacteristics.WeatherCharacteristics
import weatherWeb.*

class TomorrowIo(override val appId: String) : WeatherWeb {
	override val url = "tomorrow.io"

	override val dataJSONInformation: Map<WeatherCharacteristics, UnassembledWeatherDataField> =
		mapOf(WeatherCharacteristics("Precipitation type") to UnassembledWeatherDataField(units = "",
			path = listOf("data", "timelines", "intervals", "values", "precipitationType")),
			WeatherCharacteristics("Precipitation") to UnassembledWeatherDataField(units = "mmph",
				path = listOf("data", "timelines", "intervals", "values", "precipitationIntensity")),
			WeatherCharacteristics("Temperature") to UnassembledWeatherDataField(units = "deg. C",
				path = listOf("data", "timelines", "intervals", "values", "temperature")),
			WeatherCharacteristics("Cloud cover") to UnassembledWeatherDataField(units = "%",
				path = listOf("data", "timelines", "intervals", "values", "cloudCover")),
			WeatherCharacteristics("Humidity") to UnassembledWeatherDataField(units = "%",
				path = listOf("data", "timelines", "intervals", "values", "humidity")),
			WeatherCharacteristics("Wind direction") to UnassembledWeatherDataField(units = "°",
				path = listOf("data", "timelines", "intervals", "values", "windDirection")),
			WeatherCharacteristics("Wind speed") to UnassembledWeatherDataField(units = "mps",
				path = listOf("data", "timelines", "intervals", "values", "windSpeed")))

	override fun getRequestUrl(location: Location): String {
		return "https://api." + url + "/v4/timelines?&timesteps=current&" +
				"fields=temperature,cloudCover," + "humidity,precipitationType," +
				"precipitationIntensity,windDirection," + "windSpeed&" +
				"location=${location.lat},${location.lon}" + "&units=metric" +
				"&apikey=$appId"
	}
}