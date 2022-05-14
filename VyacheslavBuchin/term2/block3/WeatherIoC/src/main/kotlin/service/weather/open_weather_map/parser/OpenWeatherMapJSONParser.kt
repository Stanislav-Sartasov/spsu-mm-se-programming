package service.weather.open_weather_map.parser

import entity.*
import json.parser.JSONSimpleParser
import report.WeatherReport
import org.json.simple.JSONObject
import org.json.simple.parser.JSONParser
import org.json.simple.parser.ParseException

open class OpenWeatherMapJSONParser : JSONSimpleParser<WeatherReport>() {
	override fun parse(rawJson: String): WeatherReport {
		try {
			val json = JSONParser().parse(rawJson) as JSONObject
			val temperature = getByParameterOrNull(listOf("main", "temp"), json) { Temperature.ofCelsius(it.toDouble()) }
			val cloudiness = getByParameterOrNull(listOf("clouds", "all"), json) { Cloudiness(it.toInt()) }
			val humidity = getByParameterOrNull(listOf("main", "humidity"), json) { Humidity(it.toInt()) }
			val precipitation = getByParameterOrNull(listOf("precipitation"), json) { Precipitation(it.toDouble()) }
			val windDirection = getByParameterOrNull(listOf("wind", "deg"), json) { WindDirection(it.toInt()) }
			val windVelocity = getByParameterOrNull(listOf("wind", "speed"), json) { Velocity(it.toDouble()) }
			return WeatherReport(
				temperature,
				cloudiness,
				humidity,
				precipitation,
				windDirection,
				windVelocity
			)
		} catch (e: Exception) {
			throw ParseException(ParseException.ERROR_UNEXPECTED_EXCEPTION, e.message)
		}
	}
}