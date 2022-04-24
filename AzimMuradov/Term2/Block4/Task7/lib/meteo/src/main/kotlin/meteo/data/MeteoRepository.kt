package meteo.data

import meteo.domain.entity.Location
import meteo.domain.entity.Weather

interface MeteoRepository {

    suspend fun getWeather(location: Location): Result<Weather>
}
