package blackjack.classes

import blackjack.BlackjackGame
import blackjack.interfaces.IBot
import blackjack.interfaces.IGame
import blackjack.interfaces.IPlayer
import java.io.InputStream
import java.io.OutputStream

abstract class Bot(val player: IPlayer) : IBot {
    override val inStream: InputStream = System.`in`
    override val outStream: OutputStream = System.out

    private val startBalance = player.balance
    private var currentGame: IGame = BlackjackGame(Dealer(), player, this)

    override fun Map<Pair<Int, Int>, String>.addStrategy(
        playerScoreRange: IntRange,
        dealerScoreRange: IntRange,
        action: String
    ): Map<Pair<Int, Int>, String> {
        val result: MutableList<Pair<Pair<Int, Int>, String>> = mutableListOf()
        for (playerScore in playerScoreRange) {
            for (dealerScore in dealerScoreRange)
                result.add(Pair(playerScore, dealerScore) to action)
        }
        return this + result
    }

    override fun chooseFromPossibleActions(msg: String, vararg actionsNames: String): String {
        val playerScore = ScoreCounter.calculateForHand(currentGame.player.activeHand)
        val dealerScore = ScoreCounter.calculateForHand(currentGame.dealer.activeHand)
        val name = strategy[Pair(playerScore, dealerScore)]
        if (name != null && name !in actionsNames) {
            if (name == "Double")
                return "Hit"
            if (name == "Split")
                return "Stand"
        }
        return name ?: "Stand"
    }

    override fun chooseFromBetsInRange(msg: String, start: Int, stop: Int): Int {
        return start
    }

    override fun run(n: Int, resetBalanceAfterGame: Boolean) {
        for (i in 1..n) {
            currentGame = StatisticCollector.startCollectFrom(BlackjackGame(Dealer(), player, this))
            currentGame.run()
            StatisticCollector.stopCollectFrom(currentGame)
            if (resetBalanceAfterGame)
                player.balance = startBalance
        }
    }
}