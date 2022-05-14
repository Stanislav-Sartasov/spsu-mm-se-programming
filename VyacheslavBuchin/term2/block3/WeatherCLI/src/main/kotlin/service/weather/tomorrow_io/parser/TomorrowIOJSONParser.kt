package service.weather.tomorrow_io.parser

import entity.*
import json.parser.JSONSimpleParser
import org.json.simple.JSONArray
import org.json.simple.JSONObject
import org.json.simple.parser.JSONParser
import org.json.simple.parser.ParseException
import report.WeatherReport

class TomorrowIOJSONParser : JSONSimpleParser<WeatherReport>() {
	override fun parse(rawJson: String): WeatherReport {
		try {
			var json = JSONParser().parse(rawJson) as JSONObject
			json = filterJSONObject(json)
			val temperature = getByParameterOrNull(listOf("temperature"), json) { Temperature.ofCelsius(it.toDouble()) }
			val cloudiness = getByParameterOrNull(listOf("cloudCover"), json) { Cloudiness(it.toDouble().toInt()) }
			val humidity = getByParameterOrNull(listOf("humidity"), json) { Humidity(it.toDouble().toInt()) }
			val precipitation =
				getByParameterOrNull(listOf("precipitationIntensity"), json) { Precipitation(it.toDouble()) }
			val windDirection =
				getByParameterOrNull(listOf("windDirection"), json) { WindDirection(it.toDouble().toInt()) }
			val windVelocity = getByParameterOrNull(listOf("windSpeed"), json) { Velocity(it.toDouble()) }
			return WeatherReport(
				temperature,
				cloudiness,
				humidity,
				precipitation,
				windDirection,
				windVelocity
			)
		} catch(e: Exception) {
			throw ParseException(ParseException.ERROR_UNEXPECTED_EXCEPTION, e.message)
		}
	}

	private fun filterJSONObject(jsonObject: JSONObject): JSONObject {
		var json = jsonObject
		json = json["data"] as JSONObject
		json = (json["timelines"] as JSONArray)[0] as JSONObject
		json = (json["intervals"] as JSONArray)[0] as JSONObject
		json = json["values"] as JSONObject
		return json
	}
}