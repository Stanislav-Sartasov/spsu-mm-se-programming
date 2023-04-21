package meteo.domain.entity

import meteo.domain.entity.TestUtils.INT_42
import meteo.domain.entity.TestUtils.toArguments
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals

internal class WindDirectionTest {

    // `WindDirection?` is used to prohibit inlining in the bytecode

    @ParameterizedTest
    @MethodSource("valuesWithWindDirections")
    fun `get wind direction in degrees`(value: Int, windDirection: WindDirection?) {
        assertEquals(
            expected = value,
            actual = windDirection!!.degrees,
        )
    }


    private companion object {

        @JvmStatic
        fun valuesWithWindDirections() = listOf(
            INT_42 to WindDirection.inDegrees(value = INT_42)
        ).toArguments()
    }
}
