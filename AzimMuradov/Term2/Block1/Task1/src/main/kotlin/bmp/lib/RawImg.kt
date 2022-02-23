package bmp.lib

data class RawImg(
    val width: Int,
    val height: Int,
    val pixels: List<List<Color>>,
)
