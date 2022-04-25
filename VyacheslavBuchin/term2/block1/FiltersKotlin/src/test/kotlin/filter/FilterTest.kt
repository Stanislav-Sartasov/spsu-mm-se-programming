package filter

import FilterCLI
import bmp.BMPFile
import io.BMPReader
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments
import org.junit.jupiter.params.provider.MethodSource

import java.io.BufferedInputStream
import java.io.FileInputStream
import kotlin.test.assertEquals

internal class FilterTest {

	private lateinit var image: BMPFile
	private lateinit var exceptedImage: BMPFile
	private val testFilesDirectory = "src/test/resources"
	private val testFile = "test.bmp"


	companion object {
		private val filters = FilterCLI().filters.entries
		@JvmStatic
		fun filter() = filters.map { Arguments.of("${it.key}.bmp", it.value) }.toList()
	}

	@ParameterizedTest
	@MethodSource("filter")
	fun `filtered image should be the same as reference image`(referenceName: String, filter: Filter) {
		BufferedInputStream(FileInputStream("$testFilesDirectory/$testFile")).use {
			image = BMPReader(it).readBMP()
		}
		BufferedInputStream(FileInputStream("$testFilesDirectory/$referenceName")).use {
			exceptedImage = BMPReader(it).readBMP()
		}
		filter.applyTo(image)
		assertEquals(image, exceptedImage)
	}

}