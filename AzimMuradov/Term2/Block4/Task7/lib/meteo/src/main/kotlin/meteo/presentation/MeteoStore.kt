package meteo.presentation

import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.flow.*
import kotlinx.coroutines.launch
import meteo.domain.MeteoInteractor
import meteo.domain.entity.Location
import meteo.presentation.mvi.MviStore
import meteo.presentation.state.*
import meteo.presentation.wish.MeteoWish
import meteo.toLoadingState

class MeteoStore(
    private val interactors: List<MeteoInteractor>,
    private val location: Location,
    private val scope: CoroutineScope,
) : MviStore<MeteoWish, MeteoState> {

    private val _state = MutableStateFlow<MeteoState>(MeteoState.Uninitialised)

    override val state: StateFlow<MeteoState> = _state.asStateFlow()

    init {
        for ((index, interactor) in interactors.withIndex()) {
            scope.launch {
                interactor.weather.collect { result ->
                    _state.update { state ->
                        val weatherListWithoutUpdate = when (state) {
                            MeteoState.Uninitialised -> loadingMeteoState.weatherList
                            is MeteoState.Initialised -> state.weatherList
                        }
                        MeteoState.Initialised(
                            weatherList = weatherListWithoutUpdate.toMutableList().apply {
                                this[index] = NamedValue(
                                    name = interactor.serviceName,
                                    value = result.toLoadingState()
                                )
                            },
                        )
                    }
                }
            }
        }
    }


    // Handle wishes

    override fun consume(wish: MeteoWish) {
        scope.launch {
            when (wish) {
                MeteoWish.Load -> loadWeather()
            }
        }
    }

    private suspend fun loadWeather() {
        _state.update { loadingMeteoState }
        interactors.forEach { it.updateWeather(location) }
    }


    private val loadingMeteoState = MeteoState.Initialised(
        weatherList = interactors.map {
            NamedValue(name = it.serviceName, value = LoadingState.Loading)
        },
    )
}
