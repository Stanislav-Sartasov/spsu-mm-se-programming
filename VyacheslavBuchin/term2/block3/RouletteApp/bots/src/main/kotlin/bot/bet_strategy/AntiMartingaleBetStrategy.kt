package bot.bet_strategy

import bet.Bet
import bet.OutsideRouletteBet
import util.BetStatus

class AntiMartingaleBetStrategy(minimalBet: Double) : MartingaleBasedBetStrategy(minimalBet) {
	override fun nextBet(): Pair<Bet, Double> {
		if (betRequestHistory.isEmpty())
			return OutsideRouletteBet.BLACK to minimalBet
		val lastBetRequest = betRequestHistory.element()
		if (lastBetRequest.status == BetStatus.WON)
			return OutsideRouletteBet.RED to lastBetRequest.amount * 2
		return OutsideRouletteBet.BLACK to minimalBet
	}
}