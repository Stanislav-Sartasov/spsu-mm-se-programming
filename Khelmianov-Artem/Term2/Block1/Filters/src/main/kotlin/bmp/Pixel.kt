package bmp


data class Pixel(
    var b: UByte = 0u,
    var g: UByte = 0u,
    var r: UByte = 0u
) {
    operator fun get(i: Int) = when (i) {
        0 -> b
        1 -> g
        2 -> r
        else -> throw IllegalArgumentException()
    }

    operator fun set(i: Int, v: UByte) = when (i) {
        0 -> b = v
        1 -> g = v
        2 -> r = v
        else -> {}
    }
}
