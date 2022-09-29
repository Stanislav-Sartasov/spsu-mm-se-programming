package model

import kotlinx.serialization.Serializable

@Serializable
internal data class OWMain(
    val temp: Float,
    val feels_like: Float,
    val temp_min: Float,
    val temp_max: Float,
    val pressure: Int,
    val humidity: Int,
)
