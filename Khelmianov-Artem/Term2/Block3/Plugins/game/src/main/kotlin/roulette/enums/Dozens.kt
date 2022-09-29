package roulette.enums

enum class Dozens {
    FIRST,
    SECOND,
    THIRD,
    ZERO;

    companion object {
        fun fromInt(number: Int) = when (number) {
            0 -> ZERO
            in 1..12 -> FIRST
            in 13..24 -> SECOND
            in 24..36 -> THIRD
            else -> throw IllegalArgumentException()
        }
    }
}