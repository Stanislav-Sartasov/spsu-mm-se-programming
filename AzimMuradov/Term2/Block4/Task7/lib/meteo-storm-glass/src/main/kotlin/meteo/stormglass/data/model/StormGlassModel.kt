package meteo.stormglass.data.model

import kotlinx.serialization.Serializable

@Serializable
internal data class StormGlassModel(val hours: List<StormGlassWeatherModel>)
