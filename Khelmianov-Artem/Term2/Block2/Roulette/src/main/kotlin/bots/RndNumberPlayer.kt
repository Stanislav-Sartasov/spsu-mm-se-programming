package bots

import roulette.bets.ABet
import roulette.bets.NumberBet
import roulette.APlayer
import java.lang.Integer.min
import kotlin.random.Random
import kotlin.random.nextInt

class RndNumberPlayer(
    name: String,
    balance: Int
) : APlayer(name, balance) {
    private val maxBet = balance.div(20)

    override fun placeBet(): ABet {
        val bet = Random.nextInt(maxBet)
        return NumberBet(this, min(bet, balance), Random.nextInt(0..36))
    }
}
