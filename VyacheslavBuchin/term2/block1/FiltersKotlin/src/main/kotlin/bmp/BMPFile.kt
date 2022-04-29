package bmp

import java.nio.ByteBuffer
import java.nio.ByteOrder


data class BMPFile(
	val signatureFirst: Char,
	val signatureSecond: Char,

	val fileSize: Int,
	val reserved: Int,
	val dataOffset: Int,
	val size: Int,
	val width: Int,
	val height: Int,

	val planes: Short,
	val bitsPerPixel: Short,

	val compression: Int,
	val imageSize: Int,
	val xPixelsPerM: Int,
	val yPixelsPerM: Int,
	val colorsUsed: Int,
	val colorsImportant: Int,

	val colorMap: Array<Array<Color>>
) {
	companion object {
		fun from(byteArray: ByteArray): BMPFile {
			var index = 0
			val signatureFirst = byteArray[index++].toInt().toChar()
			val signatureSecond = byteArray[index++].toInt().toChar()
			val fileSize: Int = byteArray.littleEndianInt32From(index); index += 4
			val reserved: Int = byteArray.littleEndianInt32From(index); index += 4
			val dataOffset: Int = byteArray.littleEndianInt32From(index); index += 4
			val size: Int = byteArray.littleEndianInt32From(index); index += 4
			val width: Int = byteArray.littleEndianInt32From(index); index += 4
			val height: Int = byteArray.littleEndianInt32From(index); index += 4

			val planes: Short = byteArray.littleEndianInt16From(index); index += 2
			val bitsPerPixel: Short = byteArray.littleEndianInt16From(index); index += 2

			val compression = byteArray.littleEndianInt32From(index); index += 4
			val imageSize: Int = byteArray.littleEndianInt32From(index); index += 4
			val xPixelsPerM: Int = byteArray.littleEndianInt32From(index); index += 4
			val yPixelsPerM: Int = byteArray.littleEndianInt32From(index); index += 4
			val colorsUsed: Int = byteArray.littleEndianInt32From(index); index += 4
			val colorsImportant: Int = byteArray.littleEndianInt32From(index); index += 4

			if (signatureFirst != 'B' || signatureSecond != 'M' || (bitsPerPixel.toInt() != 24 && bitsPerPixel.toInt() != 32))
				informOfUnsupportedFile()


			val colorMap = Array(height) {
				val row = Array(width) {
					Color(
						byteArray[index++].toUByte().toInt(),
						byteArray[index++].toUByte().toInt(),
						byteArray[index++].toUByte().toInt(),
						if (bitsPerPixel.toInt() == 32) byteArray[index++].toUByte().toInt() else 255
					)
				}
				index += (4 - width * (bitsPerPixel / 8) % 4) % 4

				row
			}

			return BMPFile(
				signatureFirst,
				signatureSecond,
				fileSize,
				reserved,
				dataOffset,
				size,
				width,
				height,
				planes,
				bitsPerPixel,
				compression,
				imageSize,
				xPixelsPerM,
				yPixelsPerM,
				colorsUsed,
				colorsImportant,
				colorMap
			)
		}

		private fun ByteArray.littleEndianInt32From(index: Int) =
			ByteBuffer.wrap(this.copyOfRange(index, index + 4)).order(ByteOrder.LITTLE_ENDIAN).int

		private fun ByteArray.littleEndianInt16From(index: Int) =
			ByteBuffer.wrap(this.copyOfRange(index, index + 2)).order(ByteOrder.LITTLE_ENDIAN).short

		private fun informOfUnsupportedFile() {
			throw IllegalArgumentException("Unsupported file format: only 32-bit and 24-bit BMP files allowed")
		}
	}

	override fun equals(other: Any?): Boolean {
		if (this === other) return true
		if (javaClass != other?.javaClass) return false

		other as BMPFile

		if (signatureFirst != other.signatureFirst) return false
		if (signatureSecond != other.signatureSecond) return false
		if (fileSize != other.fileSize) return false
		if (reserved != other.reserved) return false
		if (dataOffset != other.dataOffset) return false
		if (size != other.size) return false
		if (width != other.width) return false
		if (height != other.height) return false
		if (planes != other.planes) return false
		if (bitsPerPixel != other.bitsPerPixel) return false
		if (compression != other.compression) return false
		if (imageSize != other.imageSize) return false
		if (xPixelsPerM != other.xPixelsPerM) return false
		if (yPixelsPerM != other.yPixelsPerM) return false
		if (colorsUsed != other.colorsUsed) return false
		if (colorsImportant != other.colorsImportant) return false
		if (!colorMap.contentDeepEquals(other.colorMap)) return false

		return true
	}

	override fun hashCode(): Int {
		var result = signatureFirst.hashCode()
		result = 31 * result + signatureSecond.hashCode()
		result = 31 * result + fileSize
		result = 31 * result + reserved
		result = 31 * result + dataOffset
		result = 31 * result + size
		result = 31 * result + width
		result = 31 * result + height
		result = 31 * result + planes
		result = 31 * result + bitsPerPixel
		result = 31 * result + compression
		result = 31 * result + imageSize
		result = 31 * result + xPixelsPerM
		result = 31 * result + yPixelsPerM
		result = 31 * result + colorsUsed
		result = 31 * result + colorsImportant
		result = 31 * result + colorMap.contentDeepHashCode()
		return result
	}


}

