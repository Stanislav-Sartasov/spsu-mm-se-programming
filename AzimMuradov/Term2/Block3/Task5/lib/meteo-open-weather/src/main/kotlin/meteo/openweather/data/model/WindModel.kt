package meteo.openweather.data.model

import kotlinx.serialization.Serializable

@Serializable
internal data class WindModel(val speed: Double?, val deg: Int?)
