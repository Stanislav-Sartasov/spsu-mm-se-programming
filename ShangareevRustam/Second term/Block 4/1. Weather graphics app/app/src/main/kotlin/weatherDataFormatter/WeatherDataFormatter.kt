package weatherDataFormatter

import weatherUtilities.WeatherCharacteristics
import weatherUtilities.WeatherCharacteristicsData
import weatherUtilities.WeatherData
import weatherUtilities.WeatherDataField
import java.util.WeakHashMap
import kotlin.math.roundToInt

object WeatherDataFormatter {

	fun formatWeatherData(data: WeatherData): Map<WeatherCharacteristics, WeatherDataField> {
		val result = mutableMapOf<WeatherCharacteristics, WeatherDataField>()

		for ((characteristics, value) in data.table) {
			val formattedCharacteristics = formatField(characteristics)

			if (!result.containsKey(formattedCharacteristics))
				result[formattedCharacteristics] = WeatherDataField("NO DATA")

			if (value.data != null) {
				if (result[formattedCharacteristics]!!.str == "NO DATA") result[formattedCharacteristics] =
					WeatherDataField(
						getFormattedWeatherCharacteristicsData(characteristics, value)
					)
				else result[formattedCharacteristics] =
					WeatherDataField(result[formattedCharacteristics]!!.str +
							", " +
							WeatherDataField(getFormattedWeatherCharacteristicsData(characteristics, value)).str
					)
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
					" °C" -> "${
						value.data!!.toDouble().roundToInt()
					}${value.units} / ${(value.data!!.toDouble() * 9 / 5 + 32).roundToInt()} °F"
					" °F" -> "${
						value.data!!.toDouble().roundToInt()
					}${value.units} / ${((value.data!!.toDouble() - 32) * 5 / 9).roundToInt()} °C"
					else -> "${value.data!!.toDouble().roundToInt()}${value.units}"
				}
			}
			"Precipitation type" -> {
				val res = value.data!!
				if (res.length == 3) return res

				return if (res == "1001") "804"
				else if (res.first() == '1') "801"
				else if (res.first() == '2') "7xx"
				else if (res.first() in listOf('4', '6', '7')) "5xx"
				else if (res.first() == '5') "6xx"
				else if (res.first() == '8') "2xx"
				else "800"
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
				return "${value.data}${value.units}"
			}
		}
	}

	private fun formatField(characteristics: WeatherCharacteristics): WeatherCharacteristics {
		val formattedName = when (characteristics.name) {
			"Wind speed" -> "Wind"
			"Wind direction" -> "Wind"
			else -> characteristics.name
		}
		return WeatherCharacteristics(formattedName)
	}

}