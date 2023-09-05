package roulette.bets

import roulette.APlayer
import roulette.SpinResult

open class BetTest {
    lateinit var player: APlayer
    lateinit var spin: SpinResult
    val initBalance = 100
    val bet = 10
}

class TestPlayer(
    name: String,
    balance: Int
): APlayer(name, balance) {
    override fun placeBet(): ABet {
        return NumberBet(this, 1, 1)
    }
}