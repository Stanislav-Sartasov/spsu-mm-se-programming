package weatherDataFormatter

import weatherCharacteristics.WeatherCharacteristics
import weatherCharacteristicsData.WeatherCharacteristicsData
import weatherData.WeatherData
import weatherDataField.WeatherDataField
import kotlin.math.roundToInt

object WeatherDataFormatter {

	fun formatWeatherData(vararg columns: WeatherData): Map<WeatherCharacteristics, List<WeatherDataField>> {

		val result = mutableMapOf<WeatherCharacteristics, MutableList<WeatherDataField>>()

		for (i in columns.indices) {
			val column = columns[i]
			for ((characteristics, value) in column.table) {
				if (!result.containsKey(characteristics)) result[characteristics] =
					MutableList(columns.size) { index -> WeatherDataField(columns[index].serviceName, "NO DATA") }

				if (value.data != null) {
					result[characteristics]!![i] = WeatherDataField(column.serviceName,
						getFormattedWeatherCharacteristicsData(characteristics, value))
				}
			}
		}

		return result
	}

	private fun getFormattedWeatherCharacteristicsData(
		characteristics: WeatherCharacteristics,
		value: WeatherCharacteristicsData,
	): String {
		when (characteristics.name) {
			"Temperature" -> {
				return when (value.units) {
					"deg. C" -> "${value.data!!.toDouble().roundToInt()} ${value.units} / ${(value.data!!.toDouble() * 9 / 5 + 32).roundToInt()} deg. F"
					"deg. F" -> "${value.data!!.toDouble().roundToInt()} ${value.units} / ${((value.data!!.toDouble() - 32) * 5 / 9).roundToInt()} deg. C"
					else -> "${value.data!!.toDouble().roundToInt()} ${value.units}"
				}
			}
			"Precipitation type" -> {
				return when (value.data) {
					"0" -> "Clear"
					"1" -> "Rain"
					"2" -> "Snow"
					"3" -> "Freezing rain"
					"4" -> "Ice Pellets / Sleet"
					else -> "${value.data}".replaceFirstChar { it.uppercaseChar() }
				}
			}
			"Wind direction" -> {
				val deg = value.data!!.toDouble()
				return when {
					deg == 0.0 -> "N"
					deg < 90.0 -> "NE"
					deg == 90.0 -> "E"
					deg < 180.0 -> "SE"
					deg == 180.0 -> "S"
					deg < 270.0 -> "SW"
					deg == 270.0 -> "W"
					else -> "NW"
				}
			}
			else -> {
				return "${value.data} ${value.units}"
			}
		}
	}

	fun makePrintableWeatherData(
		formattedWeatherData: Map<WeatherCharacteristics, List<WeatherDataField>>,
	): List<String> {
		if (formattedWeatherData.isEmpty()) return listOf()

		val data = formattedWeatherData
		val printableWeatherData = MutableList(2 * data.size + 3) {
			"-".repeat((data[data.keys.first()]!!.size + 1) * (FIELD_WIDTH + 2 * GAP_WIDTH + 5) + 1)
		}

		printableWeatherData[1] = String.format("| %${GAP_WIDTH}s %${FIELD_WIDTH}s %${GAP_WIDTH}s |", "", "", "")
		for (i in data[data.keys.first()]!!.indices) {
			printableWeatherData[1] += String.format(" %${GAP_WIDTH}s %${-FIELD_WIDTH}s %${GAP_WIDTH}s |",
				"",
				data[data.keys.first()]!![i].serviceName,
				"")
		}

		var index = 3
		for (key in data.keys) {
			printableWeatherData[index] =
				String.format("| %${GAP_WIDTH}s %${FIELD_WIDTH}s %${GAP_WIDTH}s |", "", key.name, "")
			for (field in data[key]!!) {
				printableWeatherData[index] += String.format(" %${GAP_WIDTH}s %${-FIELD_WIDTH}s %${GAP_WIDTH}s |",
					"",
					field.value,
					"")
			}
			index += 2
		}

		return printableWeatherData
	}

	private const val FIELD_WIDTH = 25

	private const val GAP_WIDTH = 3
}