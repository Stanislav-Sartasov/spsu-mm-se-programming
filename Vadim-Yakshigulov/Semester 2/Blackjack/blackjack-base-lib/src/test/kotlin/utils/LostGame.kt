package utils

import blackjack.shoe.Shoe
import blackjack.state.State
import blackjack.player.IDealer
import blackjack.game.IGame
import blackjack.ioHandler.IOHandler
import blackjack.player.IPlayer

class LostGame(
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
            state = nextState
            if (state == State.PlayerTurn(this)) {
                val hand = player.hands.removeFirst()
                state = State.PlayerLost(this, hand)
            }
        }
    }
}