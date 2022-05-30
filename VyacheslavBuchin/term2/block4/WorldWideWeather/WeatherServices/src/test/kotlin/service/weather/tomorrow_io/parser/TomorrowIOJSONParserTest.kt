package service.weather.tomorrow_io.parser

import json.parser.JSONParserTest
import java.io.File

internal class TomorrowIOJSONParserTest : JSONParserTest() {
	override val correctRawJson
		= File("src/test/resources/tomorrow.json").bufferedReader().readText()
	override val incorrectRawJson
		= File("src/test/resources/incorrect_tomorrow.json").bufferedReader().readText()
	override val temperatureC = 8.31
	override val cloudiness = 33
	override val humidity = 56
	override val precipitation = 0.0
	override val windDirection = 286
	override val windVelocity = 3.69
	override val parser = TomorrowIOJSONParser()
}