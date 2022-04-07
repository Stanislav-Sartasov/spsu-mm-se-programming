package botEasy

import blackjackHand.*
import blackjackPlayer.*
import decision.*
import card.*

class BotEasy : BlackjackPlayer {
	override val betAmount: Int = 10
	override var balance: Int = 1000

	override fun getDecision(playersHand: BlackjackHand, dealersFaceUpCard: Card): Decision {
		with(playersHand) {
			stableSum()

			return if (sumOfCardValues >= 17) Decision.STAND
			else Decision.HIT
		}
	}
}