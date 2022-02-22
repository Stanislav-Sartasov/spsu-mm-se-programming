package libs

import org.junit.jupiter.api.Test

import org.junit.jupiter.api.Assertions.*

internal class BmpImageTest {

    @Test
    fun `trying to open broken image returns null`() {
        val img = BmpImage.open("src/test/resources/img/notABmpFile.bmp")
        assertNull(img)
    }

    @Test
    fun `correct image opens correctly`() {

    }

    @Test
    fun `an identical image after saving has the same pixel data`() {

    }
}