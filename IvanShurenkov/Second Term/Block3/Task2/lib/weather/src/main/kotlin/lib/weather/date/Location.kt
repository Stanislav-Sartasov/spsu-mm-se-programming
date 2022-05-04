package lib.weather.date

class Location (_lat: Double, _lon: Double) { // Широта(-90..90) и долгота(-180..180)
    val lat: Double
    val lon: Double

    init {
        lat =_lat.coerceIn(-90.0..90.0)
        lon = _lon.coerceIn(-180.0..180.0)
    }

    override fun toString(): String {
        return "$lat,$lon"
    }
}