package bmp.lib

fun interface ImgFilter {

    fun filter(rawImg: RawImg): RawImg
}


fun RawImg.filter(imgFilter: ImgFilter): RawImg = imgFilter.filter(rawImg = this)

inline fun RawImg.filterWithWindow(windowSize: Int, filterBody: (List<Color>) -> Color): RawImg {
    val filteredPixels = pixels.map { it.toMutableList() }

    val pad = windowSize / 2
    for (y in pad until height - pad) {
        for (x in pad until width - pad) {
            val window = mutableListOf<Color>()
            for (row in y - pad..y + pad) {
                for (col in x - pad..x + pad) {
                    window += pixels[row][col]
                }
            }
            filteredPixels[y][x] = filterBody(window)
        }
    }

    return copy(pixels = filteredPixels)
}

fun RawImg.convolve(kernel: Kernel, postProcessing: (Double) -> Double = { it }): RawImg =
    filterWithWindow(windowSize = kernel.size) { window ->
        window.map(Color::toList).transpose().map { pixels ->
            (pixels.map(UByte::toDouble) scalarMultiplication kernel.elements)
                .run(postProcessing)
                .toInt()
                .coerceIn(range = 0..255)
                .toUByte()
        }.toColor()
    }


// Utilities

fun <T> List<List<T>>.transpose(): List<List<T>> {
    if (isEmpty()) return this

    val firstRow = first()

    require(firstRow.isNotEmpty() && all { row -> row.size == firstRow.size }) {
        "This two-dimensional list is not a valid matrix, so it cannot be transposed"
    }

    val firstElement = firstRow.first()
    val transposed = List(firstRow.size) { MutableList(size) { firstElement } }
    for (i in indices) {
        for (j in first().indices) {
            transposed[j][i] = this[i][j]
        }
    }
    return transposed
}

private infix fun List<Double>.scalarMultiplication(other: List<Double>): Double =
    (this zip other).sumOf { (a, b) -> a * b }
