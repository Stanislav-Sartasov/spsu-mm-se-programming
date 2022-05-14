package request

import bet.Bet
import util.BetStatus
import cash_account.Account

data class BetRequest(
	val bet: Bet,
	val account: Account,
	val amount: Double,
	val coefficient: Double,
	var status: BetStatus
)
