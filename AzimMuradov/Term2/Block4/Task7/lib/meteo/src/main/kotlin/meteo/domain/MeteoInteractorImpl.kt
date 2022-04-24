package meteo.domain

import kotlinx.coroutines.flow.*
import meteo.data.MeteoRepository
import meteo.domain.entity.Location
import meteo.domain.entity.Weather

class MeteoInteractorImpl(
    override val serviceName: String,
    private val repository: MeteoRepository,
) : MeteoInteractor {

    private val _weather = MutableSharedFlow<Result<Weather>>()

    override val weather: SharedFlow<Result<Weather>> = _weather.asSharedFlow()


    override suspend fun updateWeather(location: Location) {
        _weather.emit(repository.getWeather(location))
    }
}
