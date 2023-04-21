package lib.blackjack.base

import lib.blackjack.base.state.GameInfo
import lib.blackjack.base.state.PlayerMove

interface IPlayer {
    val name: String

    fun getBet(bankroll: UInt, gameInfo: GameInfo): UInt

    fun getMove(hand: Hand, gameInfo: GameInfo): PlayerMove
}