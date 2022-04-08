package blackjack.action.inGame

interface IInGameAction {
    val displayName: InGameAction
    fun execute()
}