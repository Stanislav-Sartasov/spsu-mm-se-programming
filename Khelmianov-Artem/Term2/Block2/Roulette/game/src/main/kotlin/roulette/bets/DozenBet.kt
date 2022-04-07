package roulette.bets

import roulette.APlayer
import roulette.enums.Dozens
import roulette.enums.PayoutCoefs
import roulette.SpinResult

class DozenBet(
    bidder: APlayer,
    betAmount: Int,
    private val dozen: Dozens
) : ABet(bidder, betAmount, PayoutCoefs.DOZEN.coef) {

    override fun isWinningBet(result: SpinResult): Boolean {
        return result.dozen == dozen
    }
}