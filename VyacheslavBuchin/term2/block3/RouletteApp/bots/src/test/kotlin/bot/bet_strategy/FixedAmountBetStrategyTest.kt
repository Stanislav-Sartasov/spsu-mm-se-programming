package bot.bet_strategy

import bet.OutsideRouletteBet
import bet.StraightBet
import cash_account.CashAccount

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments
import org.junit.jupiter.params.provider.MethodSource
import request.BetRequest
import util.BetStatus

internal class FixedAmountBetStrategyTest {

	companion object {

		private val bets = listOf(
			FixedAmountBetStrategy(StraightBet(0), 100.0),
			FixedAmountBetStrategy(OutsideRouletteBet.EVEN, 228.0),
			FixedAmountBetStrategy(OutsideRouletteBet.P12, 1337.0)
		)

		private val statuses = listOf(
			BetStatus.LOST,
			BetStatus.WON,
			BetStatus.IN_PROCESS
		)

		@JvmStatic
		fun bets() = bets.map { Arguments.of(it) }

		@JvmStatic
		fun betsWithStatuses(): ArrayList<Arguments> {
			val result = ArrayList<Arguments>()
			bets.forEach { bet ->
				statuses.forEach { status ->
					result.add(Arguments.of(bet, status))
				}
			}
			return result
		}
	}

	@ParameterizedTest
	@MethodSource("bets")
	fun `nextBet() should return same bet and same amount every time`(strategy: FixedAmountBetStrategy) {
		val (bet1, amount1) = strategy.nextBet()
		val (bet2, amount2) = strategy.nextBet()

		assertEquals(amount1, amount2)
		assertEquals(bet1::class, bet2::class)
	}

	@ParameterizedTest
	@MethodSource("betsWithStatuses")
	fun `add() should not change next bet`(strategy: FixedAmountBetStrategy, status: BetStatus) {
		val (bet1, amount1) = strategy.nextBet()
		val betRequest = BetRequest(bet1, CashAccount(0.0), 0.0, 0.0, status)
		strategy.add(betRequest)
		val (bet2, amount2) = strategy.nextBet()

		assertEquals(amount1, amount2)
		assertEquals(bet1::class, bet2::class)
	}
}