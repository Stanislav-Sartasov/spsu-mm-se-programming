package bmp.lib

import bmp.lib.ValidatedBmp.Companion.validated
import java.io.File
import java.io.RandomAccessFile
import java.nio.*
import java.nio.channels.FileChannel

object BmpIO {

    fun readBmp(path: String): ValidatedBmp {
        RandomAccessFile(path, "r").use { file ->
            val channel = file.channel
            val buffer: MappedByteBuffer = channel.map(FileChannel.MapMode.READ_ONLY, 0, file.length()).apply {
                order(ByteOrder.LITTLE_ENDIAN)
            }

            check(file.length() >= 14) { "Wrong BMP definition" }

            val fileHeader = BmpFileHeader(
                fileType = buffer.short.toUShort(),
                fileSize = buffer.int.toUInt(),
                reserved1 = buffer.short.toUShort(),
                reserved2 = buffer.short.toUShort(),
                bitmapOffset = buffer.int.toUInt(),
            )

            check(file.length().toUInt() >= fileHeader.fileSize) { "Wrong BMP definition" }

            val infoHeader = BmpInfoHeader(
                size = buffer.int.toUInt(),
                width = buffer.int,
                height = buffer.int,
                planes = buffer.short.toUShort(),
                bitsPerPixel = buffer.short.toUShort(),
                compression = buffer.int.toUInt(),
                sizeOfBitmap = buffer.int.toUInt(),
                horzResolution = buffer.int,
                vertResolution = buffer.int,
                colorsUsed = buffer.int.toUInt(),
                colorsImportant = buffer.int.toUInt(),
            )

            buffer.position(fileHeader.bitmapOffset.toInt())

            val bytesPerPixel = infoHeader.bitsPerPixel.toInt() shr 3
            val rowWidth = infoHeader.width * bytesPerPixel + (4 - (infoHeader.width * bytesPerPixel) % 4) % 4
            val pixels = ByteArray(rowWidth * infoHeader.height)
                .apply(buffer::get)
                .asSequence()
                .map(Byte::toUByte)
                .chunked(rowWidth)
                .map {
                    it.windowed(
                        size = bytesPerPixel, step = bytesPerPixel,
                        transform = List<UByte>::toColor
                    )
                }
                .toList()

            return Bmp(fileHeader, infoHeader, pixels).validated()
        }
    }

    fun writeBmp(path: String, validatedBmp: ValidatedBmp) {
        val (fileHeader, infoHeader, pixels) = validatedBmp.bmp

        File(path).outputStream().use { fileOutputStream ->
            val buffer = ByteBuffer.allocate(fileHeader.fileSize.toInt()).apply {
                order(ByteOrder.LITTLE_ENDIAN)
            }

            with(fileHeader) {
                buffer.putShort(fileType.toShort())
                buffer.putInt(fileSize.toInt())
                buffer.putShort(reserved1.toShort())
                buffer.putShort(reserved2.toShort())
                buffer.putInt(bitmapOffset.toInt())
            }

            with(infoHeader) {
                buffer.putInt(size.toInt())
                buffer.putInt(width)
                buffer.putInt(height)
                buffer.putShort(planes.toShort())
                buffer.putShort(bitsPerPixel.toShort())
                buffer.putInt(compression.toInt())
                buffer.putInt(sizeOfBitmap.toInt())
                buffer.putInt(horzResolution)
                buffer.putInt(vertResolution)
                buffer.putInt(colorsUsed.toInt())
                buffer.putInt(colorsImportant.toInt())
            }

            val array = if (infoHeader.bitsPerPixel == 24.toUShort()) {
                pixels.flatMap {
                    it.flatMap(Color::toList) + List(size = it.size * 3 % 4) { 0u }
                }.map(UByte::toByte).toByteArray()
            } else {
                pixels.flatMap {
                    it.flatMap { (b, g, r) -> listOf(b, g, r, 0u) }
                }.map(UByte::toByte).toByteArray()
            }
            buffer.put(array)

            fileOutputStream.write(buffer.array())
        }
    }
}
