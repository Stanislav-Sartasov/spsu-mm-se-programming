package bmp.lib.filters

import bmp.lib.*

object Median3Filter : ImgFilter {
    override fun filter(rawImg: RawImg) = rawImg.filterWithWindow(KERNEL_SIZE) { window ->
        window.map(Color::toList).transpose().map {
            it.sorted()[KERNEL_SIZE * KERNEL_SIZE / 2]
        }.toColor()
    }

    private const val KERNEL_SIZE = 3
}
