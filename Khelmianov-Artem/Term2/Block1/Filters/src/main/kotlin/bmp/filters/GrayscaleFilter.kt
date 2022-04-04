package bmp.bmp.filters

import bmp.BMPImage
import bmp.bmp.IFilter

object GrayscaleFilter : IFilter {
    override fun apply(image: BMPImage) {
        image.data.map {
            it.map { pixel ->
                val mean = (pixel.b + pixel.g + pixel.r).div(3u).toUByte()
                pixel.b = mean
                pixel.g = mean
                pixel.r = mean
            }
        }
    }

}