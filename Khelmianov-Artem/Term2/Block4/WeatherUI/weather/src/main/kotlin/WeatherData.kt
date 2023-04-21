package lib.weather
import kotlinx.serialization.Serializable

@Serializable
data class WeatherData(
    val source: String? = null,
    val coordinates: Coordinates? = null,
    val city: String? = null,
    val tempC: Float? = null,
    val tempF: Float? = if (tempC != null) tempC * 9 / 5 + 32 else null,
    val clouds: Int? = null,
    val description: String? = null,
    val humidity: Int? = null,
    val windDir: Int? = null,
    val windSpeed: Float? = null,
    val precipitation: Float? = null,
){
    init {
        tempC?.coerceAtLeast(-273.15F)
        clouds?.coerceIn(0..100)
        humidity?.coerceIn(0..100)
        windDir?.coerceIn(0..360)
        windSpeed?.coerceAtLeast(0F)
        precipitation?.coerceIn(0F..100F)
    }
}


