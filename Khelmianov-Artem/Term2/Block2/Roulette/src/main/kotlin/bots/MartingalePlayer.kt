package bots

import roulette.APlayer
import roulette.bets.ABet
import roulette.bets.ParityBet
import roulette.enums.Parity
import kotlin.random.Random
import kotlin.random.nextInt

class MartingalePlayer(
    name: String,
    balance: Int
) : APlayer(name, balance) {
    private var lastBet = ParityBet(this, 1, Parity.EVEN)

    override fun placeBet(): ABet {
        return if (isLastBetWin) {
            lastBet = ParityBet(this, 2, Parity.fromInt(Random.nextInt(1..2)))
            lastBet
        } else {
            lastBet.betAmount *= 2
            lastBet
        }
    }
}