package meteo.presentation.state

import meteo.domain.entity.Weather

sealed interface MeteoState {

    object Uninitialised : MeteoState

    data class Initialised(
        val weatherList: List<NamedValue<LoadingState<Weather>>>,
    ) : MeteoState
}
