package bmp.lib

import bmp.CliApp
import bmp.TestUtils.TEST_RES_PATH
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.streams.asStream
import kotlin.test.assertEquals

internal class FiltersTest {

    @ParameterizedTest
    @MethodSource("filtersNamesWithFilters")
    fun filter(data: Map.Entry<String, ImgFilter>) {
        val (name, filter) = data
        assertEquals(
            expected = BmpIO.readBmp(path = "${TEST_RES_PATH}win_tulips_$name.bmp").bmp.toRawImg(),
            actual = BmpIO.readBmp(path = "${TEST_RES_PATH}win_tulips.bmp").bmp.toRawImg().filter(filter)
        )
    }


    private companion object {

        @JvmStatic
        private fun filtersNamesWithFilters() = CliApp.FILTERS.asSequence().asStream()
    }
}
