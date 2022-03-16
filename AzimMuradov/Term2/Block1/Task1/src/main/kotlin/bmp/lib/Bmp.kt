package bmp.lib

import bmp.lib.ValidatedBmp.ValidBmpBitsPerPixel

data class Bmp(
    val fileHeader: BmpFileHeader,
    val infoHeader: BmpInfoHeader,
    val pixels: List<List<Color>>,
)


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
            bitsPerPixel = bitsPerPixel.bpp,
            compression = 0u,
            sizeOfBitmap = sizeOfBitmap,
            horzResolution = 2834,
            vertResolution = 2834,
            colorsUsed = 0u,
            colorsImportant = 0u
        ),
        pixels = pixels
    )
}
