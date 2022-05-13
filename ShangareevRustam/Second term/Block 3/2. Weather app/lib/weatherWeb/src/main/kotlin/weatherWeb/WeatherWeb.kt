package weatherWeb

import location.*
import org.apache.http.client.methods.CloseableHttpResponse
import org.apache.http.client.methods.HttpGet
import org.apache.http.impl.client.CloseableHttpClient
import org.apache.http.impl.client.HttpClients
import org.apache.http.util.EntityUtils
import org.json.JSONArray
import org.json.JSONObject
import unassembledWeatherDataField.*
import weatherCharacteristics.*
import weatherCharacteristicsData.*
import weatherData.*

interface WeatherWeb {
	val appId: String

	val url: String

	val dataJSONInformation: Map<WeatherCharacteristics, UnassembledWeatherDataField>

	fun getRequestUrl(location: Location): String

	private fun getUrlContent(url: String): JSONObject? {
		val httpClient: CloseableHttpClient
		val response: CloseableHttpResponse
		try {
			httpClient = HttpClients.createDefault()
			response = httpClient.execute(HttpGet(url))

			if (response.entity == null) throw Exception()
			val rawContent = EntityUtils.toString(response.entity, "UTF_16")

			response.close()
			httpClient.close()

			return JSONObject(rawContent)
		} catch (e: Throwable) {
			return null
		}
	}

	private fun getWeatherCharacteristicsData(
		weatherCharacteristics: WeatherCharacteristics,
		jsonObject: JSONObject?,
	): WeatherCharacteristicsData {

		val empty = WeatherCharacteristicsData(null, "")
		if (!dataJSONInformation.containsKey(weatherCharacteristics) || jsonObject == null) {
			return empty
		}

		val element = dataJSONInformation[weatherCharacteristics]!!
		val path = element.path.dropLast(1)
		var content = jsonObject

		for (key in path) {
			val temp = try {
				content!![key]
			} catch (e: Throwable) {
				return empty
			}
			content = if (temp is JSONArray) {
				temp.first() as JSONObject
			} else {
				temp as JSONObject
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
