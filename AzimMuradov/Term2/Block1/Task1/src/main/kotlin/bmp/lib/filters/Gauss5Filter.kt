package bmp.lib.filters

import bmp.lib.*

object Gauss5Filter : ImgFilter {

    override fun filter(rawImg: RawImg) = rawImg.convolve(KERNEL, postProcessing = { it / 273.0 })

    private val KERNEL = Kernel.fromMatrix(
        squareMatrix = listOf(
            1, 4, 7, 4, 1,
            4, 16, 26, 16, 4,
            7, 26, 41, 26, 7,
            4, 16, 26, 16, 4,
            1, 4, 7, 4, 1
        ).chunked(size = 5)
    )
}
