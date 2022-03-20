package bmp.lib

fun interface ImgFilter {

    fun filter(rawImg: RawImg): RawImg
}


fun RawImg.filter(imgFilter: ImgFilter): RawImg = imgFilter.filter(rawImg = this)

inline fun RawImg.filterWithWindow(windowSize: Int, filterBody: (List<Color>) -> Color): RawImg {
    val filteredPixels = pixels.map { it.toMutableList() }

    val pad1 = windowSize / 2
    val pad2 = (windowSize - 1) / 2
    for (y in 0 until height) {
        for (x in 0 until width) {
            filteredPixels[y][x] = buildList {
                for (i in y - pad1..y + pad2) {
                    for (j in x - pad1..x + pad2) {
                        val validI = i.coerceIn(range = 0 until height)
                        val validJ = j.coerceIn(range = 0 until width)
                        add(pixels[validI][validJ])
                    }
                }
            }.run(filterBody)
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
