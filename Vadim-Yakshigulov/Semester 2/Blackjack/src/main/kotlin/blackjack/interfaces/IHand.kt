package blackjack.interfaces

interface IHand {
    val cards: List<ICard>
    val blocked: Boolean
    fun dealCard(): ICard
    fun addCards(vararg cards: ICard)
    fun addCard(card: ICard)
    fun flipAllCards(isFaceUp: Boolean)
    fun splitByLastCard(): IHand
    fun clear()
}