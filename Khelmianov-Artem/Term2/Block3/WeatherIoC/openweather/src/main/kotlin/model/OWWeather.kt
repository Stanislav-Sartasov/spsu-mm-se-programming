package model

import kotlinx.serialization.Serializable

@Serializable
internal data class OWWeather(
    val description: String
)
