package roulette.bets

import roulette.APlayer
import roulette.enums.Colors
import roulette.enums.PayoutCoefs
import roulette.SpinResult

class ColorBet(
    bidder: APlayer,
    betAmount: Int,
    private val color: Colors,
) : ABet(bidder, betAmount, PayoutCoefs.COLOR.coef) {

    override fun isWinningBet(result: SpinResult): Boolean {
        return result.color == color
    }
}