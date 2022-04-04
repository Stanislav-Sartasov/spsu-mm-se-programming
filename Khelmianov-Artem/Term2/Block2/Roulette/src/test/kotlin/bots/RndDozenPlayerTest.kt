package bots

import org.junit.jupiter.api.Assertions.assertTrue
import org.junit.jupiter.api.Test
import roulette.bets.DozenBet
import kotlin.test.assertIs

internal class RndDozenPlayerTest {
    val player = RndDozenPlayer("", 100)

    @Test
    fun placeBet() {
        val bet = player.placeBet()
        assertIs<DozenBet>(bet)
        assertTrue(bet.bidder === player)
        assertTrue(bet.betAmount == 5)
    }
}