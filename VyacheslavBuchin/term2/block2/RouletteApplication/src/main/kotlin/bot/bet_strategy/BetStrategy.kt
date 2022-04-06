package bot.bet_strategy

import bet.Bet
import request.BetRequest

interface BetStrategy {
	fun add(betRequest: BetRequest)
	fun nextBet(): Pair<Bet, Double>
}