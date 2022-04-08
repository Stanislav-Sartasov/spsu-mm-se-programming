package blackjack.action.inGame

import blackjack.hand.Hand
import blackjack.game.IGame

class HitAction(val game: IGame, val activeHand: Hand) : IInGameAction {
    override val displayName: InGameAction = InGameAction.HIT

    override fun execute() {
        activeHand.addCard(game.shoe.dealCard(isFaceUp = true))
    }
}