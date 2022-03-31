package bmp.lib

import bmp.TestUtils
import bmp.lib.ValidatedBmp.Companion.validated
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.assertDoesNotThrow
import kotlin.test.assertFailsWith

internal class ValidatedBmpTest {

    @Test
    fun `successfully validated 24 bit Bmp`() {
        assertDoesNotThrow {
            TestUtils.BMP24.validated()
        }
    }

    @Test
    fun `successfully validated 32 bit Bmp`() {
        assertDoesNotThrow {
            TestUtils.BMP32.validated()
        }
    }

    @Test
    fun `fail to validate file type`() {
        assertFailsWith<IllegalArgumentException>(message = "Wrong BMP definition") {
            TestUtils.BMP24.copy(
                fileHeader = TestUtils.BMP24.fileHeader.copy(
                    fileType = 100.toUShort()
                )
            ).validated()
        }
    }

    @Test
    fun `fail to validate bits per pixel`() {
        assertFailsWith<IllegalArgumentException>(message = "Wrong BMP definition") {
            TestUtils.BMP24.copy(
                infoHeader = TestUtils.BMP24.infoHeader.copy(
                    bitsPerPixel = 8.toUShort()
                )
            ).validated()
        }
    }
}
