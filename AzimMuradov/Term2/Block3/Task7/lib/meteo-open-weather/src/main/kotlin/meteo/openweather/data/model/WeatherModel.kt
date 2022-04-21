package meteo.openweather.data.model

import kotlinx.serialization.Serializable

@Serializable
internal data class WeatherModel(val description: String?)
