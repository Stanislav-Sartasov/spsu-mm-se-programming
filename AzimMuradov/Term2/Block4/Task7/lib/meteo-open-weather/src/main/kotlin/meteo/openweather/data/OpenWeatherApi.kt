package meteo.openweather.data

import meteo.data.MeteoApi
import meteo.domain.entity.Location
import java.net.URI
import java.net.http.HttpRequest

object OpenWeatherApi : MeteoApi {

    override fun createGetWeatherRequest(location: Location, key: String): HttpRequest =
        HttpRequest.newBuilder().apply {
            val (lat, lon) = location
            val query = listOf(
                "lat=$lat", "lon=$lon",
                "appid=$key",
                "lang=ru"
            ).joinToString(separator = "&")

            uri(URI.create("$API_ENDPOINT?$query"))
            GET()
        }.build()


    private const val API_ENDPOINT = "https://api.openweathermap.org/data/2.5/weather"
}
