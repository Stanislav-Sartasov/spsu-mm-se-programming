package blackjack

import blackjack.classes.Shoe
import blackjack.classes.State
import blackjack.interfaces.IDealer
import blackjack.interfaces.IGame
import blackjack.interfaces.IOHandler
import blackjack.interfaces.IPlayer

class BlackjackGame(
    override val dealer: IDealer, override val player: IPlayer,
    override val ioHandler: IOHandler,
    override val minimumBetSize: Int = 10
) : IGame {
    var state: State = State.Start(this)
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