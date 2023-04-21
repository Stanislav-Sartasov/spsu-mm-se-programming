package lib.weather.date

class CloudCoverage(_percent: Double) {
    val percent: Double

    init {
        percent = _percent.coerceIn(0.0..100.0)
    }
}
