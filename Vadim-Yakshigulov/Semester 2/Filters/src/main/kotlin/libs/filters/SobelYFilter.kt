package libs.filters

import libs.ImageFilter
import libs.RGBColor
import kotlin.math.abs


object SobelYFilter : ImageFilter(3) {
    override fun applyKernel(neighbourPixels: Array<Array<RGBColor>>): RGBColor {
        val resultPixel = RGBColor(0, 0, 0)
        val kernel = arrayOf(
            arrayOf(-1, -2, -1),
            arrayOf(0, 0, 0),
            arrayOf(1, 2, 1)
        )
        for (colorIndex in 0..2) {
            for (x in 0 until size)
                for (y in 0 until size)
                    resultPixel[colorIndex] += neighbourPixels[x][y][colorIndex] * kernel[x][y]
            resultPixel[colorIndex] = abs(resultPixel[colorIndex])
        }

        return resultPixel.normalized()
    }
}

