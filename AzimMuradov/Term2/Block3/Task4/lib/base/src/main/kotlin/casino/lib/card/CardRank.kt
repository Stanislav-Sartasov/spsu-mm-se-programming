package casino.lib.card

enum class CardRank {
    ACE,
    TWO,
    THREE,
    FOUR,
    FIVE,
    SIX,
    SEVEN,
    EIGHT,
    NINE,
    TEN,
    JACK,
    QUEEN,
    KING;

    override fun toString(): String = when (this) {
        ACE -> "A"
        TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN -> (ordinal + 1).toString()
        JACK -> "J"
        QUEEN -> "Q"
        KING -> "K"
    }
}


fun CardRank.isNumeral(): Boolean = ordinal in 1..9

fun CardRank.isFace(): Boolean = ordinal in 10..12
