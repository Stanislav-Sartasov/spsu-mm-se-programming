package filters

import BMPFile

class SobelFilter : Filter() {
    /*
    int gx_sobel_matrix[3][3] = {{-1, 0, 1},
								 {-2, 0, 2},
								 {-1, 0, 1}};
	int gy_sobel_matrix[3][3] = {{-1, -2, -1},
								 {0,  0,  0},
								 {1,  2,  1}};
    */

    private val gxMatrix: Array<Array<Int>> = arrayOf(
        arrayOf(-1, 0, 1),
        arrayOf(-2, 0, 2),
        arrayOf(-1, 0, 1)
    )
    private val gyMatrix: Array<Array<Int>> = arrayOf(
        arrayOf(-1, -2, -1),
        arrayOf(0, 0, 0),
        arrayOf(1, 2, 1)
    )

    fun applyFilter(bmpFile: BMPFile, type: String) {
        GrayFilter().applyFilter(bmpFile)

        val step: Int = (3 + (bmpFile.bitCnt == 32).toInt())
        var ansImage: Array<Array<Short>> = Array(bmpFile.height) { Array(bmpFile.row) { 0 } }

        for (i in 1 until bmpFile.height - 1) {
            for (j in step until bmpFile.row - step step step) {
                var gx: Int = 0
                var gy: Int = 0
                for (q in i - 1..i + 1) {
                    for (w in j - step..j + step step step) {
                        gx += bmpFile.image[q][w] * gxMatrix[q - i + 1][(w - j + step) / step]
                        gy += bmpFile.image[q][w] * gyMatrix[q - i + 1][(w - j + step) / step]
                    }
                }
                var clr: Short = 0
                if (type == "x" && gx * gx > 128 * 128)
                    clr = 255
                else if (type == "y" && gy * gy > 128 * 128)
                    clr = 255
                else if (type == "xy" && (gx * gx) + (gy * gy) > 60 * 60)
                    clr = 255
                ansImage[i][j] = clr
            }
        }
        for (i in 1 until bmpFile.height - 1) {
            for (j in step until bmpFile.row - step step step) {
                for (q in 0 until step) {
                    bmpFile.image[i][j + q] = ansImage[i][j]
                }
            }
        }
    }
}