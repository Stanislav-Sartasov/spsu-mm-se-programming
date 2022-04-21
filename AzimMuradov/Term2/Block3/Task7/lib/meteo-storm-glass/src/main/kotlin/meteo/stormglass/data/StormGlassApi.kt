package meteo.stormglass.data

import meteo.data.MeteoApi
import meteo.domain.entity.Location
import java.net.URI
import java.net.http.HttpRequest
import java.time.Instant
import java.time.temporal.ChronoUnit

object StormGlassApi : MeteoApi {

    override fun createGetWeatherRequest(location: Location, key: String): HttpRequest =
        HttpRequest.newBuilder().apply {
            val time = currentTimeInEpochSeconds()
            val (lat, lon) = location
            val query = listOf(
                "lat=$lat", "lng=$lon",
                "params=${params.joinToString(separator = ",")}",
                "start=$time", "end=$time",
                "source=$SG"
            ).joinToString(separator = "&")

            uri(URI.create("$API_ENDPOINT?$query"))
            GET()
            header("Authorization", key)
        }.build()


    private const val API_ENDPOINT = "https://api.stormglass.io/v2/weather/point"

    private val params = listOf(
        "airTemperature",
        "cloudCover",
        "humidity",
        "precipitation",
        "windDirection",
        "windSpeed"
    )

    internal const val SG = "sg"

    private fun currentTimeInEpochSeconds() = Instant.now().truncatedTo(ChronoUnit.HOURS).epochSecond
}
