package blackjack.action

import blackjack.state.State
import blackjack.game.IGame

class AskForBetAction(override val game: IGame) :
    IStateAction {
    override fun execute(): State {
        if (game.player.balance < game.minimumBetSize) {
            return State.NotEnoughMoney(game)
        }

        val initialBet =
            game.ioHandler.chooseFromBetsInRange("Choose from possible bets", 10, game.player.balance)
        game.player.balance -= initialBet
        return State.InitialCardDistribution(game, initialBet)
    }
}