package game.roulette.session

import bet.StraightBet
import cash_account.CashAccount
import game.roulette.wheel.EuropeanRouletteWheel

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test
import util.BetStatus

internal class RouletteSessionTest {

	private val session = RouletteSession(EuropeanRouletteWheel(), CashAccount(1000.0))

	@Test
	fun `registerBet() should return betRequest with IN_PROCESS status if transfer account balance is enough to bet given amount`() {
		val request = session.registerBet(StraightBet(0), CashAccount(100.0), 10.0)
		assertEquals(BetStatus.IN_PROCESS, request.status)
	}

	@Test
	fun `registerBet() should return betRequest with REJECTED status if transfer account balance is less than given amount`() {
		val request = session.registerBet(StraightBet(0), CashAccount(0.0), 10.0)
		assertEquals(BetStatus.REJECTED, request.status)
	}
}