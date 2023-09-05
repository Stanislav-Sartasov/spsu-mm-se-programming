package bmp

import bmp.bmp.filters.*
import org.junit.jupiter.api.Assertions.assertArrayEquals
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertNotNull

internal class BMPImageTest {

    @Test
    fun open24bpp() {
        assertNotNull(BMPImage.open("src/test/resources/test24bpp.bmp"))
    }

    @Test
    fun open32bpp() {
        assertNotNull(BMPImage.open("src/test/resources/test32bpp.bmp"))
    }

    @Test
    fun saveAndOpen() {
        val tmpFile = kotlin.io.path.createTempFile("", ".bmp").toString()
        val img1 = BMPImage.open("src/test/resources/test24bpp.bmp")
        img1?.save(tmpFile)
        val img2 = BMPImage.open(tmpFile)
        assertEquals(img1?.header, img2?.header)
        assertArrayEquals(img1?.data, img2?.data)
    }

    @Test
    fun gauss2() {
        val img = BMPImage.open("src/test/resources/test24bpp.bmp")?.apply { applyFilter(GaussianFilter(2)) }
        val ref = BMPImage.open("src/test/resources/gauss.bmp")
        assertArrayEquals(img?.data, ref?.data)
    }

    @Test
    fun sobelx() {
        val img = BMPImage.open("src/test/resources/test24bpp.bmp")?.apply { applyFilter(SobelXFilter) }
        val ref = BMPImage.open("src/test/resources/sobelx.bmp")
        assertArrayEquals(img?.data, ref?.data)
    }

    @Test
    fun sobely() {
        val img = BMPImage.open("src/test/resources/test24bpp.bmp")?.apply { applyFilter(SobelYFilter) }
        val ref = BMPImage.open("src/test/resources/sobely.bmp")
        assertArrayEquals(img?.data, ref?.data)
    }

    @Test
    fun median() {
        val img = BMPImage.open("src/test/resources/test24bpp.bmp")?.apply { applyFilter(MedianFilter(2)) }
        val ref = BMPImage.open("src/test/resources/median.bmp")
        assertArrayEquals(img?.data, ref?.data)
    }

    @Test
    fun gray() {
        val img = BMPImage.open("src/test/resources/test24bpp.bmp")?.apply { applyFilter(GrayscaleFilter) }
        val ref = BMPImage.open("src/test/resources/gray.bmp")
        assertArrayEquals(img?.data, ref?.data)
    }
}