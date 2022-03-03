package libs

import java.security.InvalidParameterException

data class RGBColor(var red: Int, var green: Int, var blue: Int) {
    fun normalize() {
        red = normalize(red)
        green = normalize(green)
        blue = normalize(blue)
    }

    fun normalized(): RGBColor = this.apply { normalize() }

    private fun normalize(value: Int) = if (value < 0) 0
    else if (value > 255) 255
    else value

    operator fun get(index: Int): Int = when (index) {
        0 -> this.red
        1 -> this.green
        2 -> this.blue
        else -> throw InvalidParameterException()
    }

    operator fun set(index: Int, value: Int) = when (index) {
        0 -> this.red = value
        1 -> this.green = value
        2 -> this.blue = value
        else -> {}
    }

}
