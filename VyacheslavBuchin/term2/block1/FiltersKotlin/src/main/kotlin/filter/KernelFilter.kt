package filter

import bmp.BMPFile
import bmp.Color

class KernelFilter(private val kernel: Array<Array<Double>>) : Filter {

	override fun applyTo(image: BMPFile) {
		val gapY = kernel.size / 2
		val gapX = kernel[0].size / 2

		val colorMap = image.colorMap
		val oldColorMap = Array(colorMap.size) { y ->
			Array(colorMap[y].size) { x ->
				colorMap[y][x]
			}
		}

		for (y in gapY until colorMap.size - gapY) {
			for (x in gapX until colorMap[y].size - gapX) {
				colorMap[y][x] = oldColorMap.modifiedPixel(y, x)
			}
		}
	}

	private fun Array<Array<Color>>.modifiedPixel(pixelY: Int, pixelX: Int): Color {
		val offsetY = kernel.size / 2
		val offsetX = kernel[0].size / 2
		val newRGB = doubleArrayOf(.0, .0, .0)
		for (y in kernel.indices) {
			for (x in kernel[y].indices) {
				val oldRGB = this[pixelY + y - offsetY][pixelX + x - offsetX].rgb()
				for (component in newRGB.indices) {
					newRGB[component] += oldRGB[component] * kernel[y][x]
				}
			}
		}

		return Color(
			newRGB[0].toInt().coerceAtLeast(0).coerceAtMost(255),
			newRGB[1].toInt().coerceAtLeast(0).coerceAtMost(255),
			newRGB[2].toInt().coerceAtLeast(0).coerceAtMost(255),
			this[pixelY][pixelX].alpha
		)
	}
}