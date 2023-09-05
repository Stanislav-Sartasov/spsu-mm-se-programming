package libs.filters
import libs.ImageFilter
import libs.RGBColor

object GaussianFilter5x5 : ImageFilter(5) {
    public override fun applyKernel(neighbourPixels: Array<Array<RGBColor>>): RGBColor {
        val resultPixel = RGBColor(0, 0, 0)
        val kernel = arrayOf(
            arrayOf(1, 4, 7, 4, 1),
            arrayOf(4, 16, 26, 16, 4),
            arrayOf(7, 26, 41, 26, 7),
            arrayOf(4, 16, 26, 16, 4),
            arrayOf(1, 4, 7, 4, 1),
        )
        for (colorIndex in 0..2) {
            for (x in 0 until size)
                for (y in 0 until size)
                    resultPixel[colorIndex] += neighbourPixels[x][y][colorIndex] * kernel[x][y]
            resultPixel[colorIndex] = (resultPixel[colorIndex] / 273.0).toInt()
        }
        return resultPixel.normalized()
    }
}