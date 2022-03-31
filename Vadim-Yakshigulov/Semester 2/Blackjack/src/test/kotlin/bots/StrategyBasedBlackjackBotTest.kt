package bots

import blackjack.classes.Player
import blackjack.classes.StatisticCollector
import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

internal class StrategyBasedBlackjackBotTest {
    @Test
    fun `after 100000 runs bot has at least one win, one draw and one lose`() {
        val bot = StrategyBasedBlackjackBot(Player(1000))
        bot.run(100000)
        assertTrue(StatisticCollector.numberOfWins >= 1 && StatisticCollector.numberOfLosses >= 1 && StatisticCollector.numberOfDraws >= 1)
        StatisticCollector.reset()
    }
}