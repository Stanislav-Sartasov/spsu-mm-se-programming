package roulette.bets

import roulette.APlayer
import roulette.enums.Parity
import roulette.enums.PayoutCoefs
import roulette.SpinResult

class ParityBet(
    bidder: APlayer,
    betAmount: Int,
    private val parity: Parity
) : ABet(bidder, betAmount, PayoutCoefs.PARITY.coef) {

    override fun isWinningBet(result: SpinResult): Boolean {
        return result.parity == parity
    }
}