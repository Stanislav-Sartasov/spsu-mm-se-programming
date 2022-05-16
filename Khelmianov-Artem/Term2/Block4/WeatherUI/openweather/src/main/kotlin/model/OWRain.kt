package model

import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Serializable
internal data class OWRain(
    @SerialName("1h") val h1: Float,
)
