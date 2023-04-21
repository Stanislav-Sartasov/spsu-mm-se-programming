package bmp.lib

class Kernel private constructor(val elements: List<Double>, val size: Int) {

    companion object {

        fun fromMatrix(squareMatrix: List<List<Number>>): Kernel {
            val colSize = squareMatrix.size

            require(colSize != 0) {
                "Wrong kernel definition: matrix should not be empty"
            }
            require(squareMatrix.all { row -> row.size == colSize }) {
                "Wrong kernel definition: matrix should have the same number of rows and columns"
            }

            return Kernel(
                elements = squareMatrix.flatten().map(Number::toDouble),
                size = squareMatrix.size
            )
        }
    }
}
