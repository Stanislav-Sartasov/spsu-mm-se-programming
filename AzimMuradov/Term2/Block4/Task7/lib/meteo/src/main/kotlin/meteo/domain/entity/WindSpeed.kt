package meteo.domain.entity

@JvmInline
value class WindSpeed private constructor(val metersPerSecond: Double) {

    companion object {

        fun inMetersPerSecond(value: Double): WindSpeed = WindSpeed(value.coerceAtLeast(minimumValue = 0.0))
    }
}
