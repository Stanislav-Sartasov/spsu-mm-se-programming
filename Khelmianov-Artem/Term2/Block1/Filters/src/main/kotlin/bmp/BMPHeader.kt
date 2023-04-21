package bmp


data class BMPHeader(
    val type: Short,
    val fileSize: Int,
    val reserved: Int,
    val offset: Int,

    val headerSize: Int,            /* Header size in bytes      */
    val width: Int,                 /* Width and height of image */
    val height: Int,
    val planes: Short,              /* Number of colour planes   */
    val bitsPerPixel: Short,        /* Bits per pixel            */
    val compression: Int,           /* Compression type          */
    val imageSize: Int,             /* Image size in bytes       */
    val xResolution: Int,           /* Pixels per meter          */
    val yResolution: Int,
    val numColors: Int,             /* Number of colours         */
    val numImportantColors: Int     /* Important colours         */
) {
    val isValid = type.toInt() == 19778 &&
            reserved == 0 &&
            planes.toInt() == 1 &&
            compression == 0 &&
            bitsPerPixel.toInt() in arrayOf(24, 32)
}
