package roulette.bets

import roulette.APlayer
import roulette.enums.PayoutCoefs
import roulette.SpinResult

class NumberBet(
    bidder: APlayer,
    betAmount: Int,
    private val number: Int,
) : ABet(bidder, betAmount, PayoutCoefs.NUMBER.coef) {

    override fun isWinningBet(result: SpinResult): Boolean {
        return result.number == number
    }
}