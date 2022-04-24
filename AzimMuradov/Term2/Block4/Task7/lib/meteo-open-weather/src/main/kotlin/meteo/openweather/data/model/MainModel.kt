package meteo.openweather.data.model

import kotlinx.serialization.Serializable

@Serializable
internal data class MainModel(val temp: Double?, val humidity: Int?)
