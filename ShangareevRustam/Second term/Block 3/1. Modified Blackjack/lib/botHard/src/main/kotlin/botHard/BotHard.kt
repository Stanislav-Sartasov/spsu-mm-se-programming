package botHard

import blackjackHand.*
import blackjackPlayer.BlackjackPlayer
import decision.*
import card.Card

class BotHard : BlackjackPlayer {
	override val betAmount: Int = 10
	override var balance: Int = 1000

	private val hardStrategy: List<List<Decision>> = List(5) { List(12) { Decision.HIT } } +
			List(7) { List(12) { Decision.HIT } } +
			List(1) { List(12) { i -> if (i !in 4..6) Decision.HIT else Decision.STAND } } +
			List(4) { List(12) { i -> if (i !in 2..6) Decision.HIT else Decision.STAND } } +
			List(5) { List(12) { Decision.STAND } }
	private val softStrategy: List<List<Decision>> = List(13) { List(12) { Decision.HIT } } +
			List(5) { List(12) { Decision.HIT } } +
			List(1) { List(12) { i -> if (i in 9..12) Decision.HIT else Decision.STAND } } +
			List(2) { List(12) { Decision.STAND } }
	private val pairStrategy: List<List<Decision>> = List(2) { List(12) { Decision.HIT } } +
			List(2) { List(12) { i -> if (i in 2..7) Decision.SPLIT else Decision.HIT } } +
			List(1) { List(12) { i -> if (i in 5..6) Decision.SPLIT else Decision.HIT } } +
			List(1) { List(12) { Decision.HIT } } +
			List(1) { List(12) { i -> if (i in 2..6) Decision.SPLIT else Decision.HIT } } +
			List(1) { List(12) { i -> if (i in 2..7) Decision.SPLIT else Decision.HIT } } +
			List(1) { List(12) { Decision.SPLIT } } +
			List(1) { List(12) { i -> if (i in 2..6 || i in 8..9) Decision.SPLIT else Decision.STAND } } +
			List(1) { List(12) { Decision.STAND } } +
			List(1) { List(12) { Decision.SPLIT } }


	override fun getDecision(playersHand: BlackjackHand, dealersFaceUpCard: Card): Decision {
		val firstCardValue = playersHand.listOfCards[0].rank.cardValue
		val dealersCardValue = dealersFaceUpCard.rank.cardValue

		with(playersHand) {
			if (listOfCards.size == 2 &&
				listOfCards[0].rank.cardValue == listOfCards[1].rank.cardValue &&
					betAmount <= balance) {
				return pairStrategy[firstCardValue][dealersCardValue]
			}

			stableSum()

			return if (sumOfCardValues >= 21) {
				Decision.STAND
			} else if (aceElevenCount > 0) {
				softStrategy[sumOfCardValues][dealersCardValue]
			} else {
				hardStrategy[sumOfCardValues][dealersCardValue]
			}
		}
	}
}