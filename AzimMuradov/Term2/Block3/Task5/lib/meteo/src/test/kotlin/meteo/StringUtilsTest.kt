package meteo

import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments.arguments
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals

internal class StringUtilsTest {

    @ParameterizedTest
    @MethodSource("stringsWithLines")
    fun `convert string to line`(string: String, line: String) {
        assertEquals(expected = line, actual = string.ln())
    }


    private companion object {

        @JvmStatic
        fun stringsWithLines() = listOf("", "a", "\n", System.lineSeparator()).map {
            arguments(it, "$it${System.lineSeparator()}")
        }
    }
}
