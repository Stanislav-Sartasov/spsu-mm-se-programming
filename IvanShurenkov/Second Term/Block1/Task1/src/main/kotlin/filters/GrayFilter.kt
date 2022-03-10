package filters

import BMPFile

class GrayFilter : Filter() {
    private val redCoefficient: Double = 0.2126
    private val greenCoefficient: Double = 0.7152
    private val blueCoefficient: Double = 0.0722

    fun applyFilter(bmpFile: BMPFile) {
        for (i in 0 until bmpFile.height) {
            for (j in 0 until (bmpFile.row - 2) step (3 + (bmpFile.bitCnt == 32).toInt())) {
                val gray: Int = ((bmpFile.image[i][j] * redCoefficient +
                        bmpFile.image[i][j + 1] * greenCoefficient +
                        bmpFile.image[i][j + 2] * blueCoefficient).toInt() and 0xff)
                bmpFile.image[i][j] = gray.toShort()
                bmpFile.image[i][j + 1] = gray.toShort()
                bmpFile.image[i][j + 2] = gray.toShort()
            }
        }
    }
}