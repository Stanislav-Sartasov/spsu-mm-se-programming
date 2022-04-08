package blackjack.shoe

import blackjack.card.ICard

interface IShoe {
    val cards: List<ICard>
    val numberOfDecks: Int
    fun dealCard(isFaceUp: Boolean): ICard
    fun shuffle()
    fun renew()
}