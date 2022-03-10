package bmp.lib.filters

import bmp.TestUtils.TEST_RES_PATH
import bmp.lib.*
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

internal class Gauss5FilterTest {
    @Test
    fun filter() {
        assertEquals(
            expected = BmpIO.readBmp("${TEST_RES_PATH}win_tulips_gauss_5.bmp").bmp.toRawImg(),
            actual = BmpIO.readBmp("${TEST_RES_PATH}win_tulips.bmp").bmp.toRawImg().filter(Gauss5Filter)
        )
    }
}
