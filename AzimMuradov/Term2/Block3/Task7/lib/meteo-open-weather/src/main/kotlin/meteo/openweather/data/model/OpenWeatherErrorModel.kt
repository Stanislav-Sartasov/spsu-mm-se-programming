package meteo.openweather.data.model

import kotlinx.serialization.Serializable

@Serializable
internal data class OpenWeatherErrorModel(val cod: Int, val message: String)
