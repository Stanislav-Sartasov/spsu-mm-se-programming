package bmpEditor.bmp

import bmpEditor.pixel.Pixel
import bmpEditor.pixel.abs
import bmpEditor.pixel.sum
import java.io.File
import java.io.IOException
import java.io.RandomAccessFile
import java.nio.ByteBuffer
import java.nio.ByteOrder
import java.nio.MappedByteBuffer
import java.nio.channels.FileChannel
import kotlin.system.exitProcess

data class BmpFileHeader(
	val identifier: UShort,
	// The header field used to identify the BMP
	// and DIB file is 0x42 0x4D in hexadecimal, same as BM in ASCII
	var fileSize: UInt,
	// The size of the BMP file in bytes
	val reserved1: UShort,
	val reserved2: UShort,
	// Reserved cells; actual value depends on the
	// application that creates the image, if created manually can be 0
	val imageOffset: UInt,
	// The offset, i.e. starting address, of the byte where
	// the bitmap image data (pixel array) can be found.
) {
	companion object {
		fun read(buffer: MappedByteBuffer): BmpFileHeader {
			// after each call to get bytes from the buffer,
			// the pointer in the buffer is moved by the number of bytes taken
			// using toUInt and toUShort is correct, because two's complement code is used
			return BmpFileHeader(
				identifier = buffer.short.toUShort(),
				fileSize = buffer.int.toUInt(),
				reserved1 = buffer.short.toUShort(),
				reserved2 = buffer.short.toUShort(),
				imageOffset = buffer.int.toUInt()
			)
		}
	}

	fun write(buffer: ByteBuffer) {
		with(this) {
			with(buffer) {
				putShort(identifier.toShort())
				putInt(fileSize.toInt())
				putShort(reserved1.toShort())
				putShort(reserved2.toShort())
				putInt(imageOffset.toInt())
			}
		}
	}
}

// DIB Header (Raster Information Header)
// BITMAPINFOHEADER - the common Windows format is the header
data class BmpInfoHeader(
	val headerSize: UInt,
	// the size of this header, in bytes
	// must be 40
	val imageWidth: Int,
	// the bitmap width in pixels (signed integer)
	val imageHeight: Int,
	// the bitmap height in pixels (signed integer)
	val numbOfColorPlanes: UShort,
	// the number of color planes
	// must be 1
	val bitsPerPixel: UShort,
	// the number of bits per pixel, which is the color depth of the image.
	// must be 24 or 32
	val compressionMethod: UInt,
	// the compression method being used(0 if not used)
	// must be 0
	val sizeOfBitmap: UInt,
	// the image size. This is the size of the raw bitmap data
	val horResolution: Int,
	// the horizontal resolution of the image
	// pixel per meter, signed integer
	val verResolution: Int,
	// the vertical resolution of the image
	// pixel per meter, signed integer
	val colorsUsed: UInt,
	// the number of colors in the color palette
	// 0 to default to 2^n
	val colorsImportant: UInt,
	// the number of important colors used or 0 when every color is important
	// generally ignored
) {
	companion object {
		fun read(buffer: MappedByteBuffer): BmpInfoHeader {
			// after each call to get bytes from the buffer,
			// the pointer in the buffer is moved by the number of bytes taken
			// using toUInt and toUShort is correct, because two's complement code is used
			return BmpInfoHeader(
				headerSize = buffer.int.toUInt(),
				imageWidth = buffer.int,
				imageHeight = buffer.int,
				numbOfColorPlanes = buffer.short.toUShort(),
				bitsPerPixel = buffer.short.toUShort(),
				compressionMethod = buffer.int.toUInt(),
				sizeOfBitmap = buffer.int.toUInt(),
				horResolution = buffer.int,
				verResolution = buffer.int,
				colorsUsed = buffer.int.toUInt(),
				colorsImportant = buffer.int.toUInt(),
			)
		}
	}

	fun write(buffer: ByteBuffer) {
		with(this) {
			with(buffer) {
				putInt(headerSize.toInt())
				putInt(imageWidth)
				putInt(imageHeight)
				putShort(numbOfColorPlanes.toShort())
				putShort(bitsPerPixel.toShort())
				putInt(compressionMethod.toInt())
				putInt(sizeOfBitmap.toInt())
				putInt(horResolution)
				putInt(verResolution)
				putInt(colorsUsed.toInt())
				putInt(colorsImportant.toInt())
			}
		}
	}
}

