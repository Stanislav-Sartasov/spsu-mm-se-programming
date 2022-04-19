package testPlayer

import blackjackHand.BlackjackHand
import blackjackPlayer.BlackjackPlayer
import card.Card
import decision.Decision

class TestPlayer(
	override var balance: Int,
	override val betAmount: Int,
	val keyValue: Int = 0
) : BlackjackPlayer {

	override fun getDecision(playersHand: BlackjackHand, dealersFaceUpCard: Card): Decision {
		with(playersHand) {
			if (listOfCards.size == 2 &&
				listOfCards[0].rank == listOfCards[1].rank &&
				betAmount <= balance
			) {
				return Decision.SPLIT
			}
		}

		playersHand.stableSum()
		if (playersHand.sumOfCardValues > 21) return Decision.STAND

		return if (playersHand.sumOfCardValues >= keyValue) Decision.STAND
		else Decision.HIT
	}
}