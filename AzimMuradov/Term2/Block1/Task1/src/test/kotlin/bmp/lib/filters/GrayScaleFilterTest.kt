package bmp.lib.filters

import bmp.lib.*
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

internal class GrayScaleFilterTest {
    @Test
    fun filter() {
        assertEquals(
            expected = BmpIO.readBmp("src/test/resources/win_tulips_gray_scale.bmp").bmp.toRawImg(),
            actual = BmpIO.readBmp("src/test/resources/win_tulips.bmp").bmp.toRawImg().filter(GrayScaleFilter)
        )
    }
}
