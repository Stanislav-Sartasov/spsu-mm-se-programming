package game.roulette.session

import bet.Bet
import request.BetRequest
import cash_account.Account

interface Session {
	fun play()

	fun registerBet(bet: Bet, account: Account, amount: Double): BetRequest
}