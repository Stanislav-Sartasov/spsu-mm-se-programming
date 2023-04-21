import io.ktor.client.*
import io.ktor.client.call.*
import io.ktor.client.request.*
import lib.weather.AWeatherAPI
import lib.weather.Coordinates
import lib.weather.WeatherData


object OpenWeatherAPI : AWeatherAPI("https://api.openweathermap.org/data/2.5/weather") {
    override val name: String = "OpenWeather"

    override suspend fun get(coord: Coordinates, client: HttpClient): WeatherData {
        val data: OpenweatherData = client.get(api) {
            parameter("appid", System.getenv("OPENWEATHER_API_KEY"))
            parameter("lat", coord.lat)
            parameter("lon", coord.lon)
            parameter("lang", "ru")
            parameter("units", "metric")
        }.body()
        return data.toWeatherData(name)
    }
}
