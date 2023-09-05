package bmp.lib

@JvmInline
value class ValidatedBmp private constructor(val bmp: Bmp) {

    init {
        val fileType = bmp.fileHeader.fileType
        val bitsPerPixel = bmp.infoHeader.bitsPerPixel
        require(fileType == Bmp.FILE_TYPE && bitsPerPixel in ValidBmpBitsPerPixel.values().map { it.bpp }) {
            "Wrong BMP definition"
        }
    }

    enum class ValidBmpBitsPerPixel(val bpp: UShort) {
        BPP_32(32.toUShort()),
        BPP_24(24.toUShort()),
    }

    companion object {

        fun Bmp.validated(): ValidatedBmp = ValidatedBmp(bmp = this)
    }
}
