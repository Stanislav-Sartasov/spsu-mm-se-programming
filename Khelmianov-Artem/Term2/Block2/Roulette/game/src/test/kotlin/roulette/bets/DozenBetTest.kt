package roulette.bets

import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import roulette.SpinResult
import roulette.enums.Dozens
import roulette.enums.PayoutCoefs
import kotlin.test.assertEquals

internal class DozenBetTest : BetTest() {
    @BeforeEach
    fun setUp() {
        player = TestPlayer("test", 100)
        spin = SpinResult(7)
    }

    @Test
    fun `isWinningBet loss`() {
        val dozenBet = DozenBet(player, bet, Dozens.THIRD)
        dozenBet.calculateWinnings(spin)
        assertEquals(player.balance, initBalance - bet)
    }

    @Test
    fun `isWinningBet win`() {
        val dozenBet = DozenBet(player, bet, Dozens.FIRST)
        dozenBet.calculateWinnings(spin)
        assertEquals(player.balance, initBalance + bet * PayoutCoefs.DOZEN.coef)
    }
}