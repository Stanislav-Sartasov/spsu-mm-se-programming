package lib.weather.date

class Precipitation(_mmPerHour: Double) {
    val mmPerHour: Double

    init {
        mmPerHour = _mmPerHour.coerceAtLeast(0.0)
    }
}
