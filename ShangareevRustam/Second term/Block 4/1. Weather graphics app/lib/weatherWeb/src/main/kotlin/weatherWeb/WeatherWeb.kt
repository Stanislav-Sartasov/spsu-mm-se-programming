package weatherWeb

import org.apache.http.client.methods.CloseableHttpResponse
import org.apache.http.client.methods.HttpGet
import org.apache.http.impl.client.CloseableHttpClient
import org.apache.http.util.EntityUtils
import org.json.JSONArray
import org.json.JSONObject
import weatherUtilities.*

abstract class WeatherWeb {

	abstract val appId: String

	abstract val url: String

	abstract val httpClient: CloseableHttpClient

	abstract val dataJSONInformation: Map<WeatherCharacteristics, UnassembledWeatherDataField>

	abstract fun getRequestUrl(location: Location): String

	private fun getUrlContent(url: String): JSONObject? {
		try {
			val response = httpClient.execute(HttpGet(url))
			val rawContent = EntityUtils.toString(response.entity, "UTF-8")

			response.close()

			return JSONObject(rawContent)
		} catch (e: Throwable) {
			return null
		}
	}

	private fun getWeatherCharacteristicsData(
		weatherCharacteristics: WeatherCharacteristics,
		jsonObject: JSONObject?
	): WeatherCharacteristicsData {

		val empty = WeatherCharacteristicsData(null, "")
		if (!dataJSONInformation.containsKey(weatherCharacteristics) || jsonObject == null) {
			return empty
		}

		val element = dataJSONInformation[weatherCharacteristics]!!
		val path = element.path.dropLast(1)
		var content = jsonObject

		for (key in path) {
			try {
				val temp = content!![key]
				content = if (temp is JSONArray) {
					temp.first() as JSONObject
				} else {
					temp as JSONObject
				}
			}
			catch (e: Throwable) {
				return empty
			}
		}

		return try {
			WeatherCharacteristicsData(content!![element.path.last()].toString(), element.units)
		} catch (e: Exception) {
			empty
		}
	}

	fun getWeatherData(characteristics: List<WeatherCharacteristics>, location: Location): WeatherData {
		val table = mutableMapOf<WeatherCharacteristics, WeatherCharacteristicsData>()
		val jsonObject = getUrlContent(getRequestUrl(location))

		for (field in characteristics) {
			table[field] = getWeatherCharacteristicsData(field, jsonObject)
		}

		return WeatherData(url, table)
	}
}
