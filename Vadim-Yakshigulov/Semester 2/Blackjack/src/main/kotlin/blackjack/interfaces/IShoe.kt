package blackjack.interfaces

interface IShoe {
    val cards: List<ICard>
    val numberOfDecks: Int
    fun dealCard(isFaceUp: Boolean): ICard
    fun shuffle()
    fun renew()
}