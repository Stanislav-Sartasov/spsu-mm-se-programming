package blackjack.actions.inGame

import blackjack.classes.Hand
import blackjack.interfaces.IGame
import blackjack.interfaces.IInGameAction

class SplitAction(val game: IGame, val activeHand: Hand) : IInGameAction {
    override val displayName: String = "Split"

    override fun execute() {
        val newHand = activeHand.splitByLastCard()
        game.player.hands.add(newHand)
        HitAction(game, activeHand).execute()
        HitAction(game, newHand).execute()
        newHand.bet = activeHand.bet
    }
}