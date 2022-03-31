package blackjack.classes

import blackjack.interfaces.ICard
import blackjack.interfaces.IShoe

class Shoe(override val numberOfDecks: Int) : IShoe {
    private val _cards: MutableList<ICard> = mutableListOf()
    init {
        renew()
    }
    override val cards: List<ICard>
        get() = _cards

    override fun dealCard(isFaceUp: Boolean): ICard = _cards.removeLast().apply { this.isFaceUp = isFaceUp}

    override fun renew() {
        _cards.clear()
        for (n in 0 until numberOfDecks) {
            _cards += Deck().cards
        }
        shuffle()
    }

    override fun shuffle() = _cards.shuffle()
}