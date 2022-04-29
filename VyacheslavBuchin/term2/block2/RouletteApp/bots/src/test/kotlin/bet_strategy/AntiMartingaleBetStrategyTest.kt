package bot.bet_strategy

import cash_account.CashAccount

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test
import request.BetRequest
import util.BetStatus

internal class AntiMartingaleBetStrategyTest {

	private val initialBet = 10.0
	private val strategy = AntiMartingaleBetStrategy(initialBet)
	private val account = CashAccount(100.0)

	@Test
	fun `nextBet() should return bet with doubled amount and same bet if last game is won`() {
		val (bet1, amount1) = strategy.nextBet()
		strategy.add(BetRequest(bet1, account, amount1, 1.0, BetStatus.WON))
		val (bet2, amount2) = strategy.nextBet()
		assertEquals(amount1 * 2, amount2)
		assertEquals(bet1::class, bet2::class)
	}

	@Test
	fun `nextBet() should return bet with initial amount if last game is lost`() {
		val (bet1, amount1) = strategy.nextBet()
		strategy.add(BetRequest(bet1, account, amount1, 1.0, BetStatus.LOST))
		val (bet2, amount2) = strategy.nextBet()
		assertEquals(initialBet, amount2)
	}
}