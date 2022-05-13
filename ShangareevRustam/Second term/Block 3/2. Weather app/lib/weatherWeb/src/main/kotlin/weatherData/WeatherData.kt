package weatherData

import weatherCharacteristics.WeatherCharacteristics
import weatherCharacteristicsData.WeatherCharacteristicsData

data class WeatherData(val serviceName: String, val table: Map<WeatherCharacteristics, WeatherCharacteristicsData>)