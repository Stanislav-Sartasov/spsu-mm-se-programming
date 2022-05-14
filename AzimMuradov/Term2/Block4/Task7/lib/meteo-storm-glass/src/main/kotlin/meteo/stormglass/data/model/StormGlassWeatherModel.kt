package meteo.stormglass.data.model

import kotlinx.serialization.Serializable

@Serializable
internal data class StormGlassWeatherModel(
    val airTemperature: Map<String, Double>,
    val cloudCover: Map<String, Double>,
    val humidity: Map<String, Double>,
    val precipitation: Map<String, Double>,
    val windDirection: Map<String, Double>,
    val windSpeed: Map<String, Double>,
)
