package bot.bet_strategy

import request.BetRequest
import java.util.*

abstract class QueueBetStrategy(
	protected val betRequestHistory: Queue<BetRequest> = ArrayDeque()
) : BetStrategy {
	override fun add(betRequest: BetRequest) {
		betRequestHistory.add(betRequest)
	}
}