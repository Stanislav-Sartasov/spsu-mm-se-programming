package meteo.openweather.data.model

import kotlinx.serialization.Serializable

@Serializable
internal data class RainModel(val `1h`: Double?)
