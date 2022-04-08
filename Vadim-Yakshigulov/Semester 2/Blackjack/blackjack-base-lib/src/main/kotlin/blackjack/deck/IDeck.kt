package blackjack.deck

import blackjack.card.ICard

interface IDeck {
    val cards: List<ICard>
    fun renew()
}