class Bmp(
	val bmpFileHeader: BmpFileHeader,
	val bmpInfoHeader: BmpInfoHeader,
	var image: List<List<Pixel>>
) {

	private val sobelYMatrix = listOf(
		1, 2, 1,
		0, 0, 0,
		-1, -2, -1
	)
	private val sobelXMatrix = listOf(
		-1, 0, 1,
		-2, 0, 2,
		-1, 0, 1
	)
	private val gaussMatrix = listOf(
		1, 2, 1,
		2, 4, 2,
		1, 2, 1
	)

	private fun readImage24(buffer: MappedByteBuffer) {
		// For 24 BPPs, "garbage" bytes are also read after reading each row
		// in the amount of (-3 * numbOfColumns) % 4.
		val numbOfGarbageBytes = (4 + (-3 * bmpInfoHeader.imageWidth) % 4) % 4

		image = List(size = bmpInfoHeader.imageHeight) {
			List(size = bmpInfoHeader.imageWidth) {
				Pixel(
					buffer.get().toUByte().toInt(),
					buffer.get().toUByte().toInt(),
					buffer.get().toUByte().toInt(),
					0
				)
			}.also {
				buffer.get(ByteArray(numbOfGarbageBytes))
			}
		}
	}

	private fun readImage32(buffer: MappedByteBuffer) {
		image = List(size = bmpInfoHeader.imageHeight) {
			List(bmpInfoHeader.imageWidth) {
				Pixel(
					buffer.get().toUByte().toInt(),
					buffer.get().toUByte().toInt(),
					buffer.get().toUByte().toInt(),
					buffer.get().toUByte().toInt()
				)
			}
		}
	}

	companion object {
		fun readBmp(filePath: String): Bmp? {
			val file = try {
				RandomAccessFile(filePath, "r")
			} catch (e: java.io.FileNotFoundException) {
				println("Error opening file. It is likely that a file with that name does not exist. Try again")
				exitProcess(0)
			}

			// use a memory-mapped byte buffer for better performance
			val buffer = try {
				file.channel.map(
					/* mode = */ FileChannel.MapMode.READ_ONLY,
					/* position = */ 0,
					/* size = */ file.length()
				).apply {
					order(ByteOrder.LITTLE_ENDIAN)
				}
			} catch (e: java.io.IOException) {
				println("Memory mapping error. The specified file is probably too large or corrupted. Try again")
				throw e
			}

			// the minimum file header size is 14 bytes
			// the minimum and only allowed size of the information header is 40 bytes
			if (file.length() < 54) return null

			val curBmpFileHeader = BmpFileHeader.read(buffer)
			val curBmpInfoHeader = BmpInfoHeader.read(buffer)

			// the minimum and only allowed size of the information header is 40 bytes
			if (curBmpInfoHeader.headerSize != 40u) return null

			val result: Bmp = Bmp(curBmpFileHeader, curBmpInfoHeader, listOf(listOf()))

			// creates a list of lists that consist of the pixels read from the buffer.
			when (curBmpInfoHeader.bitsPerPixel) {
				24.toUShort() -> result.readImage24(buffer)
				32.toUShort() -> result.readImage32(buffer)
				else -> return null
			}

			file.close()
			return result
		}
	}

	private fun getByteArrayOfImage24(): ByteArray {
		// in each line of image pixels, we divide each pixel into 3 bytes,
		// after processing the next line,
		// we supplement the number of bytes in the line up to a multiple of 4 (for DWORD)
		// At the end, we give a list of bytes to an array of bytes and put to the buffer
		val numbOfGarbageBytes = (4 + (-3 * bmpInfoHeader.imageWidth) % 4) % 4

		return image.flatMap { curList ->
			curList.flatMap { curPixel ->
				listOf(
					curPixel.blue.toByte(),
					curPixel.green.toByte(),
					curPixel.red.toByte()
				)
			} + List(numbOfGarbageBytes) { 0.toByte() }
		}.toByteArray()
	}

	private fun getByteArrayOfImage32(): ByteArray {
		return image.flatMap { curList ->
			curList.flatMap { curPixel ->
				listOf(
					curPixel.blue.toByte(),
					curPixel.green.toByte(),
					curPixel.red.toByte(),
					curPixel.alpha.toByte()
				)
			}
		}.toByteArray()
	}

	fun write(filePath: String): Boolean {
		val outputStream = File(filePath).outputStream()
		val buffer = try {
			ByteBuffer.allocate(bmpFileHeader.fileSize.toInt()).apply {
				order(ByteOrder.LITTLE_ENDIAN)
			}
		} catch (e: IOException) {
			return false
		}

		bmpFileHeader.write(buffer)
		bmpInfoHeader.write(buffer)

		when (bmpInfoHeader.bitsPerPixel) {
			24.toUShort() -> buffer.put(getByteArrayOfImage24())
			32.toUShort() -> buffer.put(getByteArrayOfImage32())
			else -> return false
		}

		outputStream.write(buffer.array())
		outputStream.close()
		return true
	}

	private fun getListsOf3x3Matrices(): List<List<Pair<Pair<List<Pixel>, List<Pixel>>, List<Pixel>>>> {
		// make a list of all possible triplets of lists of pixels
		return image.windowed(size = 3, step = 1).map { curTripleOfRows ->
			// in the next triple, we break each line into triples of pixels,
			// then we make triples of triples of pixels from them
			val tripleOfTriples = curTripleOfRows.map { row ->
				row.windowed(size = 3, step = 1)
			}
			return@map tripleOfTriples[0] zip tripleOfTriples[1] zip tripleOfTriples[2]
			// the result is rows from 3x3 matrices(windows) in which we need to change the central pixel
		}
	}

	private fun getListOfChangedBy3x3FilterPixels(
		rowOfWindows: List<Pair<Pair<List<Pixel>, List<Pixel>>, List<Pixel>>>,
		matrix: List<Int>,
		divider: Int
	): List<Pixel> {
		// consider each a 3x3 matrix separately
		val curImageRow = rowOfWindows.map { curWindow ->
			// unpack the 3x3 matrix into a convenient format
			val listOfWindow = curWindow.first.first + curWindow.first.second + curWindow.second
			// through the previously defined operations of addition,
			// multiplication and division of pixels, we obtain the value of the central pixel of the 3x3 matrix
			return@map abs(((listOfWindow zip matrix).map { (a, b) -> a * b }).sum() / divider)
		}
		// fill in the extreme pixels of each row of pixels, since they cannot be the center of a 3x3 matrix
		return listOf(curImageRow[0]) + curImageRow + listOf(curImageRow[curImageRow.size - 1])
	}

	private fun getListOfChangedByMedianFilterPixels(
		rowOfWindows: List<Pair<Pair<List<Pixel>, List<Pixel>>, List<Pixel>>>
	): List<Pixel> {
		// consider each a 3x3 matrix separately
		val curImageRow = rowOfWindows.map { curWindow ->
			// unpack the 3x3 matrix into a convenient format,
			// then put all the pixel channels in one continuous list
			val listOfWindow =
				(curWindow.first.first + curWindow.first.second + curWindow.second).flatMap { curPixel ->
					listOf(curPixel.blue, curPixel.green, curPixel.red, curPixel.alpha)
				}
			// sort each channel, considering that the values of the same channel
			// are between each other with a gap of 3 elements
			val channels = MutableList(4) { 0 }
			for (i in 0..3) channels[i] = listOfWindow.slice(
				i until 36 step 4
			).sorted()[4]
			return@map Pixel(channels[0], channels[1], channels[2], channels[3])
		}
		return listOf(curImageRow[0]) + curImageRow + listOf(curImageRow[curImageRow.size - 1])
	}

	private fun apply3x3Filter(matrix: List<Int>, divider: Int) {
		var newImage = getListsOf3x3Matrices().map { rowOfWindows ->
			getListOfChangedBy3x3FilterPixels(rowOfWindows, matrix, divider)
		}
		// we also duplicate the first and last row of the matrix of pixels,
		// because the pixels on them cannot be the centers of a 3x3 matrix
		image = listOf(newImage[0]) + newImage + listOf(newImage[newImage.size - 1])
	}

	private fun applyMedianFilter() {
		var newImage = getListsOf3x3Matrices().map { rowOfWindows ->
			getListOfChangedByMedianFilterPixels(rowOfWindows)
		}
		// fill in the extreme pixels of each row of pixels, since they cannot be the center of a 3x3 matrix
		image = listOf(newImage[0]) + newImage + listOf(newImage[newImage.size - 1])
	}

	private fun applyGrayFilter() {
		image = image.map { curList ->
			curList.map { curPixel ->
				val grayValue = (0.1 * curPixel.blue.toDouble() +
						0.6 * curPixel.green.toDouble() + 0.3 * curPixel.red.toDouble()).toInt()
				Pixel(grayValue, grayValue, grayValue, grayValue)
			}
		}
	}

	fun applyFilter(filterName: String): Boolean {
		when (filterName) {
			"gray" -> {
				applyGrayFilter()
			}
			"median" -> {
				applyMedianFilter()
			}
			"sobelX" -> {
				apply3x3Filter(matrix = sobelXMatrix, divider = 1)
				applyGrayFilter()
			}
			"sobelY" -> {
				apply3x3Filter(matrix = sobelYMatrix, divider = 1)
				applyGrayFilter()
			}
			"gauss" -> {
				apply3x3Filter(matrix = gaussMatrix, divider = 16)
			}
			else -> return false
		}
		return true
	}

	fun equals(element: Bmp): Boolean {
		return (bmpFileHeader == element.bmpFileHeader &&
				bmpInfoHeader == element.bmpInfoHeader &&
				image == element.image)
	}
}