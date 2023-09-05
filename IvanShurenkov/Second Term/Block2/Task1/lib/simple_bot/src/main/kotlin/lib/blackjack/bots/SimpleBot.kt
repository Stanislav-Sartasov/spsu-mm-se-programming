package lib.blackjack.bots

import lib.blackjack.base.state.GameInfo
import lib.blackjack.base.Hand
import lib.blackjack.base.IPlayer
import lib.blackjack.base.state.PlayerMove
import lib.blackjack.base.state.PlayerMove.HIT
import lib.blackjack.base.state.PlayerMove.STAND

object SimpleBot : IPlayer {
    override val name: String = "SimpleBot"

    override fun getBet(bankroll: UInt, gameInfo: GameInfo): UInt {
        return gameInfo.rangeBet.first
    }

    override fun getMove(hand: Hand, gameInfo: GameInfo): PlayerMove {
        return if (hand.maxScore() < 16) HIT else STAND
    }
}