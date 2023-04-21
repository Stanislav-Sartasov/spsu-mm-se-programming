package meteo.domain.entity

@JvmInline
value class Precipitation private constructor(val mmPerHour: Double) {

    companion object {

        fun inMmPerHour(value: Double): Precipitation = Precipitation(value.coerceAtLeast(minimumValue = 0.0))
    }
}
