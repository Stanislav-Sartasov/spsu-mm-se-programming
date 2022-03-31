package blackjack.actions.inGame

import blackjack.classes.Hand
import blackjack.interfaces.IGame
import blackjack.interfaces.IInGameAction

class DoubleAction(val game: IGame, val activeHand: Hand) : IInGameAction {
    override val displayName: String
        get() = "Double"
    override fun execute() {
        game.player.balance -= activeHand.bet
        activeHand.bet *= 2
        HitAction(game, activeHand).execute()
        StandAction(game, activeHand).execute()
    }
}