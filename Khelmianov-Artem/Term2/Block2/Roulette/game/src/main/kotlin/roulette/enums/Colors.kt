package roulette.enums

enum class Colors {
    RED,
    BLACK,
    ZERO;

    companion object{
        private val reds = listOf(1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36)
        fun fromInt(number:Int) = if (number == 0) ZERO else if (reds.contains(number)) RED else BLACK
    }
}
