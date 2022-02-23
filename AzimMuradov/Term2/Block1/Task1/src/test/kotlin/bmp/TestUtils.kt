package bmp

import bmp.lib.*

object TestUtils {
    val BMP24 = bmp(bitsPerPixel = 24)

    val BMP32 = bmp(bitsPerPixel = 32)


    private fun bmp(bitsPerPixel: Int): Bmp {
        val width = 4
        val height = 4
        val bytesPerPixel = bitsPerPixel shr 3
        // No need for padding because width is equal to 4
        val sizeOfBitmap = (bytesPerPixel * width * height).toUInt()
        return Bmp(
            fileHeader = BmpFileHeader(
                fileType = 19778.toUShort(),
                fileSize = 54u + sizeOfBitmap,
                reserved1 = 0.toUShort(),
                reserved2 = 0.toUShort(),
                bitmapOffset = 54u
            ),
            infoHeader = BmpInfoHeader(
                size = 40u,
                width = width,
                height = height,
                planes = 1.toUShort(),
                bitsPerPixel = bitsPerPixel.toUShort(),
                compression = 0u,
                sizeOfBitmap = sizeOfBitmap,
                horzResolution = 2834,
                vertResolution = 2834,
                colorsUsed = 0u,
                colorsImportant = 0u
            ),
            pixels = List(size = height) { y ->
                List(size = width) { x ->
                    Color(
                        b = (y * 3).toUByte(),
                        g = (x * 3).toUByte(),
                        r = ((y + x) * 3).toUByte()
                    )
                }
            }
        )
    }
}
