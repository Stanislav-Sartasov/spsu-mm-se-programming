package blackjack.deck

import blackjack.card.Card
import blackjack.card.CardFace
import blackjack.card.CardSuit
import blackjack.card.ICard

class Deck : IDeck {
    override val cards: List<ICard>
        get() = _cards
    private val _cards: MutableList<ICard> = mutableListOf()
    init {
        renew()
    }

    override fun renew() {
        _cards.clear()
        for (suit in CardSuit.values())
            for (face in CardFace.values()) {
                _cards.add(Card(suit, face))
            }
    }
}