package meteo.domain

import kotlinx.coroutines.flow.SharedFlow
import meteo.domain.entity.Location
import meteo.domain.entity.Weather

interface MeteoInteractor {

    val serviceName: String

    val weather: SharedFlow<Result<Weather>>


    suspend fun updateWeather(location: Location)
}
