package roulette.bets

import bots.RndDozenPlayer
import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import roulette.SpinResult
import roulette.enums.PayoutCoefs

internal class NumberBetTest : BetTest() {
    @BeforeEach
    fun setUp() {
        player = RndDozenPlayer("test", initBalance)
        spin = SpinResult(7)
    }

    @Test
    fun `isWinningBet loss`() {
        val numberBet = NumberBet(player, bet, 10)
        numberBet.calculateWinnings(spin)
        assertEquals(player.balance, initBalance - bet)
    }

    @Test
    fun `isWinningBet win`() {
        val numberBet = NumberBet(player, bet, 7)
        numberBet.calculateWinnings(spin)
        assertEquals(player.balance, initBalance + bet * PayoutCoefs.NUMBER.coef)
    }

}