package meteo.domain.entity

import meteo.domain.entity.TestUtils.ABSOLUTE_TOLERANCE
import meteo.domain.entity.TestUtils.DOUBLE_42
import meteo.domain.entity.TestUtils.toArguments
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals

internal class WindSpeedTest {

    // `WindSpeed?` is used to prohibit inlining in the bytecode

    @ParameterizedTest
    @MethodSource("valuesWithWindSpeeds")
    fun `get wind speed in meters per second`(value: Double, windSpeed: WindSpeed?) {
        assertEquals(
            expected = value,
            actual = windSpeed!!.metersPerSecond,
            absoluteTolerance = ABSOLUTE_TOLERANCE
        )
    }


    private companion object {

        @JvmStatic
        fun valuesWithWindSpeeds() = listOf(
            DOUBLE_42 to WindSpeed.inMetersPerSecond(value = DOUBLE_42)
        ).toArguments()
    }
}
