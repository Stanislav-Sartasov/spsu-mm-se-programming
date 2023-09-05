package model

import kotlinx.serialization.Serializable

@Serializable
internal data class WBWeather(
    val description: String
)
