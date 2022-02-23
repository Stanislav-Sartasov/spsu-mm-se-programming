package bmp.lib.filters

import bmp.lib.*
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

internal class Median3FilterTest {
    @Test
    fun filter() {
        assertEquals(
            expected = BmpIO.readBmp("src/test/resources/win_tulips_median_3.bmp").bmp.toRawImg(),
            actual = BmpIO.readBmp("src/test/resources/win_tulips.bmp").bmp.toRawImg().filter(Median3Filter)
        )
    }
}
