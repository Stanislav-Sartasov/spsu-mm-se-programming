package blackjackHand

import card.*
import cardValue.*

class BlackjackHand {
	val listOfCards: MutableList<Card> = mutableListOf()
	var aceElevenCount: Int = 0
		private set
	var sumOfCardValues: Int = 0
		private set

	fun dealCard(card: Card) {
		if (card.rank == CardValue.ACE) ++aceElevenCount
		listOfCards.add(card)
		sumOfCardValues += card.rank.cardValue
	}

	fun splitCards(newHand: BlackjackHand) {
		val transferredCard = listOfCards.removeLast()
		newHand.dealCard(transferredCard)
		sumOfCardValues -= transferredCard.rank.cardValue
		if (transferredCard.rank == CardValue.ACE) {
			--aceElevenCount
		}
	}

	fun clear() {
		listOfCards.clear()
		aceElevenCount = 0
		sumOfCardValues = 0
	}

	fun stableSum() {
		while (sumOfCardValues > 21 && aceElevenCount > 0) {
			sumOfCardValues -= 10
			--aceElevenCount
		}
	}
}