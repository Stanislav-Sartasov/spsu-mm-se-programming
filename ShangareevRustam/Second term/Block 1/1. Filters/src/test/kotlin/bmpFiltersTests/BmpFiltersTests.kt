package bmpFiltersTests

import bmpEditor.bmp.Bmp
import org.junit.jupiter.api.Test

class BmpFiltersTests {

	@Test
	fun grayTest24() {
		val bmp = Bmp.readBmp("src/test/resources/bmpFiltersTest24.bmp")
		bmp?.applyFilter("gray")
		bmp?.write("src/test/resources/bmpFiltersTest24grayCompare.bmp")

		assert(
			Bmp.readBmp("src/test/resources/bmpFiltersTest24grayCompare.bmp")!!.equals(
				Bmp.readBmp("src/test/resources/bmpFiltersTest24grayCorrect.bmp")!!
			)
		)
	}

	@Test
	fun gaussTest24() {
		val bmp = Bmp.readBmp("src/test/resources/bmpFiltersTest24.bmp")
		bmp?.applyFilter("gauss")
		bmp?.write("src/test/resources/bmpFiltersTest24gaussCompare.bmp")

		assert(
			Bmp.readBmp("src/test/resources/bmpFiltersTest24gaussCompare.bmp")!!.equals(
				Bmp.readBmp("src/test/resources/bmpFiltersTest24gaussCorrect.bmp")!!
			)
		)
	}

	@Test
	fun medianTest24() {
		val bmp = Bmp.readBmp("src/test/resources/bmpFiltersTest24.bmp")
		bmp?.applyFilter("median")
		bmp?.write("src/test/resources/bmpFiltersTest24medianCompare.bmp")

		assert(
			Bmp.readBmp("src/test/resources/bmpFiltersTest24medianCompare.bmp")!!.equals(
				Bmp.readBmp("src/test/resources/bmpFiltersTest24medianCorrect.bmp")!!
			)
		)
	}

	@Test
	fun sobelXTest24() {
		val bmp = Bmp.readBmp("src/test/resources/bmpFiltersTest24.bmp")
		bmp!!.applyFilter("sobelX")
		bmp.write("src/test/resources/bmpFiltersTest24sobelXCompare.bmp")

		assert(
			Bmp.readBmp("src/test/resources/bmpFiltersTest24sobelXCompare.bmp")!!.equals(
				Bmp.readBmp("src/test/resources/bmpFiltersTest24sobelXCorrect.bmp")!!
			)
		)
	}

	@Test
	fun sobelYTest24() {
		val bmp = Bmp.readBmp("src/test/resources/bmpFiltersTest24.bmp")
		bmp!!.applyFilter("sobelY")
		bmp.write("src/test/resources/bmpFiltersTest24sobelYCompare.bmp")

		assert(
			Bmp.readBmp("src/test/resources/bmpFiltersTest24sobelYCompare.bmp")!!.equals(
				Bmp.readBmp("src/test/resources/bmpFiltersTest24sobelYCorrect.bmp")!!
			)
		)
	}

	@Test
	fun grayTest32() {
		val bmp = Bmp.readBmp("src/test/resources/bmpFiltersTest32.bmp")
		bmp!!.applyFilter("gray")
		bmp.write("src/test/resources/bmpFiltersTest32grayCompare.bmp")

		assert(
			Bmp.readBmp("src/test/resources/bmpFiltersTest32grayCompare.bmp")!!.equals(
				Bmp.readBmp("src/test/resources/bmpFiltersTest32grayCorrect.bmp")!!
			)
		)
	}

	@Test
	fun gaussTest32() {
		val bmp = Bmp.readBmp("src/test/resources/bmpFiltersTest32.bmp")
		bmp!!.applyFilter("gauss")
		bmp.write("src/test/resources/bmpFiltersTest32gaussCompare.bmp")

		assert(
			Bmp.readBmp("src/test/resources/bmpFiltersTest32gaussCompare.bmp")!!.equals(
				Bmp.readBmp("src/test/resources/bmpFiltersTest32gaussCorrect.bmp")!!
			)
		)
	}

	@Test
	fun medianTest32() {
		val bmp = Bmp.readBmp("src/test/resources/bmpFiltersTest32.bmp")
		bmp!!.applyFilter("median")
		bmp.write("src/test/resources/bmpFiltersTest32medianCompare.bmp")

		assert(
			Bmp.readBmp("src/test/resources/bmpFiltersTest32medianCompare.bmp")!!.equals(
				Bmp.readBmp("src/test/resources/bmpFiltersTest32medianCorrect.bmp")!!
			)
		)
	}

	@Test
	fun sobelXTest32() {
		val bmp = Bmp.readBmp("src/test/resources/bmpFiltersTest32.bmp")
		bmp!!.applyFilter("sobelX")
		bmp.write("src/test/resources/bmpFiltersTest32sobelXCompare.bmp")

		assert(
			Bmp.readBmp("src/test/resources/bmpFiltersTest32sobelXCompare.bmp")!!.equals(
				Bmp.readBmp("src/test/resources/bmpFiltersTest32sobelXCorrect.bmp")!!
			)
		)
	}

	@Test
	fun sobelYTest32() {
		val bmp = Bmp.readBmp("src/test/resources/bmpFiltersTest32.bmp")
		bmp!!.applyFilter("sobelY")
		bmp.write("src/test/resources/bmpFiltersTest32sobelYCompare.bmp")


		assert(
			Bmp.readBmp("src/test/resources/bmpFiltersTest32sobelYCompare.bmp")!!.equals(
				Bmp.readBmp("src/test/resources/bmpFiltersTest32sobelYCorrect.bmp")!!
			)
		)
	}
}