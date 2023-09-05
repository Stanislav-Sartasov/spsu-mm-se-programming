package casino.lib.card

enum class CardSuite {
    SPADES,
    HEARTS,
    DIAMONDS,
    CLUBS;

    override fun toString(): String = when (this) {
        SPADES -> "\u2660"
        HEARTS -> "\u2661"
        DIAMONDS -> "\u2662"
        CLUBS -> "\u2663"
    }
}
