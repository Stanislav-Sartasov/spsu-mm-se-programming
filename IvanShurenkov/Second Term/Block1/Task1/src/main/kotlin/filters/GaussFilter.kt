package filters

import BMPFile

class GaussFilter : Filter() {
    private val gaussMatrix: Array<Double> = arrayOf(0.147761, 0.118318, 0.0947416)

    fun applyFilter(bmpFile: BMPFile) {
        val step: Int = (3 + (bmpFile.bitCnt == 32).toInt())
        for (clrNum in 0..2) {
            for (i in 1 until bmpFile.height - 1) {
                for (j in step + clrNum until bmpFile.row - step step step) {
                    var clr: Double = 0.0
                    for (q in i - 1..i + 1) {
                        for (w in j - step..j + step step step) {
                            val gaussId: Int = (q != i).toInt() + (w != j).toInt()
                            clr += bmpFile.image[q][w] * gaussMatrix[gaussId]
                        }
                    }
                    bmpFile.image[i][j] = clr.toInt().toShort()
                }
            }
        }
    }
}