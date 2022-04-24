package meteo.domain.entity

import meteo.domain.entity.TestUtils.INT_42
import meteo.domain.entity.TestUtils.toArguments
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals

internal class HumidityTest {

    // `Humidity?` is used to prohibit inlining in the bytecode

    @ParameterizedTest
    @MethodSource("valuesWithHumidity")
    fun `get humidity in percent`(value: Int, humidity: Humidity?) {
        assertEquals(
            expected = value,
            actual = humidity!!.percent,
        )
    }


    private companion object {

        @JvmStatic
        fun valuesWithHumidity() = listOf(
            INT_42 to Humidity.inPercent(value = INT_42)
        ).toArguments()
    }
}
