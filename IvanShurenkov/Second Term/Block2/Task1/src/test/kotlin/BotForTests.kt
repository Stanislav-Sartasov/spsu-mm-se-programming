import lib.blackjack.base.GameInfo
import lib.blackjack.base.Hand
import lib.blackjack.base.IPlayer
import lib.blackjack.base.PlayerMove
import lib.blackjack.base.PlayerMove.HIT
import lib.blackjack.base.PlayerMove.STAND

object BotForTests : IPlayer {
    override val name: String = "BotForTests"
    var log: List<PlayerMove> = emptyList()
    var bet: Int = 1
    var onlyHit: Boolean = false
    var onlyStand: Boolean = false

    override fun getBet(bankroll: UInt, gameInfo: GameInfo): UInt {
        return bet.toUInt()
    }

    override fun getMove(hand: Hand, gameInfo: GameInfo): PlayerMove {
        if (onlyHit)
            return HIT
        if (onlyStand)
            return STAND
        log = log + (if (hand.maxScore() < 16) HIT else STAND)
        return if (hand.maxScore() < 16) HIT else STAND
    }
}