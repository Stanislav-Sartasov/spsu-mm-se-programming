package lib.weather

import kotlinx.serialization.Serializable

@Serializable
data class Coordinates(
    val lat: Float,
    val lon: Float
) {
    init {
        require(lon in -180F..180F)
        require(lat in -90F..90F)
    }
}