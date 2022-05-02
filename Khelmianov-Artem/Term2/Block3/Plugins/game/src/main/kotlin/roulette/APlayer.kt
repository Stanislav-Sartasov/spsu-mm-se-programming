package roulette

import roulette.bets.ABet

abstract class APlayer(
    val name: String,
    balance: Int
) {
    var balance = balance
        private set
    var isLastBetWin = false
    var isPlaying = true
        private set

    abstract fun placeBet(): ABet

    fun changeBalance(delta: Int) {
        balance += delta
        if (balance <= 0) {
            isPlaying = false
        }
    }
}