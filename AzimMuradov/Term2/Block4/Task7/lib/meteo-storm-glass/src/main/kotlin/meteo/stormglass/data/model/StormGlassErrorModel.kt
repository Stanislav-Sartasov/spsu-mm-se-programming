package meteo.stormglass.data.model

import kotlinx.serialization.Serializable

@Serializable
internal data class StormGlassErrorModel(val errors: Map<String, String>)
