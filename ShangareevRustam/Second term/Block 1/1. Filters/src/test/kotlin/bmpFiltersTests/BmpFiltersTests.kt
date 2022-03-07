package bmpFiltersTests

import bmpEditor.bmp.*
import org.junit.jupiter.api.Test

class BmpFiltersTests {

	@Test
	fun grayTest24() {
		val bmp = readBmp("src/test/resources/bmpFiltersTest24.bmp")
		bmp.applyFilter("gray")
		bmp.write("src/test/resources/bmpFiltersTest24grayCompare.bmp")

		assert(
			readBmp("src/test/resources/bmpFiltersTest24grayCompare.bmp").equals(
				readBmp("src/test/resources/bmpFiltersTest24grayCorrect.bmp")
			)
		)
	}

	@Test
	fun gaussTest24() {
		val bmp = readBmp("src/test/resources/bmpFiltersTest24.bmp")
		bmp.applyFilter("gauss")
		bmp.write("src/test/resources/bmpFiltersTest24gaussCompare.bmp")

		assert(
			readBmp("src/test/resources/bmpFiltersTest24gaussCompare.bmp").equals(
				readBmp("src/test/resources/bmpFiltersTest24gaussCorrect.bmp")
			)
		)
	}

	@Test
	fun medianTest24() {
		val bmp = readBmp("src/test/resources/bmpFiltersTest24.bmp")
		bmp.applyFilter("median")
		bmp.write("src/test/resources/bmpFiltersTest24medianCompare.bmp")

		assert(
			readBmp("src/test/resources/bmpFiltersTest24medianCompare.bmp").equals(
				readBmp("src/test/resources/bmpFiltersTest24medianCorrect.bmp")
			)
		)
	}

	@Test
	fun sobelXTest24() {
		val bmp = readBmp("src/test/resources/bmpFiltersTest24.bmp")
		bmp.applyFilter("sobelX")
		bmp.write("src/test/resources/bmpFiltersTest24sobelXCompare.bmp")

		assert(
			readBmp("src/test/resources/bmpFiltersTest24sobelXCompare.bmp").equals(
				readBmp("src/test/resources/bmpFiltersTest24sobelXCorrect.bmp")
			)
		)
	}

	@Test
	fun sobelYTest24() {
		val bmp = readBmp("src/test/resources/bmpFiltersTest24.bmp")
		bmp.applyFilter("sobelY")
		bmp.write("src/test/resources/bmpFiltersTest24sobelYCompare.bmp")

		assert(
			readBmp("src/test/resources/bmpFiltersTest24sobelYCompare.bmp").equals(
				readBmp("src/test/resources/bmpFiltersTest24sobelYCorrect.bmp")
			)
		)
	}

	@Test
	fun grayTest32() {
		val bmp = readBmp("src/test/resources/bmpFiltersTest32.bmp")
		bmp.applyFilter("gray")
		bmp.write("src/test/resources/bmpFiltersTest32grayCompare.bmp")

		assert(
			readBmp("src/test/resources/bmpFiltersTest32grayCompare.bmp").equals(
				readBmp("src/test/resources/bmpFiltersTest32grayCorrect.bmp")
			)
		)
	}

	@Test
	fun gaussTest32() {
		val bmp = readBmp("src/test/resources/bmpFiltersTest32.bmp")
		bmp.applyFilter("gauss")
		bmp.write("src/test/resources/bmpFiltersTest32gaussCompare.bmp")

		assert(
			readBmp("src/test/resources/bmpFiltersTest32gaussCompare.bmp").equals(
				readBmp("src/test/resources/bmpFiltersTest32gaussCorrect.bmp")
			)
		)
	}

	@Test
	fun medianTest32() {
		val bmp = readBmp("src/test/resources/bmpFiltersTest32.bmp")
		bmp.applyFilter("median")
		bmp.write("src/test/resources/bmpFiltersTest32medianCompare.bmp")

		assert(
			readBmp("src/test/resources/bmpFiltersTest32medianCompare.bmp").equals(
				readBmp("src/test/resources/bmpFiltersTest32medianCorrect.bmp")
			)
		)
	}

	@Test
	fun sobelXTest32() {
		val bmp = readBmp("src/test/resources/bmpFiltersTest32.bmp")
		bmp.applyFilter("sobelX")
		bmp.write("src/test/resources/bmpFiltersTest32sobelXCompare.bmp")

		assert(
			readBmp("src/test/resources/bmpFiltersTest32sobelXCompare.bmp").equals(
				readBmp("src/test/resources/bmpFiltersTest32sobelXCorrect.bmp")
			)
		)
	}

	@Test
	fun sobelYTest32() {
		val bmp = readBmp("src/test/resources/bmpFiltersTest32.bmp")
		bmp.applyFilter("sobelY")
		bmp.write("src/test/resources/bmpFiltersTest32sobelYCompare.bmp")

		assert(
			readBmp("src/test/resources/bmpFiltersTest32sobelYCompare.bmp").equals(
				readBmp("src/test/resources/bmpFiltersTest32sobelYCorrect.bmp")
			)
		)
	}
}