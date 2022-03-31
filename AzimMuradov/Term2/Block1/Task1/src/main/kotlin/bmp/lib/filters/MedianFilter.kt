package bmp.lib.filters

import bmp.lib.*

class MedianFilter(val size: Int) : ImgFilter {

    override fun filter(rawImg: RawImg) = rawImg.filterWithWindow(size) { window ->
        window.map(Color::toList).transpose().map {
            it.sorted()[size * size / 2]
        }.toColor()
    }
}
