package bmp.lib.filters

import bmp.lib.*
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

internal class Gauss5FilterTest {
    @Test
    fun filter() {
        assertEquals(
            expected = BmpIO.readBmp("src/test/resources/win_tulips_gauss_5.bmp").bmp.toRawImg(),
            actual = BmpIO.readBmp("src/test/resources/win_tulips.bmp").bmp.toRawImg().filter(Gauss5Filter)
        )
    }
}
