package libs

abstract class ImageFilter(protected val size: Int) {
    private val cornerOffset
        get() = (size - 1) / 2

    protected abstract fun applyKernel(neighbourPixels: Array<Array<RGBColor>>): RGBColor

    fun applyFor(img: BmpImage) {
        val buffer = img.pixelData.deepCopy()
        for (pixelRowPos in cornerOffset until img.height - cornerOffset)
            for (pixelColPos in cornerOffset until img.width - cornerOffset) {

                img.pixelData[pixelRowPos][pixelColPos] =
                    applyKernel(getNeighbourPixels(pixelRowPos, pixelColPos, buffer))
            }
    }

    private fun getNeighbourPixels(
        originPixelRowPos: Int,
        originPixelColPos: Int,
        pixelData: Array<Array<RGBColor>>
    ): Array<Array<RGBColor>> {
        val neighbourPixels = Array(size) { Array(size) { RGBColor(0, 0, 0) } }

        for (rowOffset in -cornerOffset..cornerOffset)
            for (colOffset in -cornerOffset..cornerOffset)
                neighbourPixels[cornerOffset + rowOffset][cornerOffset + colOffset] =
                    pixelData[originPixelRowPos + rowOffset][originPixelColPos + colOffset]
        return neighbourPixels
    }
}

fun Array<Array<RGBColor>>.deepCopy() =
    this.map { it.map { it.copy() }.toTypedArray().clone() }.toTypedArray()

fun BmpImage.applyFilter(filter: ImageFilter) = filter.applyFor(this)

