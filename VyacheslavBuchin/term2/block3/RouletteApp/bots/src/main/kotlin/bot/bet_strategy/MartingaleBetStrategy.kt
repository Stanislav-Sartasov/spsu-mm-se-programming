package bot.bet_strategy

import bet.Bet
import bet.OutsideRouletteBet
import util.BetStatus

class MartingaleBetStrategy(minimalBet: Double) : MartingaleBasedBetStrategy(minimalBet) {

	override fun nextBet(): Pair<Bet, Double> {
		if (betRequestHistory.isEmpty())
			return OutsideRouletteBet.RED to minimalBet
		val lastBetRequest = betRequestHistory.element()
		if (lastBetRequest.status == BetStatus.LOST)
			return OutsideRouletteBet.BLACK to lastBetRequest.amount * 2
		return OutsideRouletteBet.RED to minimalBet
	}
}