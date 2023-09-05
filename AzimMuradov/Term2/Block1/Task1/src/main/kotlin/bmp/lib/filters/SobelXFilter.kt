package bmp.lib.filters

import bmp.lib.*
import kotlin.math.abs

object SobelXFilter : ImgFilter {

    override fun filter(rawImg: RawImg) = rawImg.convolve(KERNEL, postProcessing = ::abs)

    private val KERNEL = Kernel.fromMatrix(
        squareMatrix = listOf(
            -1, 0, 1,
            -2, 0, 2,
            -1, 0, 1
        ).chunked(size = 3)
    )
}
