package botLib

import blackjack.player.Player
import blackjack.util.StatisticCollector
import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

internal class DealerLikeBlackjackBotTest {
    @Test
    fun `after 100000 runs bot has at least one win, one draw and one lose`() {
        val bot = DealerLikeBlackjackBot(Player(1000))
        bot.run(100000)
        assertTrue(StatisticCollector.numberOfWins >= 1 && StatisticCollector.numberOfLosses >= 1 && StatisticCollector.numberOfDraws >= 1)
        StatisticCollector.reset()
    }
}