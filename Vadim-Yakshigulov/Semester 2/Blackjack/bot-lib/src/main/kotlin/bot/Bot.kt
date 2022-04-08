package bot

import blackjack.action.inGame.InGameAction
import blackjack.game.BlackjackGame
import blackjack.game.IGame
import blackjack.player.Dealer
import blackjack.player.IPlayer
import blackjack.util.ScoreCounter
import blackjack.util.StatisticCollector
import java.io.InputStream
import java.io.OutputStream

abstract class Bot(val player: IPlayer) : IBot {
    override val inStream: InputStream = System.`in`
    override val outStream: OutputStream = System.out

    private val startBalance = player.balance
    private var currentGame: IGame = BlackjackGame(Dealer(), player, this)

    override fun Map<Pair<Int, Int>, InGameAction>.addStrategy(
        playerScoreRange: IntRange,
        dealerScoreRange: IntRange,
        action: InGameAction
    ): Map<Pair<Int, Int>, InGameAction> {
        val result: MutableList<Pair<Pair<Int, Int>, InGameAction>> = mutableListOf()
        for (playerScore in playerScoreRange) {
            for (dealerScore in dealerScoreRange)
                result.add(Pair(playerScore, dealerScore) to action)
        }
        return this + result
    }

    override fun chooseFromPossibleActions(msg: String, vararg actionsNames: InGameAction): InGameAction {
        val playerScore = ScoreCounter.calculateForHand(currentGame.player.activeHand)
        val dealerScore = ScoreCounter.calculateForHand(currentGame.dealer.activeHand)
        val name = strategy[Pair(playerScore, dealerScore)]
        if (name != null && name !in actionsNames) {
            if (name == InGameAction.DOUBLE)
                return InGameAction.HIT
            if (name == InGameAction.SPLIT)
                return InGameAction.STAND
        }
        return name ?: InGameAction.STAND
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