package blackjackPlayer

import blackjackHand.*
import card.*
import decision.*

interface BlackjackPlayer {
	val betAmount: Int
	var balance: Int

	fun getBet(): Int {
		if (balance < betAmount) return 0
		balance -= betAmount
		return betAmount
	}

	fun getDecision(playersHand: BlackjackHand, dealersFaceUpCard: Card): Decision
}
