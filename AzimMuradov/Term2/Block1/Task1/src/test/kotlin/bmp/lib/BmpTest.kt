package bmp.lib

import bmp.TestUtils
import bmp.lib.ValidatedBmp.ValidBmpBitsPerPixel
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

internal class BmpTest {

    @Test
    fun `convert 24 bit Bmp to RawImg`() {
        assertEquals(
            expected = RawImg(
                width = TestUtils.BMP24.infoHeader.width,
                height = TestUtils.BMP24.infoHeader.height,
                pixels = TestUtils.BMP24.pixels
            ),
            actual = TestUtils.BMP24.toRawImg()
        )
    }

    @Test
    fun `convert 32 bit Bmp to RawImg`() {
        assertEquals(
            expected = RawImg(
                width = TestUtils.BMP32.infoHeader.width,
                height = TestUtils.BMP32.infoHeader.height,
                pixels = TestUtils.BMP32.pixels
            ),
            actual = TestUtils.BMP32.toRawImg()
        )
    }

    @Test
    fun `convert RawImg to 24 bit Bmp`() {
        assertEquals(
            expected = TestUtils.BMP24,
            actual = RawImg(
                width = TestUtils.BMP24.infoHeader.width,
                height = TestUtils.BMP24.infoHeader.height,
                pixels = TestUtils.BMP24.pixels
            ).toBmp(ValidBmpBitsPerPixel.BPP_24)
        )
    }

    @Test
    fun `convert RawImg to 32 bit Bmp`() {
        assertEquals(
            expected = TestUtils.BMP32,
            actual = RawImg(
                width = TestUtils.BMP32.infoHeader.width,
                height = TestUtils.BMP32.infoHeader.height,
                pixels = TestUtils.BMP32.pixels
            ).toBmp(ValidBmpBitsPerPixel.BPP_32)
        )
    }
}
