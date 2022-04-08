package blackjack.action.inGame

import blackjack.hand.Hand
import blackjack.game.IGame

class StandAction(val game: IGame, val activeHand: Hand) : IInGameAction {
    override val displayName: InGameAction = InGameAction.STAND

    override fun execute() {
        activeHand.blocked = true
    }
}