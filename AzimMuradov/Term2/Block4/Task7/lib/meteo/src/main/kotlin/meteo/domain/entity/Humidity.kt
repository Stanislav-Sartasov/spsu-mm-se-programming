package meteo.domain.entity

@JvmInline
value class Humidity private constructor(val percent: Int) {

    companion object {

        fun inPercent(value: Int): Humidity = Humidity(value.coerceIn(range = 0..100))
    }
}
