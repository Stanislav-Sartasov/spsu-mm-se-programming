package blackjack.action.inGame

import blackjack.hand.Hand
import blackjack.game.IGame

class DoubleAction(val game: IGame, val activeHand: Hand) : IInGameAction {
    override val displayName: InGameAction = InGameAction.DOUBLE
    override fun execute() {
        game.player.balance -= activeHand.bet
        activeHand.bet *= 2
        HitAction(game, activeHand).execute()
        StandAction(game, activeHand).execute()
    }
}