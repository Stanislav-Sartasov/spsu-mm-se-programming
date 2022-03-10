package libs.filters

import libs.ImageFilter
import libs.RGBColor

object GreyscaleFilter : ImageFilter(1) {
    override fun applyKernel(neighbourPixels: Array<Array<RGBColor>>): RGBColor {
        var averageColor = 0
        for (colorIndex in 0..2)
            averageColor += neighbourPixels[size / 2][size / 2][colorIndex]
        averageColor = (averageColor / 3.0).toInt()
        return RGBColor(averageColor, averageColor, averageColor).normalized()
    }
}