package blackjack.classes

import blackjack.enums.CardFace
import blackjack.enums.CardSuit
import blackjack.interfaces.ICard
import blackjack.interfaces.IDeck

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