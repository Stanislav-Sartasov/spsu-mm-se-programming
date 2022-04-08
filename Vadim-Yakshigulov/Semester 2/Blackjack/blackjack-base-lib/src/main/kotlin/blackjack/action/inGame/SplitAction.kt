package blackjack.action.inGame

import blackjack.hand.Hand
import blackjack.game.IGame

class SplitAction(val game: IGame, val activeHand: Hand) : IInGameAction {
    override val displayName: InGameAction = InGameAction.SPLIT

    override fun execute() {
        val newHand = activeHand.splitByLastCard()
        game.player.hands.add(newHand)
        HitAction(game, activeHand).execute()
        HitAction(game, newHand).execute()
        newHand.bet = activeHand.bet
    }
}