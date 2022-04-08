package blackjack.action

import blackjack.player.Dealer
import blackjack.player.Player
import org.junit.jupiter.api.Test

import org.junit.jupiter.api.Assertions.*
import utils.GameOnlyWithInitialCardDistribution
import utils.SimpleIOHandler

internal class DistributeCardsInitiallyActionTest {

    @Test
    fun `after execution player and dealer have 2 cards`() {
        val game = GameOnlyWithInitialCardDistribution(Dealer(), Player(100), SimpleIOHandler())
        game.run()
        assertTrue(game.player.hands.size == 1 && game.player.hands.first().cards.size == 2 && game.player.hands.first().bet >= game.minimumBetSize)
        assertTrue(game.dealer.hands.size == 1 && game.dealer.hands.first().cards.size == 2)
    }
}