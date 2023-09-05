package bmp

import bmp.bmp.IFilter
import java.io.File
import java.io.RandomAccessFile
import java.nio.ByteBuffer
import java.nio.ByteOrder
import java.nio.channels.FileChannel

typealias PixelArray = Array<Array<Pixel>>

class BMPImage(
    var header: BMPHeader,
    var data: PixelArray
) {
    companion object {
        fun open(path: String): BMPImage? {
            try {

                RandomAccessFile(path, "r").use { file ->
                    val buffer = file.channel.map(FileChannel.MapMode.READ_ONLY, 0, file.length())
                        .apply { order(ByteOrder.LITTLE_ENDIAN) }

                    val header = BMPHeader(
                        type = buffer.short,
                        fileSize = buffer.int,
                        reserved = buffer.int,
                        offset = buffer.int,
                        headerSize = buffer.int,
                        width = buffer.int,
                        height = buffer.int,
                        planes = buffer.short,
                        bitsPerPixel = buffer.short,
                        compression = buffer.int,
                        imageSize = buffer.int,
                        xResolution = buffer.int,
                        yResolution = buffer.int,
                        numColors = buffer.int,
                        numImportantColors = buffer.int
                    )

                    if (!header.isValid) {
                        return null
                    }

                    buffer.position(header.offset)
                    val data = Array(header.height) { Array(header.width) { Pixel() } }
                    val isAlpha = header.bitsPerPixel == 32.toShort()
                    val isPadding = header.bitsPerPixel == 24.toShort()
                    val padding = if (isPadding) (-3 * header.width) % 4 else 0

                    for (y in header.height - 1 downTo 0) {
                        for (x in 0 until header.width) {
                            data[y][x] = Pixel(
                                buffer.get().toUByte(),
                                buffer.get().toUByte(),
                                buffer.get().toUByte()
                            )
                            if (isAlpha) buffer.get()
                        }
                        if (isPadding) buffer.get(padding)
                    }

                    return BMPImage(header, data)
                }
            } catch (exception: Exception) {
                return null
            }
        }
    }

    fun save(path: String) {
        File(path).outputStream().use { file ->
            val buffer = ByteBuffer.allocate(header.fileSize).apply {
                order(ByteOrder.LITTLE_ENDIAN)
            }

            buffer.putShort(header.type)
            buffer.putInt(header.fileSize)
            buffer.putInt(header.reserved)
            buffer.putInt(header.offset)
            buffer.putInt(header.headerSize)
            buffer.putInt(header.width)
            buffer.putInt(header.height)
            buffer.putShort(header.planes)
            buffer.putShort(header.bitsPerPixel)
            buffer.putInt(header.compression)
            buffer.putInt(header.imageSize)
            buffer.putInt(header.xResolution)
            buffer.putInt(header.yResolution)
            buffer.putInt(header.numColors)
            buffer.putInt(header.numImportantColors)

            buffer.position(header.offset)
            val isAlpha = header.bitsPerPixel == 32.toShort()
            val isPadding = header.bitsPerPixel == 24.toShort()
            val padding = if (isPadding) (-3 * header.width) % 4 else 0

            for (y in header.height - 1 downTo 0) {
                for (x in 0 until header.width) {
                    buffer.put(data[y][x].b.toByte())
                    buffer.put(data[y][x].g.toByte())
                    buffer.put(data[y][x].r.toByte())
                    if (isAlpha) buffer.put(0)
                }
                if (isPadding) buffer.put(ByteArray(padding) { 0 })
            }
            file.write(buffer.array())
        }
    }

    fun applyFilter(filter: IFilter) = filter.apply(this)
}
