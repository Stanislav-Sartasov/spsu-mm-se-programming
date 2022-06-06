package lib.weather.date

class WindDirection(_degree: Double) {
    val degree: Double

    init {
        degree = _degree.coerceIn(0.0..360.0)
    }
}