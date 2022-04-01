package lib.blackjack.base

interface IPlayer {
    val name: String

    fun getBet(bankroll: UInt, gameInfo: GameInfo): UInt

    fun getMove(hand: Hand, gameInfo: GameInfo): PlayerMove
}