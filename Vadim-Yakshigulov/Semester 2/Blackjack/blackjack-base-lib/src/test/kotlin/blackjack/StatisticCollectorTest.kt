package blackjack.classes

import blackjack.game.BlackjackGame
import blackjack.player.Dealer
import blackjack.player.Player
import blackjack.util.StatisticCollector
import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test
import utils.SimpleIOHandler

internal class StatisticCollectorTest {
    @Test
    fun `after reset statistic calculation works fine`() {
        val player = Player(100)
        val firstGame = StatisticCollector.startCollectFrom(BlackjackGame(Dealer(), player, SimpleIOHandler()))
        firstGame.run()
        StatisticCollector.stopCollectFrom(firstGame)

        assertEquals(StatisticCollector.totalGamesPlayed, 1)
        StatisticCollector.reset()
        assertEquals(StatisticCollector.totalGamesPlayed, 0)
        val secondGame = StatisticCollector.startCollectFrom(BlackjackGame(Dealer(), player, SimpleIOHandler()))
        secondGame.run()
        StatisticCollector.stopCollectFrom(secondGame)
        assertEquals(StatisticCollector.totalGamesPlayed, 1)

        StatisticCollector.reset()
    }
}