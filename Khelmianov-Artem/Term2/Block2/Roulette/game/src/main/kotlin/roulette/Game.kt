package roulette

import roulette.bets.ABet
import kotlin.random.Random
import kotlin.random.nextInt

class Game {
    val players = ArrayList<APlayer>()
    private var placedBets = ArrayList<ABet>()

    fun play(turns: Int = 1) {
        for (i in 0 until turns) {
            takeBets()
            val result = spinWheel()
            processBets(result)
        }
    }

    fun addPlayer(player: APlayer) = players.add(player)

    fun removePlayer(playerName: String) = players.removeIf { p -> p.name == playerName }

    fun removeAllPlayers() = players.clear()

    private fun takeBets() {
        placedBets.addAll(
            players
                .filter { it.isPlaying }
                .map { it.placeBet() }
        )
    }

    private fun spinWheel(): SpinResult {
        val randomNumber = Random.nextInt(0..36)
        return SpinResult(randomNumber)
    }

    private fun processBets(result: SpinResult) {
        placedBets.forEach { it.calculateWinnings(result) }
        placedBets.clear()
    }
}