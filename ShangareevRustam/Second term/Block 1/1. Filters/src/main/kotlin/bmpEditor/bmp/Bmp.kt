package bmpEditor.bmp

import bmpEditor.*
import java.io.File
import java.io.RandomAccessFile
import java.nio.ByteBuffer
import java.nio.ByteOrder
import java.nio.channels.FileChannel
import kotlin.system.exitProcess
import bmpEditor.pixel.*

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
)

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
)

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

	companion object {
		fun readBmp(filePath: String): Bmp {
			val file = try {
				RandomAccessFile(filePath, "r")
			} catch (e: java.io.FileNotFoundException) {
				println("Error opening file. It is likely that a file with that name does not exist. Try again")
				inputPrompt()
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
				inputPrompt()
				exitProcess(0)
			}

			// the minimum file header size is 14 bytes
			// the minimum and only allowed size of the information header is 40 bytes
			if (file.length() < 54) {
				println(
					"Bmp file is corrupted or the header information file" +
							" is not in the common Windows BITMAPINFOHEADER format. Try again"
				)
				inputPrompt()
				exitProcess(0)
			}

			// after each call to get bytes from the buffer,
			// the pointer in the buffer is moved by the number of bytes taken
			val curBmpFileHeader = BmpFileHeader(
				identifier = buffer.short.toUShort(),
				fileSize = buffer.int.toUInt(),
				reserved1 = buffer.short.toUShort(),
				reserved2 = buffer.short.toUShort(),
				imageOffset = buffer.int.toUInt()
			)

			// using toUInt and toUShort is correct, because two's complement code is used
			val curBmpInfoHeader = BmpInfoHeader(
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

			// the minimum and only allowed size of the information header is 40 bytes
			if (curBmpInfoHeader.headerSize != 40u) {
				println(
					"Bmp file is corrupted or the header information file " +
							"is not in the common Windows BITMAPINFOHEADER format! Try again"
				)
				inputPrompt()
				exitProcess(0)
			}

			val numbOfRows = curBmpInfoHeader.imageHeight
			val numbOfColumns = curBmpInfoHeader.imageWidth
			val numbOfGarbageBytes = (4 + (-3 * numbOfColumns) % 4) % 4
			// creates a list of lists that consist of the pixels read from the buffer.
			// For 24 BPPs, "garbage" bytes are also read after reading each row in the amount of (-3 * numbOfColumns) % 4.
			val pixels: List<List<Pixel>> = when (curBmpInfoHeader.bitsPerPixel) {
				24.toUShort() -> {
					List(size = numbOfRows) {
						List(size = numbOfColumns) {
							Pixel(
								buffer.get().toUByte().toInt(),
								buffer.get().toUByte().toInt(), buffer.get().toUByte().toInt(), 0
							)
						}.also {
							buffer.get(ByteArray(numbOfGarbageBytes))
						}
					}
				}
				32.toUShort() -> {
					List(size = numbOfRows) {
						List(numbOfColumns) {
							Pixel(
								buffer.get().toUByte().toInt(),
								buffer.get().toUByte().toInt(),
								buffer.get().toUByte().toInt(),
								buffer.get().toUByte().toInt()
							)
						}
					}
				}
				else -> {
					println("Bmp file don't have 24 or 32 bits per pixel. Try again")
					inputPrompt()
					exitProcess(0)
				}
			}

			file.close()
			return Bmp(curBmpFileHeader, curBmpInfoHeader, pixels)
		}
	}

	fun write(filePath: String) {
		val outputStream = File(filePath).outputStream()
		val buffer = ByteBuffer.allocate(bmpFileHeader.fileSize.toInt()).apply {
			order(ByteOrder.LITTLE_ENDIAN)
		}

		// put bmp file header in buffer
		with(bmpFileHeader) {
			buffer.putShort(identifier.toShort())
			buffer.putInt(fileSize.toInt())
			buffer.putShort(reserved1.toShort())
			buffer.putShort(reserved2.toShort())
			buffer.putInt(imageOffset.toInt())
		}

		// put bmp info header in buffer
		with(bmpInfoHeader) {
			buffer.putInt(headerSize.toInt())
			buffer.putInt(imageWidth)
			buffer.putInt(imageHeight)
			buffer.putShort(numbOfColorPlanes.toShort())
			buffer.putShort(bitsPerPixel.toShort())
			buffer.putInt(compressionMethod.toInt())
			buffer.putInt(sizeOfBitmap.toInt())
			buffer.putInt(horResolution)
			buffer.putInt(verResolution)
			buffer.putInt(colorsUsed.toInt())
			buffer.putInt(colorsImportant.toInt())
		}

		// in each line of image pixels, we divide each pixel into 3 bytes,
		// after processing the next line,
		// we supplement the number of bytes in the line up to a multiple of 4 (for DWORD)
		// At the end, we give a list of bytes to an array of bytes and put to the buffer
		val numbOfGarbageBytes = (4 + (-3 * bmpInfoHeader.imageWidth) % 4) % 4
		val byteArrayOfPixels = when (bmpInfoHeader.bitsPerPixel) {
			24.toUShort() -> image.flatMap { curList ->
				curList.flatMap { curPixel ->
					listOf(
						curPixel.blue.toByte(),
						curPixel.green.toByte(),
						curPixel.red.toByte()
					)
				} + List(numbOfGarbageBytes) { 0.toByte() }
			}.toByteArray()
			32.toUShort() -> image.flatMap { curList ->
				curList.flatMap { curPixel ->
					listOf(
						curPixel.blue.toByte(),
						curPixel.green.toByte(),
						curPixel.red.toByte(),
						curPixel.alpha.toByte()
					)
				}
			}.toByteArray()
			else -> throw java.lang.IllegalArgumentException(
				"bmp file cannot be written because has an incorrect number of bits per pixel (not 24 or 32)"
			)
		}
		buffer.put(byteArrayOfPixels)

		outputStream.write(buffer.array())
		outputStream.close()
	}

	private fun apply3x3Filter(matrix: List<Int>, divider: Int) {
		// make a list of all possible triplets of lists of pixels
		var newImage = image.windowed(size = 3, step = 1).map { curTripleOfRows ->
			// in the next triple, we break each line into triples of pixels,
			// then we make triples of triples of pixels from them
			val tripleOfTriples = curTripleOfRows.map { row ->
				row.windowed(size = 3, step = 1)
			}
			return@map tripleOfTriples[0] zip tripleOfTriples[1] zip tripleOfTriples[2]
			// the result is rows from 3x3 matrices(windows) in which we need to change the central pixel
		}.map { rowOfWindows ->
			// consider each a 3x3 matrix separately
			val curImageRow = rowOfWindows.map { curWindow ->
				// unpack the 3x3 matrix into a convenient format
				val listOfWindow = curWindow.first.first + curWindow.first.second + curWindow.second
				// through the previously defined operations of addition,
				// multiplication and division of pixels, we obtain the value of the central pixel of the 3x3 matrix
				return@map abs(((listOfWindow zip matrix).map { (a, b) -> a * b }).sum() / divider)
			}
			// fill in the extreme pixels of each row of pixels, since they cannot be the center of a 3x3 matrix
			return@map listOf(curImageRow[0]) + curImageRow + listOf(curImageRow[curImageRow.size - 1])
		}
		// we also duplicate the first and last row of the matrix of pixels,
		// because the pixels on them cannot be the centers of a 3x3 matrix
		image = listOf(newImage[0]) + newImage + listOf(newImage[newImage.size - 1])
	}

	private fun applyMedianFilter() {
		var newImage = image.windowed(size = 3, step = 1).map { curTripleOfRows ->
			// in the next triple, we break each line into triples of pixels,
			// then we make triples of triples of pixels from them
			val tripleOfTriples = curTripleOfRows.map { row -> row.windowed(size = 3, step = 1) }
			return@map tripleOfTriples[0] zip tripleOfTriples[1] zip tripleOfTriples[2]
			// the result is rows from 3x3 matrices(windows) in which we need to change the central pixel
		}.map { rowOfWindows ->
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
			// fill in the extreme pixels of each row of pixels, since they cannot be the center of a 3x3 matrix
			return@map listOf(curImageRow[0]) + curImageRow + listOf(curImageRow[curImageRow.size - 1])
		}
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

	fun applyFilter(filterName: String) {
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
			else -> {
				println("Invalid filter name entered. Available filters: gray, sobelX, sobelY, gauss, median")
				inputPrompt()
				exitProcess(0)
			}
		}
	}

	fun equals(element: Bmp): Boolean {
		return (bmpFileHeader == element.bmpFileHeader &&
				bmpInfoHeader == element.bmpInfoHeader &&
				image == element.image)
	}
}

