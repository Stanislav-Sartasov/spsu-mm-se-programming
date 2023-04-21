package roulette.bets

import roulette.APlayer
import roulette.SpinResult

abstract class ABet(
    val bidder: APlayer,
    val betAmount: Int,
    private var payoutCoef: Int
) {

    protected abstract fun isWinningBet(result: SpinResult): Boolean

    fun calculateWinnings(result: SpinResult) {
        if (isWinningBet(result)) {
            bidder.changeBalance(payoutCoef * betAmount)
            bidder.isLastBetWin = true
        } else {
            bidder.changeBalance(-betAmount)
            bidder.isLastBetWin = false
        }
    }
}
