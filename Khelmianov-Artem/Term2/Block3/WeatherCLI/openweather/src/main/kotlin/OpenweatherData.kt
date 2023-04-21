import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable
import model.*

@Serializable
internal class OpenweatherData(
    val coord: Coordinates,
    val weather: List<OWWeather>,
    val main: OWMain,
    val wind: OWWind,
    val clouds: OWClouds,
    val rain: OWRain? = null,
    val snow: OWSnow? = null,
    @SerialName("name") val cityName: String
) : IWeatherData {
    override fun toWeatherData(apiName: String): WeatherData =
        WeatherData(
            source = "OpenWeather",
            coordinates = coord,
            city = cityName,
            tempC = main.temp,
            clouds = clouds.all,
            description = weather[0].description,
            humidity = main.humidity,
            windDir = wind.deg,
            windSpeed = wind.speed,
            precipitation = (rain?.h1 ?: 0F) + (snow?.h1 ?: 0F)
        )
}
