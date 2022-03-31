package blackjack.actions.inGame

import blackjack.classes.Hand
import blackjack.interfaces.IGame
import blackjack.interfaces.IInGameAction

class HitAction(val game: IGame, val activeHand: Hand) : IInGameAction {
    override val displayName: String = "Hit"

    override fun execute() {
        activeHand.addCard(game.shoe.dealCard(isFaceUp = true))
    }
}