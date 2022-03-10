package libs.filters
import libs.ImageFilter
import libs.RGBColor

object GaussianFilter3x3 : ImageFilter(3) {
    override fun applyKernel(neighbourPixels: Array<Array<RGBColor>>): RGBColor {
        val resultPixel = RGBColor(0, 0, 0)
        val kernel = arrayOf(
            arrayOf(1, 2, 1),
            arrayOf(2, 4, 2),
            arrayOf(1, 2, 1)
        )
        for (colorIndex in 0..2) {
            for (x in 0 until size)
                for (y in 0 until size)
                    resultPixel[colorIndex] += neighbourPixels[x][y][colorIndex] * kernel[x][y]
            resultPixel[colorIndex] = (resultPixel[colorIndex] / 16.0).toInt()
        }
        return resultPixel.normalized()
    }
}