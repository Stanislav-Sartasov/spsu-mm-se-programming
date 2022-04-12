package meteo.data

import meteo.domain.entity.Location
import java.net.http.HttpRequest

interface MeteoApi {

    fun createGetWeatherRequest(location: Location, key: String): HttpRequest
}
