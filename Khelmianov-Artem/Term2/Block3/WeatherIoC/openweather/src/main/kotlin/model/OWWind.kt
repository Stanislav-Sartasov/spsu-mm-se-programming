package model

import kotlinx.serialization.Serializable

@Serializable
internal data class OWWind(
    val speed: Float,
    val deg: Int
)
