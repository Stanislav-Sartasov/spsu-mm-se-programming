package meteo.domain.entity

@JvmInline
value class Temperature private constructor(val kelvin: Double) {

    val celsius: Double get() = kelvin.kToC()

    val fahrenheit: Double get() = kelvin.kToC().cToF()


    companion object {

        fun inKelvin(value: Double): Temperature = Temperature(value.coerceKelvin())

        fun inCelsius(value: Double): Temperature = Temperature(value.cToK().coerceKelvin())

        fun inFahrenheit(value: Double): Temperature = Temperature(value.fToC().cToK().coerceKelvin())


        private fun Double.fToC() = (this - 32) * 5 / 9

        private fun Double.cToF() = this * 9 / 5 + 32

        private fun Double.cToK() = this + 273.15

        private fun Double.kToC() = this - 273.15

        private fun Double.coerceKelvin() = coerceAtLeast(minimumValue = 0.0)
    }
}
