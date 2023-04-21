package model

import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Serializable
internal data class OWSnow(
    @SerialName("1h") val h1: Float,
)
