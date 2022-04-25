package bmp

data class Color(
	val red: Int,
	val green: Int,
	val blue: Int,
	val alpha: Int
) {

	fun rgb() = intArrayOf(red, green, blue)


}