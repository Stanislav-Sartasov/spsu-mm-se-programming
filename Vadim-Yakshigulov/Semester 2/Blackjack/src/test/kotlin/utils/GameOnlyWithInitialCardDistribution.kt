package utils

import blackjack.classes.Shoe
import blackjack.classes.State
import blackjack.interfaces.IDealer
import blackjack.interfaces.IGame
import blackjack.interfaces.IOHandler
import blackjack.interfaces.IPlayer

class GameOnlyWithInitialCardDistribution(
    override val dealer: IDealer, override val player: IPlayer,
    override val ioHandler: IOHandler,
) : IGame {
    override val minimumBetSize: Int = 10
    var state: State = State.Start(this)
    override val shoe = Shoe(8)
    override fun run() {
        while (state != State.GameOver(this)) {
            state.doBeforeActionExecution()
            val nextState = state.action.execute()
            state.doAfterActionExecution()
            state = if (state == State.InitialCardDistribution(this, -1))
                State.GameOver(this)
            else
                nextState
        }
    }
}
