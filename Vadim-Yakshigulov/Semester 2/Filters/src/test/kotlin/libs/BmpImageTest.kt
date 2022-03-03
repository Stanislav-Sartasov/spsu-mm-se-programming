package libs

import libs.filters.GreyscaleFilter
import org.junit.jupiter.api.Assertions
import org.junit.jupiter.api.Assertions.assertArrayEquals
import org.junit.jupiter.api.Test
import java.io.File
import kotlin.io.path.absolutePathString
import kotlin.io.path.createTempDirectory
import kotlin.test.assertNotNull
import kotlin.test.assertNull

internal class BmpImageTest {
    @Test
    fun `trying to open empty image returns null`() {
        val img = BmpImage.open("src/test/resources/img/notABmpFile.bmp")
        assertNull(img)
    }

    @Test
    fun `trying to open broken image returns null`() {
        val img = BmpImage.open("src/test/resources/img/brokenKitten.bmp")
        assertNull(img)
    }

    @Test
    fun `correct image opens correctly`() {
        val img = BmpImage.open("src/test/resources/img/kitten.bmp")
        assertNotNull(img)
    }

    @Test
    fun `an identical image after saving has the same pixel data`() {
        val tempFolder = File(createTempDirectory("tempFiltersTestFolder").toUri())
        val img1 = BmpImage.open("src/test/resources/img/kitten.bmp")
        img1?.saveAs(tempFolder.resolve("kittenCopy").absolutePath)

        val img2 = BmpImage.open(tempFolder.resolve("kittenCopy").absolutePath)
        assertArrayEquals(img1?.pixelData, img2?.pixelData)

        tempFolder.deleteRecursively()
    }

    @Test
    fun `can apply filter from image directly`() {
        val img1 = BmpImage.open("src/test/resources/img/kitten.bmp")!!
        img1.applyFilter(GreyscaleFilter)

        val img2 = BmpImage.open("src/test/resources/img/kitten.bmp")!!
        GreyscaleFilter.applyFor(img2)

        assertArrayEquals(img1.pixelData, img2.pixelData)
    }

    @Test
    fun `can open 32bit per pixel images`() {
        val img = BmpImage.open("src/test/resources/img/32.bmp")
        assertNotNull(img)
    }

    @Test
    fun `can open 24bit per pixel images`() {
        val img = BmpImage.open("src/test/resources/img/24.bmp")
        assertNotNull(img)
    }
}