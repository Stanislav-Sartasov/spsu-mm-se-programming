package lib.blackjack.base.state

enum class CardRank {
    TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE;

    override fun toString(): String {
        return when (this) {
            TWO -> "2"
            THREE -> "3"
            FOUR -> "4"
            FIVE -> "5"
            SIX -> "6"
            SEVEN -> "7"
            EIGHT -> "8"
            NINE -> "9"
            TEN -> "10"
            JACK -> "J"
            QUEEN -> "Q"
            KING -> "K"
            ACE -> "A"
        }
    }

    fun toInt(): Int {
        return when (this) {
            TWO -> 2
            THREE -> 3
            FOUR -> 4
            FIVE -> 5
            SIX -> 6
            SEVEN -> 7
            EIGHT -> 8
            NINE -> 9
            TEN -> 10
            JACK -> 10
            QUEEN -> 10
            KING -> 10
            ACE -> 11
        }
    }
}
