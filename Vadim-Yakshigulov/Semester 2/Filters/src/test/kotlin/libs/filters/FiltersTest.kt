package libs.filters

import libs.BmpImage
import libs.ImageFilter
import org.junit.jupiter.api.Assertions.assertArrayEquals
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments
import org.junit.jupiter.params.provider.MethodSource
import java.io.File
import java.util.stream.Stream
import kotlin.io.path.*

internal class FiltersTest {

    companion object {
        @JvmStatic
        fun filtersProvider(): Stream<Arguments> {
            return Stream.of(
                Arguments.of("gaussian_3x3", GaussianFilter3x3),
                Arguments.of("greyscale", GreyscaleFilter),
                Arguments.of("gaussian_5x5", GaussianFilter5x5),
                Arguments.of("sobelX", SobelXFilter),
                Arguments.of("sobelY", SobelYFilter),
                Arguments.of("median", MedianFilter)
            )
        }
    }

    @ParameterizedTest(name = "Applying {0} filter")
    @MethodSource("filtersProvider")
    fun `applying filters on c and on kotlin returns the same images`(name: String, filter: ImageFilter) {
        val tempFolder = File(createTempDirectory("tempFiltersTestFolder").toUri())

        val img = BmpImage.open("src/test/resources/img/kitten.bmp")!!
        filter.applyFor(img)
        img.saveAs(tempFolder.resolve("kittenAfterKotlin.bmp").absolutePath)

        ProcessBuilder(
            "src/test/resources/CFilters",
            "src/test/resources/img/kitten.bmp",
            name,
            tempFolder.resolve("kittenAfterC.bmp").absolutePath
        ).start().waitFor()

        val imgAfterKotlinFilters =
            BmpImage.open(tempFolder.resolve("kittenAfterKotlin.bmp").absolutePath)
        val imgAfterCFilters = BmpImage.open(tempFolder.resolve("kittenAfterC.bmp").absolutePath)

        assertArrayEquals(imgAfterCFilters?.pixelData, imgAfterKotlinFilters?.pixelData)

        tempFolder.deleteRecursively()
    }
}