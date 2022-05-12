import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Test
import weatherUtilities.WeatherCharacteristics
import weatherUtilities.WeatherCharacteristicsData
import weatherUtilities.WeatherData
import weatherUtilities.WeatherDataField
import weatherDataFormatter.WeatherDataFormatter
import kotlin.random.Random

internal class WeatherDataFormatterTests {

	private fun getRandomString(): String {
		val size = Random.nextInt(10)
		var result = ""
		repeat(size) {
			result += Random.nextInt(64, 128).toChar()
		}
		return result
	}

	private fun getRandomWeatherCharacteristics(): WeatherCharacteristics {
		return WeatherCharacteristics(getRandomString())
	}

	private fun getRandomWeatherCharacteristicsData(): WeatherCharacteristicsData {
		return when (Random.nextInt(0, 2)) {
			0 -> WeatherCharacteristicsData(null, getRandomString())
			else -> WeatherCharacteristicsData(getRandomString(), getRandomString())
		}
	}

	private fun getRandomWeatherData(): WeatherData {
		val map = mutableMapOf<WeatherCharacteristics, WeatherCharacteristicsData>()
		repeat(Random.nextInt(10)) {
			map[getRandomWeatherCharacteristics()] = getRandomWeatherCharacteristicsData()
		}

		return WeatherData(getRandomString(), map)
	}

	@Test
	fun formatWeatherDataTest() {
		val columns = Array(Random.nextInt(5)) { getRandomWeatherData() }

		val expected = mutableMapOf<WeatherCharacteristics, MutableList<WeatherDataField>>()
		for (column in columns) {
			for ((fieldName, _) in column.table) {
				expected[fieldName] = mutableListOf()
			}
		}

		for ((field, _) in expected) {
			for ((serviceName, table) in columns) {
				if (table.containsKey(field) && table[field]!!.data != null) {
					expected[field]!!.add(
						WeatherDataField(serviceName, "${table[field]!!.data} ${table[field]!!.units}")
					)
				}
				else {
					expected[field]!!.add(
						WeatherDataField(serviceName, "NO DATA")
					)
				}
			}
		}

		assertEquals(
			WeatherDataFormatter.formatWeatherData(*columns),
			expected
		)
	}

	@Test
	fun makePrintableWeatherDataTest() {
		val formattedWeatherData =
			WeatherDataFormatter.formatWeatherData(*Array(Random.nextInt(1, 5)) { getRandomWeatherData() })

		val preExpected = MutableList(1) {String.format("| %3s %-25s %3s |", "", "", "")}
		for ((serviceName, _) in formattedWeatherData[formattedWeatherData.keys.first()]!!) {
			preExpected[0] += String.format(" %3s %-25s %3s |", "", serviceName, "")
		}

		for ((leftField, rightFields) in formattedWeatherData) {
			var str = String.format("| %3s %25s %3s |", "", leftField.name, "")
			for ((_, value) in rightFields) {
				str += String.format(" %3s %-25s %3s |", "", value, "")
			}
			preExpected.add(str)
		}

		val len = preExpected.first().length
		val expected = List(2 * preExpected.size + 1) {index ->
			if (index % 2 == 0) "-".repeat(len)
			else preExpected[(index - 1) / 2]
		}

		assertEquals(
			expected,
			WeatherDataFormatter.makePrintableWeatherData(formattedWeatherData)
		)
	}


}