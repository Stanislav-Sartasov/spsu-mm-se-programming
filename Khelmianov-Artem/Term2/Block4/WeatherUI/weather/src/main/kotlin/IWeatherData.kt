package lib.weather
interface IWeatherData {
    fun toWeatherData(apiName: String): WeatherData
}