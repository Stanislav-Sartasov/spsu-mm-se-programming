package blackjack.interfaces

interface IInGameAction {
    val displayName: String

    fun execute()
}