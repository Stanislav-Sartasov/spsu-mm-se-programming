package bot.bet_strategy

import bet.Bet
import request.BetRequest

class FixedAmountBetStrategy(private val bet: Bet, private val amount: Double) : BetStrategy {
	override fun add(betRequest: BetRequest) {}

	override fun nextBet(): Pair<Bet, Double> = bet to amount
}