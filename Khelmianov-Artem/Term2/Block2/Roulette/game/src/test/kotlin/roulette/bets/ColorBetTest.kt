package roulette.bets

import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import roulette.SpinResult
import roulette.enums.Colors
import roulette.enums.PayoutCoefs
import kotlin.test.assertEquals

internal class ColorBetTest : BetTest() {
    @BeforeEach
    fun setUp() {
        player = TestPlayer("test", 100)
        spin = SpinResult(7)
    }

    @Test
    fun `isWinningBet loss`() {
        val colorBet = ColorBet(player, bet, Colors.BLACK)
        colorBet.calculateWinnings(spin)
        assertEquals(player.balance, initBalance - bet)
    }

    @Test
    fun `isWinningBet win`() {
        val colorBet = ColorBet(player, bet, Colors.RED)
        colorBet.calculateWinnings(spin)
        assertEquals(player.balance, initBalance + bet * PayoutCoefs.COLOR.coef)
    }
}