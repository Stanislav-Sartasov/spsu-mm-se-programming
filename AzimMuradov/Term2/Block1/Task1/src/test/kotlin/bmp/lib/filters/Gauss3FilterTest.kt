package bmp.lib.filters

import bmp.lib.*
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

internal class Gauss3FilterTest {
    @Test
    fun filter() {
        assertEquals(
            expected = BmpIO.readBmp("src/test/resources/win_tulips_gauss_3.bmp").bmp.toRawImg(),
            actual = BmpIO.readBmp("src/test/resources/win_tulips.bmp").bmp.toRawImg().filter(Gauss3Filter)
        )
    }
}
