package lib.weather.date

class WindSpeed(_speed: Double) {
    val speed: Double

    init {
        speed = _speed.coerceAtLeast(0.0)
    }
}