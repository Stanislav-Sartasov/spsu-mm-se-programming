package bmp.lib.filters

import bmp.lib.*

object GrayScaleFilter : ImgFilter {

    override fun filter(rawImg: RawImg): RawImg = rawImg.copy(
        pixels = rawImg.pixels.map {
            it.map { (b, g, r) ->
                val m = ((b + g + r) / 3u).toUByte()
                Color(b = m, g = m, r = m)
            }
        }
    )
}
