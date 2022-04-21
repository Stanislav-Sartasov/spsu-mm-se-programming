package meteo.domain.entity

import meteo.domain.entity.TestUtils.ABSOLUTE_TOLERANCE
import meteo.domain.entity.TestUtils.DOUBLE_42
import meteo.domain.entity.TestUtils.toArguments
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals

internal class PrecipitationTest {

    // `Precipitation?` is used to prohibit inlining in the bytecode

    @ParameterizedTest
    @MethodSource("valuesWithPrecipitations")
    fun `get precipitation in mm per hour`(value: Double, precipitation: Precipitation?) {
        assertEquals(
            expected = value,
            actual = precipitation!!.mmPerHour,
            absoluteTolerance = ABSOLUTE_TOLERANCE
        )
    }


    private companion object {

        @JvmStatic
        fun valuesWithPrecipitations() = listOf(
            DOUBLE_42 to Precipitation.inMmPerHour(value = DOUBLE_42)
        ).toArguments()
    }
}
