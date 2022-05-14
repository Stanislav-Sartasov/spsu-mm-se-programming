package json.parser

import org.json.simple.parser.ParseException
import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test
import report.WeatherReport

internal abstract class JSONParserTest {

	protected abstract val correctRawJson: String
	protected abstract val incorrectRawJson: String
	protected abstract val temperatureC: Double?
	protected abstract val cloudiness: Int?
	protected abstract val humidity: Int?
	protected abstract val precipitation: Double?
	protected abstract val windDirection: Int?
	protected abstract val windVelocity: Double?
	protected abstract val parser: JSONParser<WeatherReport>

	@Test
	fun `parse() should return same report as reference`() {
		val report = parser.parse(correctRawJson)

		assertEquals(temperatureC, report.temperature?.celsius)
		assertEquals(cloudiness, report.cloudiness?.percent)
		assertEquals(humidity, report.humidity?.percent)
		assertEquals(precipitation, report.precipitation?.mmPerHour)
		assertEquals(windDirection, report.windDirection?.degrees)
		assertEquals(windVelocity, report.windVelocity?.metersPerSecond)
	}

	@Test
	fun `parse() should throw a ParseException if given json do not match parsing format`() {
		assertThrows(ParseException::class.java) { parser.parse(incorrectRawJson) }
	}
}