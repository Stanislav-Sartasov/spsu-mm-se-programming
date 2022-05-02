package roulette.enums

enum class Parity {
    EVEN,
    ODD,
    ZERO;

    companion object {
        fun fromInt(number: Int) = if (number == 0) ZERO else if (number.mod(2) == 0) EVEN else ODD
    }
}