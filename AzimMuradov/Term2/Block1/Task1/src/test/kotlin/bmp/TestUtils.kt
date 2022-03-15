package bmp

import bmp.lib.*
import java.io.File

internal object TestUtils {

    val BMP24 = bmp(bitsPerPixel = 24)

    val BMP32 = bmp(bitsPerPixel = 32)

    private val SEP = File.separatorChar

    val TEST_RES_PATH = "src${SEP}test${SEP}resources${SEP}"


    private fun bmp(bitsPerPixel: Int): Bmp {
        val width = 4
        val height = 4
        val bytesPerPixel = bitsPerPixel shr 3
        // No need for padding because width is equal to 4
        val sizeOfBitmap = (bytesPerPixel * width * height).toUInt()
        return Bmp(
            fileHeader = BmpFileHeader(
                fileType = Bmp.FILE_TYPE,
                fileSize = 54u + sizeOfBitmap,
                reserved1 = Bmp.RESERVED_1,
                reserved2 = Bmp.RESERVED_2,
                bitmapOffset = 54u
            ),
            infoHeader = BmpInfoHeader(
                size = 40u,
                width = width,
                height = height,
                planes = Bmp.PLANES,
                bitsPerPixel = bitsPerPixel.toUShort(),
                compression = Bmp.NO_COMPRESSION,
                sizeOfBitmap = sizeOfBitmap,
                horzResolution = Bmp.DEFAULT_HORZ_RESOLUTION,
                vertResolution = Bmp.DEFAULT_VERT_RESOLUTION,
                colorsUsed = Bmp.NO_PALETTE_COLORS_USED,
                colorsImportant = Bmp.NO_PALETTE_COLORS_IMPORTANT
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
