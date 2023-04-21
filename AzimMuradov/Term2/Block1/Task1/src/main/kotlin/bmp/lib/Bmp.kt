package bmp.lib

import bmp.lib.ValidatedBmp.ValidBmpBitsPerPixel

data class Bmp(
    val fileHeader: BmpFileHeader,
    val infoHeader: BmpInfoHeader,
    val pixels: List<List<Color>>,
) {

    enum class BitsPerPixel(val bytes: UShort) {
        BPP_8(bytes = 1u),
        BPP_16(bytes = 2u),
        BPP_24(bytes = 3u),
        BPP_32(bytes = 4u);

        val bits: UShort = (bytes.toInt() shl 3).toUShort()
    }

    companion object {

        const val FILE_TYPE: UShort = 19778u

        const val RESERVED_1: UShort = 0u

        const val RESERVED_2: UShort = 0u

        const val PLANES: UShort = 1u

        const val NO_COMPRESSION: UInt = 0u

        const val DEFAULT_HORZ_RESOLUTION: Int = 2834

        const val DEFAULT_VERT_RESOLUTION: Int = 2834

        const val NO_PALETTE_COLORS_USED: UInt = 0u

        const val NO_PALETTE_COLORS_IMPORTANT: UInt = 0u
    }
}


fun Bmp.toRawImg(): RawImg = RawImg(
    width = infoHeader.width,
    height = infoHeader.height,
    pixels = pixels
)

fun RawImg.toBmp(bitsPerPixel: ValidBmpBitsPerPixel): Bmp {
    val bytesPerPixel = bitsPerPixel.bpp.toInt() shr 3
    val sizeOfBitmap = ((bytesPerPixel * width + (4 - bytesPerPixel * width % 4) % 4) * height).toUInt()
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
            bitsPerPixel = bitsPerPixel.bpp,
            compression = Bmp.NO_COMPRESSION,
            sizeOfBitmap = sizeOfBitmap,
            horzResolution = Bmp.DEFAULT_HORZ_RESOLUTION,
            vertResolution = Bmp.DEFAULT_VERT_RESOLUTION,
            colorsUsed = Bmp.NO_PALETTE_COLORS_USED,
            colorsImportant = Bmp.NO_PALETTE_COLORS_IMPORTANT
        ),
        pixels = pixels
    )
}
