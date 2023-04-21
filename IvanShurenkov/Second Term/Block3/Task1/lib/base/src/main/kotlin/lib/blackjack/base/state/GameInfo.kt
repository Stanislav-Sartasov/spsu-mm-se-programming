package lib.blackjack.base.state

import lib.blackjack.base.Card

class GameInfo(_rangeBet: UIntRange, _cntDecks: UInt) {
    var cntDecks: UInt
        private set
    var rangeBet: UIntRange
        private set
    var dealt: List<Card> = emptyList()
        private set
    var croupierCard: Card = Card()

    init {
        rangeBet = _rangeBet
        cntDecks = _cntDecks
    }

    fun addCard(card: Card) {
        dealt = dealt + card
    }

    fun clearDealtCards() {
        dealt = emptyList()
    }
}
