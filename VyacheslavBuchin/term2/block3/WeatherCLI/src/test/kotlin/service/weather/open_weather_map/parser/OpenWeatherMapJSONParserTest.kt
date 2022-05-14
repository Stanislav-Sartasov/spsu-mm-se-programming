package service.weather.open_weather_map.parser

import json.parser.JSONParserTest
import java.io.File

internal class OpenWeatherMapJSONParserTest : JSONParserTest() {
	override val correctRawJson =
		File("src/test/resources/open_weather_map.json").bufferedReader().readText()
	override val incorrectRawJson =
		File("src/test/resources/incorrect_open_weather_map.json").bufferedReader().readText()
	override val temperatureC = 3.94
	override val cloudiness = 40
	override val humidity = 62
	override val precipitation = null
	override val windDirection = 240
	override val windVelocity = 4.0
	override val parser = OpenWeatherMapJSONParser()
}