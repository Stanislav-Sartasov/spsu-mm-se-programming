package bmp.lib.filters

import bmp.lib.*

object Gauss3Filter : ImgFilter {

    override fun filter(rawImg: RawImg) = rawImg.convolve(KERNEL, postProcessing = { it / 16.0 })

    private val KERNEL = Kernel.fromMatrix(
        squareMatrix = listOf(
            1, 2, 1,
            2, 4, 2,
            1, 2, 1
        ).chunked(size = 3)
    )
}
