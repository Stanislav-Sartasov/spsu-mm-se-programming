package lib.blackjack.bots

import lib.blackjack.base.state.GameInfo
import lib.blackjack.base.Hand
import lib.blackjack.base.IPlayer
import lib.blackjack.base.state.PlayerMove
import lib.blackjack.base.state.PlayerMove.HIT
import lib.blackjack.base.state.PlayerMove.STAND

object BaseBot : IPlayer {
    override val name: String = "BaseBot"

    override fun getBet(bankroll: UInt, gameInfo: GameInfo): UInt {
        return gameInfo.rangeBet.first
    }

    override fun getMove(hand: Hand, gameInfo: GameInfo): PlayerMove {
        val score1 = hand.minScore()
        val score2 = hand.maxScore()

        if (score1 == score2) {
            if (score1 in 2..11 ||
                score1 == 12 && gameInfo.croupierCard.count !in 4..6 ||
                score1 in 13..16 && gameInfo.croupierCard.count !in 2..6
            )
                return HIT
        } else {
            if (score2 in 12..17 ||
                score2 == 18 && gameInfo.croupierCard.count in 9..11
            )
                return HIT
        }
        return STAND
    }
}
