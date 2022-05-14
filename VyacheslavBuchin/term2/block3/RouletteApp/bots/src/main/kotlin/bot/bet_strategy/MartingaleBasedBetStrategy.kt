package bot.bet_strategy

import request.BetRequest

abstract class MartingaleBasedBetStrategy(
	protected val minimalBet: Double
) : QueueBetStrategy() {

	override fun add(betRequest: BetRequest) {
		betRequestHistory.poll()
		super.add(betRequest)
	}
}