package bmp.lib

data class Color(
    val b: UByte,
    val g: UByte,
    val r: UByte,
)


fun Color.toList(): List<UByte> = listOf(b, g, r)

fun List<UByte>.toColor(): Color = Color(get(0), get(1), get(2))
