import io.ktor.client.*

interface IWeatherAPI {
    val name: String
    suspend fun get(coord: Coordinates, client:HttpClient): WeatherData
}