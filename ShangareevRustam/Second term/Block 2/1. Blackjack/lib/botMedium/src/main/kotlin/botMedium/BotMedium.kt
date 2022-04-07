package botMedium

import blackjackHand.*
import blackjackPlayer.*
import decision.*
import card.Card

class BotMedium : BlackjackPlayer {
	override val betAmount: Int = 10
	override var balance: Int = 1000

	override fun getDecision(playersHand: BlackjackHand, dealersFaceUpCard: Card): Decision {
		val dealersCardValue = dealersFaceUpCard.rank.cardValue
		with(playersHand) {
			stableSum()

			return if (sumOfCardValues < 12) {
				Decision.HIT
			} else if (sumOfCardValues == 12) {
				if (dealersCardValue in 4..6) Decision.STAND
				else Decision.HIT
			} else if (sumOfCardValues < 17) {
				if (dealersCardValue in 2..6) Decision.STAND
				else Decision.HIT
			} else {
				Decision.STAND
			}
		}

	}
}