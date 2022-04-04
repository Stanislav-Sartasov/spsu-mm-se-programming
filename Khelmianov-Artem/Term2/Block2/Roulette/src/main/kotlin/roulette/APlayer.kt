package roulette

import roulette.bets.ABet

abstract class APlayer(
    val name: String,
    var balance: Int
) {
    var isLastBetWin = false
    var isPlaying = true

    abstract fun placeBet(): ABet

    fun changeBalance(delta: Int) {
        balance += delta
        if (balance <= 0) {
            isPlaying = false
        }
    }

}