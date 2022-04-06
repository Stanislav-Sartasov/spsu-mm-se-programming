package game.roulette.session

import bet.Bet
import request.BetRequest
import cash_account.Account
import game.roulette.wheel.RouletteWheel
import service.bet.BetManager
import service.bet.ListBetManager
import util.BetStatus

class RouletteSession(
	private val rouletteWheel: RouletteWheel,
	val casinoAccount: Account
) : Session {

	private val betManager: BetManager = ListBetManager(casinoAccount)

	override fun play() {
		val wonValue = rouletteWheel.spin()
		betManager.process(wonValue)
		betManager.clear()
	}

	override fun registerBet(bet: Bet, account: Account, amount: Double): BetRequest {
		val betRequest = BetRequest(
			bet,
			account,
			amount,
			1.0 * (rouletteWheel.size - rouletteWheel.zeroCount) / bet.wonValuesCount(),
			BetStatus.IN_PROCESS
		)
		betManager.add(betRequest)
		return betRequest
	}
}