package lib.blackjack.base

import lib.blackjack.base.state.CardRank
import lib.blackjack.base.state.CardSuit

class Card(_rank: CardRank = CardRank.QUEEN, _suit: CardSuit = CardSuit.HEARTS) {
    val rank: CardRank
    val suit: CardSuit

    var count: Int
        private set
        get() {
            if (field in 2..10)
                return field
            return 11
        }

    init {
        count = _rank.toInt()
        rank = _rank
        suit = _suit
    }

    override fun toString(): String {
        return rank.toString() + suit.toString()
    }
}