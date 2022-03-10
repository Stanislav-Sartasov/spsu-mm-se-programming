package filters

import BMPFile

class MedianFilter : Filter() {
    fun applyFilter(bmpFile: BMPFile) {
        val step: Int = (3 + (bmpFile.bitCnt == 32).toInt())
        for (clrNum in 0..2) {
            for (i in 1 until bmpFile.height - 1) {
                for (j in step + clrNum until bmpFile.row - step step step) {
                    var index: Int = 0
                    var arr: Array<Short> = Array(9) { 255 }
                    for (q in i - 1..i + 1) {
                        for (w in j - step..j + step step step) {
                            if (0 <= w && w < bmpFile.row && 0 <= q && q < bmpFile.height) {
                                arr[index] = bmpFile.image[q][w]
                                index++
                            }
                        }
                    }
                    arr.sort()
                    bmpFile.image[i][j] = arr[index / 2]
                }
            }
        }
    }
}