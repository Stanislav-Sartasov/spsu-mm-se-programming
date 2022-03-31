package blackjack.actions

import blackjack.actions.inGame.HitAction
import blackjack.classes.ScoreCounter
import blackjack.classes.State
import blackjack.interfaces.IGame
import blackjack.interfaces.IStateAction

class DealersMoveAction(override val game: IGame) : IStateAction {
    override fun execute(): State {
        if (ScoreCounter.calculateForHand(game.dealer.activeHand) >= 17) {
            return State.WinnerSelection(game)
        }

        game.dealer.activeHand.flipAllCards(true)
        HitAction(game, game.dealer.activeHand).execute()
        return State.DealerTurn(game)
    }
}