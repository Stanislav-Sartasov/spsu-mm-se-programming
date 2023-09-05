package roulette.bots

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
    private var lastBet = 1
    private var lastParity = Parity.EVEN

    override fun placeBet(): ABet {
        if (isLastBetWin) {
            lastBet = 2
            lastParity = Parity.fromInt(Random.nextInt(1..2))
        } else {
            lastBet *= 2
        }
        return ParityBet(this, lastBet, lastParity)
    }
}