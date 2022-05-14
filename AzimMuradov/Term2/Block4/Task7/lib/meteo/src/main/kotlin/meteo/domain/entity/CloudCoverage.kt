package meteo.domain.entity

@JvmInline
value class CloudCoverage private constructor(val percent: Int) {

    companion object {

        fun inPercent(value: Int): CloudCoverage = CloudCoverage(value.coerceIn(range = 0..100))
    }
}
