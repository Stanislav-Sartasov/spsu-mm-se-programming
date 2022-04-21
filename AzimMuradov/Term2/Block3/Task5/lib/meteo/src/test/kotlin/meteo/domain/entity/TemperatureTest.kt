package meteo.domain.entity

import meteo.domain.entity.TestUtils.ABSOLUTE_TOLERANCE
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals

internal class TemperatureTest {

    // `Temperature?` is used to prohibit inlining in the bytecode

    @ParameterizedTest
    @MethodSource("temperatures")
    fun `get temperature in kelvin`(temperature: Temperature?) {
        assertEquals(
            expected = KELVIN_42,
            actual = temperature!!.kelvin,
            absoluteTolerance = ABSOLUTE_TOLERANCE
        )
    }

    @ParameterizedTest
    @MethodSource("temperatures")
    fun `get temperature in celsius`(temperature: Temperature?) {
        assertEquals(
            expected = CELSIUS_42,
            actual = temperature!!.celsius,
            absoluteTolerance = ABSOLUTE_TOLERANCE
        )
    }

    @ParameterizedTest
    @MethodSource("temperatures")
    fun `get temperature in fahrenheit`(temperature: Temperature?) {
        assertEquals(
            expected = FAHRENHEIT_42,
            actual = temperature!!.fahrenheit,
            absoluteTolerance = ABSOLUTE_TOLERANCE
        )
    }


    private companion object {

        const val KELVIN_42 = 42.0

        const val CELSIUS_42 = -231.15

        const val FAHRENHEIT_42 = -384.07

        @JvmStatic
        fun temperatures() = listOf(
            Temperature.inKelvin(value = KELVIN_42),
            Temperature.inCelsius(value = CELSIUS_42),
            Temperature.inFahrenheit(value = FAHRENHEIT_42),
        )
    }
}
