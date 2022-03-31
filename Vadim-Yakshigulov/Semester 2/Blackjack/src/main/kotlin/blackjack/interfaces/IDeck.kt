package blackjack.interfaces

interface IDeck {
    val cards: List<ICard>
    fun renew()
}