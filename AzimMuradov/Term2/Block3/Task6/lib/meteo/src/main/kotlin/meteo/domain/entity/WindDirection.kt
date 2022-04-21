package meteo.domain.entity

@JvmInline
value class WindDirection private constructor(val degrees: Int) {

    companion object {

        fun inDegrees(value: Int): WindDirection = WindDirection(value.coerceIn(range = 0..360))
    }
}
