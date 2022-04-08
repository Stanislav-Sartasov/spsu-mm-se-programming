package bmp.bmp

import bmp.BMPImage
import bmp.Pixel
import bmp.PixelArray
import kotlin.math.absoluteValue
import kotlin.math.pow

abstract class AKernelFilter : IFilter {
    protected abstract var radius: Int
    protected abstract var kernel: List<Float>

    override fun apply(image: BMPImage) {
        val newPixels = image.data.copy()

        for (y in 0 until image.header.height) {
            for (x in 0 until image.header.width) {
                newPixels[y][x] = calculateConvolution(kernel, getLocalPixels(image, x, y))
            }
        }
        image.data = newPixels
    }

    private fun calculateConvolution(kernel: List<Float>, pixels: List<Pixel>): Pixel {
        val conv = Pixel()
        for (color in 0..2) {
            conv[color] = (kernel zip pixels).fold(0.0) { acc, (coef, pix) ->
                acc + (coef * pix[color].toInt())
            }.toInt().absoluteValue.coerceIn(0..255).toUByte()
        }
        return conv
    }

    private fun getLocalPixels(image: BMPImage, x: Int, y: Int): List<Pixel> {
        val pixels = image.data
        return buildList((radius * 2 + 1.0).pow(2.0).toInt()) {
            for (i in -radius..radius) {
                for (j in -radius..radius) {
                    val xx = (x + j).coerceIn(0 until image.header.width)
                    val yy = (y + i).coerceIn(0 until image.header.height)
                    add(pixels[yy][xx])
                }
            }
        }
    }
}

fun PixelArray.copy(): PixelArray {
    return map { outer -> outer.map { inner -> inner.copy() }.toTypedArray() }.toTypedArray()
}
