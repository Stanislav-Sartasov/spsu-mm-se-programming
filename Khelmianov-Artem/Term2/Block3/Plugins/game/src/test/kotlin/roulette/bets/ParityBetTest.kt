package roulette.bets

import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import roulette.SpinResult
import roulette.enums.Parity
import roulette.enums.PayoutCoefs
import kotlin.test.assertEquals

internal class ParityBetTest : BetTest() {
    @BeforeEach
    fun setUp() {
        player = TestPlayer("test", 100)
        spin = SpinResult(7)
    }

    @Test
    fun `isWinningBet loss`() {
        val parityBet = ParityBet(player, bet, Parity.EVEN)
        parityBet.calculateWinnings(spin)
        assertEquals(player.balance, initBalance - bet)
    }

    @Test
    fun `isWinningBet win`() {
        val parityBet = ParityBet(player, bet, Parity.ODD)
        parityBet.calculateWinnings(spin)
        assertEquals(player.balance, initBalance + bet * PayoutCoefs.PARITY.coef)
    }
}