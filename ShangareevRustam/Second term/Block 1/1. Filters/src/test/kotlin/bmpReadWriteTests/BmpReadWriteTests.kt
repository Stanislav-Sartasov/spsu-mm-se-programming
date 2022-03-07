package bmpReadWriteTests

import org.junit.jupiter.api.Test
import bmpEditor.bmp.*
import bmpEditor.pixel.*

class BmpReadWriteTests {
	private val bmpFileHeader24 = BmpFileHeader(
		identifier = (0x4d42).toUShort(),
		fileSize = 118u,
		reserved1 = 0u,
		reserved2 = 0u,
		imageOffset = 54u
	)
	private val bmpInfoHeader24 = BmpInfoHeader(
		headerSize = 40u,
		imageWidth = 5,
		imageHeight = 4,
		numbOfColorPlanes = 1u,
		bitsPerPixel = 24u,
		compressionMethod = 0u,
		sizeOfBitmap = (bmpFileHeader24.fileSize - 54u),
		horResolution = 3780,
		verResolution = 3780,
		colorsUsed = 0u,
		colorsImportant = 0u
	)
	private val image24 = listOf(List(5) { Pixel(50, 200, 200, 0) },
		List(5) { Pixel(50, 200, 200, 0) },
		List(5) { Pixel(200, 100, 50, 0) },
		List(5) { Pixel(200, 100, 50, 0) })

	private val bmpFileHeader32 = BmpFileHeader(
		identifier = (0x4d42).toUShort(),
		fileSize = 174u,
		reserved1 = 0u,
		reserved2 = 0u,
		imageOffset = 54u
	)
	private val bmpInfoHeader32 = BmpInfoHeader(
		headerSize = 40u,
		imageWidth = 5,
		imageHeight = 6,
		numbOfColorPlanes = 1u,
		bitsPerPixel = 32u,
		compressionMethod = 0u,
		sizeOfBitmap = (bmpFileHeader32.fileSize - 54u),
		horResolution = 3780,
		verResolution = 3780,
		colorsUsed = 0u,
		colorsImportant = 0u
	)
	private val image32 = listOf(List(5) { Pixel(0, 0, 255, 255) },
		List(5) { Pixel(0, 0, 255, 255) },
		List(5) { Pixel(255, 0, 0, 100) },
		List(5) { Pixel(255, 0, 0, 100) },
		List(5) { Pixel(255, 255, 255, 200) },
		List(5) { Pixel(255, 255, 255, 200) })

	private val testBmp24 = Bmp(bmpFileHeader24, bmpInfoHeader24, image24)
	private val testBmp32 = Bmp(bmpFileHeader32, bmpInfoHeader32, image32)

	@Test
	fun bmpReadWriteTest24() {
		val firstBmp = readBmp("src/test/resources/bmpReadTest24.bmp")
		firstBmp.write("src/test/resources/newBmpReadTest24.bmp")
		val secondBmp = readBmp("src/test/resources/newBmpReadTest24.bmp")
		assert(firstBmp.equals(secondBmp))
	}

	@Test
	fun bmpReadWriteTest32() {
		val firstBmp = readBmp("src/test/resources/bmpReadTest32.bmp")
		firstBmp.write("src/test/resources/newBmpReadTest32.bmp")
		val secondBmp = readBmp("src/test/resources/newBmpReadTest32.bmp")
		assert(firstBmp.equals(secondBmp))
	}

	@Test
	fun bmpReadTest24() {
		val curBmp24 = readBmp("src/test/resources/bmpReadTest24.bmp")
		assert(curBmp24.equals(testBmp24))
	}

	@Test
	fun bmpReadTest32() {
		val curBmp32 = readBmp("src/test/resources/bmpReadTest32.bmp")
		assert(curBmp32.equals(testBmp32))
	}

	@Test
	fun bmpWriteTest24() {
		testBmp24.write("src/test/resources/bmpWriteTest24.bmp")
		assert(readBmp("src/test/resources/bmpWriteTest24.bmp").equals(testBmp24))
	}

	@Test
	fun bmpWriteTest32() {
		testBmp32.write("src/test/resources/bmpWriteTest32.bmp")
		assert(readBmp("src/test/resources/bmpWriteTest32.bmp").equals(testBmp32))
	}
}