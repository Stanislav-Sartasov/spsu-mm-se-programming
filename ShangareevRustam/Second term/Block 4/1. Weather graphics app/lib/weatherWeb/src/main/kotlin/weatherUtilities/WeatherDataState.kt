package weatherUtilities

import weatherUtilities.WeatherCharacteristics
import weatherUtilities.WeatherDataField

data class WeatherDataState(
	val time: String,
	val data: Map<WeatherCharacteristics, WeatherDataField>
)
