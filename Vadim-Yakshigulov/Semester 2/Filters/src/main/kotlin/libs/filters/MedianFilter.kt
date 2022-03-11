package libs.filters
import libs.ImageFilter
import libs.RGBColor

object MedianFilter : ImageFilter(3) {
    override fun applyKernel(neighbourPixels: Array<Array<RGBColor>>): RGBColor {
        val resultPixel = RGBColor(0, 0, 0)
        for (colorIndex in 0..2) {
            resultPixel[colorIndex] = neighbourPixels.flatten().sortedBy { it[colorIndex] }[size * size / 2][colorIndex]
        }

        return resultPixel.normalized()
    }
}