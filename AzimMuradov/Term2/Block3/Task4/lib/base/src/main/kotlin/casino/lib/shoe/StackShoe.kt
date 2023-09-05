package casino.lib.shoe

import casino.lib.card.Card

class StackShoe(private val cards: List<Card>) : Shoe {

    override val dealt: List<Card> get() = cards.subList(0, i)

    override fun dealCard(): Card = cards[i++]

    override fun isNotEmpty(): Boolean = i < cards.size


    private var i = 0
}
