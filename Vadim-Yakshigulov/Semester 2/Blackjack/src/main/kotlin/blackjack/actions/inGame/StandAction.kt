package blackjack.actions.inGame

import blackjack.classes.Hand
import blackjack.interfaces.IGame
import blackjack.interfaces.IInGameAction

class StandAction(val game: IGame, val activeHand: Hand) : IInGameAction {
    override val displayName: String = "Stand"

    override fun execute() {
        activeHand.blocked = true
    }
}