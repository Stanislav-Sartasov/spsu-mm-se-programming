package lib.blackjack.base

import lib.blackjack.base.state.CardRank
import lib.blackjack.base.state.CardSuit

class Deck(cntDecks: UInt) {
    private var deck: Array<Card> = emptyArray()
    private var pos: Int = -1

    init {
        for (i in 1..cntDecks.toInt()) {
            for (suit in CardSuit.values()) {
                for (rank in CardRank.values()) {
                    deck += Card(rank, suit)
                }
            }
        }
    }

    fun getCard(): Card {
        pos++
        return deck[pos]
    }

    fun remain(): Int {
        return deck.size - (pos + 1)
    }

    fun shaffle() {
        pos = -1
        deck.shuffle()
    }
}
