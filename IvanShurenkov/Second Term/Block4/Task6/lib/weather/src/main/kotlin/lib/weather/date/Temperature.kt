package lib.weather.date

import kotlin.math.roundToInt

class Temperature(_celsius: Double) {
    val celsius: Double
    val fahrenheit: Double
        get() {
            return (((celsius * 1.8) + 32) * 100).roundToInt() / 100.0
        }

    init {
        celsius = _celsius.coerceAtLeast(-273.15)
    }
}
