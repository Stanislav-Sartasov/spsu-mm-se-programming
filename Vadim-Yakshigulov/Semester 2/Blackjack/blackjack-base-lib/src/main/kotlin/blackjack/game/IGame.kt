package blackjack.game

import blackjack.ioHandler.IOHandler
import blackjack.player.IDealer
import blackjack.player.IPlayer
import blackjack.shoe.IShoe
import blackjack.state.State

interface IGame {
    val minimumBetSize: Int
    val ioHandler: IOHandler
    val dealer: IDealer
    val player: IPlayer
    val shoe: IShoe
    val state: State
    fun run()
}