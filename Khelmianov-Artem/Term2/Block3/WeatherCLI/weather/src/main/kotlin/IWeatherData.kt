interface IWeatherData {
    fun toWeatherData(apiName: String): WeatherData
}