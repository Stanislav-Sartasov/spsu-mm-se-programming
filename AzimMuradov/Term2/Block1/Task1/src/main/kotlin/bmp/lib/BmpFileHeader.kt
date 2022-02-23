package bmp.lib

data class BmpFileHeader(
    val fileType: UShort,
    val fileSize: UInt,
    val reserved1: UShort,
    val reserved2: UShort,
    val bitmapOffset: UInt,
)
