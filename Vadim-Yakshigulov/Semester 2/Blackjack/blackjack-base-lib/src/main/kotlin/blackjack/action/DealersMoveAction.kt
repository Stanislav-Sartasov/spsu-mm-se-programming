package blackjack.action

import blackjack.action.inGame.HitAction
import blackjack.util.ScoreCounter
import blackjack.state.State
import blackjack.game.IGame

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