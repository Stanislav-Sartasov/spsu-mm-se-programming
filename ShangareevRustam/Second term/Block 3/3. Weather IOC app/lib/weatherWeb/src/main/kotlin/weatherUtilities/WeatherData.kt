package weatherUtilities

data class WeatherData(val serviceName: String, val table: Map<WeatherCharacteristics, WeatherCharacteristicsData>)