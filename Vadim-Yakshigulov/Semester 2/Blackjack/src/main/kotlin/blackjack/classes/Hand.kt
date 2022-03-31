package blackjack.classes

import blackjack.interfaces.IBetPlaced
import blackjack.interfaces.ICard
import blackjack.interfaces.IHand

class Hand : IHand, IBetPlaced {
    private val _cards: MutableList<ICard> = mutableListOf()
    override val cards: List<ICard>
        get() = _cards
    override var blocked: Boolean = false

    override var bet: Int = 0

    override fun dealCard(): ICard {
        return _cards.removeLast()
    }

    override fun splitByLastCard(): Hand {
        val lastCard = _cards.removeLast()
        return Hand().apply { addCard(lastCard) }
    }

    override fun addCards(vararg cards: ICard) {
        cards.forEach { c -> this.addCard(c) }
    }

    override fun addCard(card: ICard) {
        _cards.add(card)
    }

    override fun flipAllCards(isFaceUp: Boolean) {
        for (card in _cards)
            card.isFaceUp = isFaceUp
    }

    override fun clear() {
        _cards.clear()
    }
}