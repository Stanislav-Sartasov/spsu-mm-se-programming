package bmp.lib

data class BmpInfoHeader(
    val size: UInt,
    val width: Int,
    val height: Int,
    val planes: UShort,
    val bitsPerPixel: UShort,
    val compression: UInt,
    val sizeOfBitmap: UInt,
    val horzResolution: Int,
    val vertResolution: Int,
    val colorsUsed: UInt,
    val colorsImportant: UInt,
)
