import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable
import model.WBWeather

@Serializable
internal class WeatherbitData(
    val lat: Float,
    val lon: Float,
    @SerialName("city_name") val cityName: String,
    @SerialName("temp") val tempC: Float,
    val tempF: Float = tempC * 9 / 5 + 32,
    val clouds: Int,
    val weather: WBWeather,
    @SerialName("rh") val humidity: Float,
    @SerialName("wind_dir") val windDir: Int,
    @SerialName("wind_spd") val windSpeed: Float,
    @SerialName("precip") val precipitation: Float,
) : IWeatherData {
    override fun toWeatherData(apiName: String): WeatherData =
        WeatherData(
            source = apiName,
            coordinates = Coordinates(lat, lon),
            city = cityName,
            tempC = tempC,
            tempF = tempF,
            clouds = clouds,
            description = weather.description,
            humidity = humidity.toInt(),
            windDir = windDir,
            windSpeed = windSpeed,
            precipitation = precipitation,
        )
}