package blackjack.game

import blackjack.shoe.Shoe
import blackjack.state.State
import blackjack.player.IDealer
import blackjack.game.IGame
import blackjack.ioHandler.IOHandler
import blackjack.player.IPlayer

class BlackjackGame(
    override val dealer: IDealer, override val player: IPlayer,
    override val ioHandler: IOHandler,
    override val minimumBetSize: Int = 10
) : IGame {
    override var state: State = State.Start(this)
    override val shoe = Shoe(8)
    override fun run() {
        while (state != State.GameOver(this)) {
            state.doBeforeActionExecution()
            val nextState = state.action.execute()
            state.doAfterActionExecution()
            state = nextState
        }
    }
}