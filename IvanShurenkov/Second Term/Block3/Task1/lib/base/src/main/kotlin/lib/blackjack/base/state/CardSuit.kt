package lib.blackjack.base.state

enum class CardSuit {
    HEARTS,
    TILES,
    CLOVERS,
    PIKES;

    override fun toString(): String {
        return when (this) {
            HEARTS -> "♥"
            TILES -> "♦"
            CLOVERS -> "♣"
            PIKES -> "♠"
        }
    }
}
