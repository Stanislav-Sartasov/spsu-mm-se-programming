import io.ktor.client.*
import io.ktor.client.call.*
import io.ktor.client.request.*
import kotlinx.serialization.json.decodeFromJsonElement
import kotlinx.serialization.json.jsonArray
import kotlinx.serialization.json.jsonObject
import lib.weather.AWeatherAPI
import lib.weather.Coordinates
import lib.weather.WeatherData

object WeatherbitAPI : AWeatherAPI("https://api.weatherbit.io/v2.0/current") {
    override val name = "WeatherBit"

    override suspend fun get(coord: Coordinates, client: HttpClient): WeatherData {
        val wbData = json.parseToJsonElement(
            client.get(api) {
                parameter("key", System.getenv("WEATHERBIT_API_KEY"))
                parameter("lat", coord.lat)
                parameter("lon", coord.lon)
                parameter("lang", "ru")
            }.body()
        )
        val data = wbData.jsonObject["data"]?.jsonArray?.get(0)
        return json.decodeFromJsonElement<WeatherbitData>(data!!).toWeatherData(name)
    }

}