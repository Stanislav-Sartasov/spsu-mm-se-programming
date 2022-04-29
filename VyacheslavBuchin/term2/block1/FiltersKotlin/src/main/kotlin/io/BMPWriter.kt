package io

import bmp.BMPFile
import java.io.OutputStream
import java.nio.ByteBuffer
import java.nio.ByteOrder

class BMPWriter(private val outputStream: OutputStream) {

	fun writeBMP(file: BMPFile) {
		outputStream.apply {
			write(file.signatureFirst.code)
			write(file.signatureSecond.code)
			writeInt(file.fileSize)
			writeInt(file.reserved)
			writeInt(file.dataOffset)
			writeInt(file.size)
			writeInt(file.width)
			writeInt(file.height)

			writeShort(file.planes)
			writeShort(file.bitsPerPixel)

			writeInt(file.compression)
			writeInt(file.imageSize)
			writeInt(file.xPixelsPerM)
			writeInt(file.yPixelsPerM)
			writeInt(file.colorsUsed)
			writeInt(file.colorsImportant)
		}
		file.colorMap.forEach { row ->
			row.forEach { color ->
				outputStream.apply {
					write(color.red)
					write(color.green)
					write(color.blue)
					if (file.bitsPerPixel.toInt() == 32)
						write(color.alpha)
				}
			}
			file.apply {
				val gapBytes = (4 - width * (bitsPerPixel / 8) % 4) % 4
				for (i in 1..gapBytes)
					outputStream.write(0)
			}
		}
	}

	private fun OutputStream.writeInt(value: Int) {
		this.write(
			ByteBuffer
			.allocate(4)
			.order(ByteOrder.LITTLE_ENDIAN)
			.putInt(value)
			.array()
		)
	}

	private fun OutputStream.writeShort(value: Short) {
		this.write(
			ByteBuffer
				.allocate(2)
				.order(ByteOrder.LITTLE_ENDIAN)
				.putShort(value)
				.array()
		)
	}


}