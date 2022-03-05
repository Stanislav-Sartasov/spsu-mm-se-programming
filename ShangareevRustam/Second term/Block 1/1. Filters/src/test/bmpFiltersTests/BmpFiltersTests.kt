package test.bmpFiltersTests

import bmp.*
import org.junit.Test

class BmpFiltersTests {

	@Test
	fun grayTest24() {
		val bmp = readBmp("input/bmpFiltersTest24.bmp")
		bmp.applyFilter("gray")
		bmp.write("input/bmpFiltersTest24grayCompare.bmp")

		assert(
			readBmp("input/bmpFiltersTest24grayCompare.bmp").equals(
				readBmp("input/bmpFiltersTest24grayCorrect.bmp")
			)
		)
	}

	@Test
	fun gaussTest24() {
		val bmp = readBmp("input/bmpFiltersTest24.bmp")
		bmp.applyFilter("gauss")
		bmp.write("input/bmpFiltersTest24gaussCompare.bmp")

		assert(
			readBmp("input/bmpFiltersTest24gaussCompare.bmp").equals(
				readBmp("input/bmpFiltersTest24gaussCorrect.bmp")
			)
		)
	}

	@Test
	fun medianTest24() {
		val bmp = readBmp("input/bmpFiltersTest24.bmp")
		bmp.applyFilter("median")
		bmp.write("input/bmpFiltersTest24medianCompare.bmp")

		assert(
			readBmp("input/bmpFiltersTest24medianCompare.bmp").equals(
				readBmp("input/bmpFiltersTest24medianCorrect.bmp")
			)
		)
	}

	@Test
	fun sobelXTest24() {
		val bmp = readBmp("input/bmpFiltersTest24.bmp")
		bmp.applyFilter("sobelX")
		bmp.write("input/bmpFiltersTest24sobelXCompare.bmp")

		assert(
			readBmp("input/bmpFiltersTest24sobelXCompare.bmp").equals(
				readBmp("input/bmpFiltersTest24sobelXCorrect.bmp")
			)
		)
	}

	@Test
	fun sobelYTest24() {
		val bmp = readBmp("input/bmpFiltersTest24.bmp")
		bmp.applyFilter("sobelY")
		bmp.write("input/bmpFiltersTest24sobelYCompare.bmp")

		assert(
			readBmp("input/bmpFiltersTest24sobelYCompare.bmp").equals(
				readBmp("input/bmpFiltersTest24sobelYCorrect.bmp")
			)
		)
	}

	@Test
	fun grayTest32() {
		val bmp = readBmp("input/bmpFiltersTest32.bmp")
		bmp.applyFilter("gray")
		bmp.write("input/bmpFiltersTest32grayCompare.bmp")

		assert(
			readBmp("input/bmpFiltersTest32grayCompare.bmp").equals(
				readBmp("input/bmpFiltersTest32grayCorrect.bmp")
			)
		)
	}

	@Test
	fun gaussTest32() {
		val bmp = readBmp("input/bmpFiltersTest32.bmp")
		bmp.applyFilter("gauss")
		bmp.write("input/bmpFiltersTest32gaussCompare.bmp")

		assert(
			readBmp("input/bmpFiltersTest32gaussCompare.bmp").equals(
				readBmp("input/bmpFiltersTest32gaussCorrect.bmp")
			)
		)
	}

	@Test
	fun medianTest32() {
		val bmp = readBmp("input/bmpFiltersTest32.bmp")
		bmp.applyFilter("median")
		bmp.write("input/bmpFiltersTest32medianCompare.bmp")

		assert(
			readBmp("input/bmpFiltersTest32medianCompare.bmp").equals(
				readBmp("input/bmpFiltersTest32medianCorrect.bmp")
			)
		)
	}

	@Test
	fun sobelXTest32() {
		val bmp = readBmp("input/bmpFiltersTest32.bmp")
		bmp.applyFilter("sobelX")
		bmp.write("input/bmpFiltersTest32sobelXCompare.bmp")

		assert(
			readBmp("input/bmpFiltersTest32sobelXCompare.bmp").equals(
				readBmp("input/bmpFiltersTest32sobelXCorrect.bmp")
			)
		)
	}

	@Test
	fun sobelYTest32() {
		val bmp = readBmp("input/bmpFiltersTest32.bmp")
		bmp.applyFilter("sobelY")
		bmp.write("input/bmpFiltersTest32sobelYCompare.bmp")

		assert(
			readBmp("input/bmpFiltersTest32sobelYCompare.bmp").equals(
				readBmp("input/bmpFiltersTest32sobelYCorrect.bmp")
			)
		)
	}
}