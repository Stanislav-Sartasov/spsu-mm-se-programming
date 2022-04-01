package lib.blackjack.base

class GameInfo(_rangeBet: UIntRange, _cntDecks: UInt) {
    var cntDecks: UInt
        private set
    var rangeBet: UIntRange
        private set
    var dealt: List<Card> = emptyList()
        private set
    var croupierCard: Card = Card(-1, "")

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
