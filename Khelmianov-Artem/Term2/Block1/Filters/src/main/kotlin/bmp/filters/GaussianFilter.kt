package bmp.bmp.filters

import bmp.bmp.AKernelFilter

class GaussianFilter constructor(
    override var radius: Int,
) : AKernelFilter() {
    override var kernel: List<Float> = calculateGaussianKernel() //.also { println(it) }
    private fun binomials(n: Int = radius * 2, k: Int): Int {
        return if ((n == k) || (k == 0))
            1
        else
            binomials(n - 1, k) + binomials(n - 1, k - 1)
    }

    private fun calculateGaussianKernel(): List<Float> {
        // Вычисление приближенного значения матрицы Гаусса размера 2*radius + 1 по биноминальным коэффициентам
        // Г ~ 1/s * (A^T * A), где А - матрица-строка с биноминальными коэффициентами, s - сумма всех элементов произведения матриц
        return buildList {
            for (i in 0 until radius * 2 + 1) {
                add(binomials(k = i).toFloat())
            }
        }.toMutableList().apply {
            val row = 1..this.lastIndex
            val col = this.indices
            for (i in row) {
                for (j in col) {
                    add(this[i] * this[j])
                }
            }
        }.map { f -> (f / (1 shl (radius * 4))) }
    }
}
