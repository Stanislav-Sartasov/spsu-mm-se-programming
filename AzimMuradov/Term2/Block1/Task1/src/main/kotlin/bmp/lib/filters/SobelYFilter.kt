package bmp.lib.filters

import bmp.lib.*
import kotlin.math.abs

object SobelYFilter : ImgFilter {
    override fun filter(rawImg: RawImg) = rawImg.convolve(KERNEL, ::abs)

    private val KERNEL = Kernel.fromMatrix(
        squareMatrix = listOf(
            -1, -2, -1,
            0, 0, 0,
            1, 2, 1
        ).chunked(size = 3)
    )
}
