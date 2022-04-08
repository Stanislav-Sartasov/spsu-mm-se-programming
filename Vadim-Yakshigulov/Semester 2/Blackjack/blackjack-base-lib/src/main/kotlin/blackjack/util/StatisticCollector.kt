package blackjack.util

import blackjack.game.IGame

object StatisticCollector {
    var numberOfLosses = 0
        private set
    var numberOfWins = 0
        private set
    var numberOfDraws = 0
        private set
    var totalLooseAmount = 0
        private set
    var totalWonAmount = 0
        private set
    var totalGamesPlayed = 0
        private set

    val winRate
        get() = numberOfWins.toDouble() / totalGamesPlayed

    var playerBalanceBeforeLastGame = 0
    var playerBalanceAfterLastGame = 0

    fun startCollectFrom(game: IGame): IGame {
        playerBalanceBeforeLastGame = game.player.balance
        return game
    }

    fun stopCollectFrom(game: IGame): IGame {
        playerBalanceAfterLastGame = game.player.balance
        updateValues()
        return game
    }

    private fun updateValues() {
        totalGamesPlayed++
        if (playerBalanceAfterLastGame == playerBalanceBeforeLastGame) numberOfDraws++
        if (playerBalanceAfterLastGame < playerBalanceBeforeLastGame) {
            numberOfLosses++
            totalLooseAmount += playerBalanceBeforeLastGame - playerBalanceAfterLastGame
        }
        if (playerBalanceAfterLastGame > playerBalanceBeforeLastGame) {
            numberOfWins++
            totalWonAmount += playerBalanceAfterLastGame - playerBalanceBeforeLastGame
        }
    }

    fun printFullStatisticToConsole() {
        println("Statistic for the last $totalGamesPlayed games")
        println("Number of:")
        println("1. Wins: $numberOfWins")
        println("2. Losses: $numberOfLosses")
        println("3. Draws: $numberOfDraws")
        println("Win rate: $winRate")
        println("Total loose amount: $totalLooseAmount")
        println("Total won amount: $totalWonAmount")
    }

    fun reset() {
        numberOfLosses = 0
        numberOfWins = 0
        numberOfDraws = 0
        totalLooseAmount = 0
        totalWonAmount = 0
        totalGamesPlayed = 0
        playerBalanceBeforeLastGame = 0
        playerBalanceAfterLastGame = 0
    }
}