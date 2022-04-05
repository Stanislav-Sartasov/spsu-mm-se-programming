package bots

import roulette.APlayer
import roulette.bets.ABet
import roulette.bets.DozenBet
import roulette.enums.Dozens
import kotlin.random.Random
import kotlin.random.nextInt

class RndDozenPlayer(
    name: String,
    balance: Int
) : APlayer(name, balance) {
    private val bet = balance.div(20)

    override fun placeBet(): ABet {
        return DozenBet(this, Integer.min(bet, balance), Dozens.fromInt(Random.nextInt(1..36)))
    }

}