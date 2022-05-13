package dealer

import bettingBox.*
import blackjackHand.*
import blackjackPlayer.*
import blackjackTable.BlackjackTable
import decision.*
import shoe.*
import gameResult.*

class Dealer {
	internal val hand: BlackjackHand = BlackjackHand()

	fun playBlackjack(listOfPlayers: List<BlackjackPlayer>, table: BlackjackTable) {
		with (table) {
			bettingBoxes.clear()
			hand.clear()

			acceptBets(listOfPlayers, bettingBoxes)
			dealCards(bettingBoxes, shoe)
			playWithBoxes(bettingBoxes, shoe)
			resolveHand(shoe)
			sumUpTheGame(bettingBoxes)
		}
	}

	private fun acceptBets(listOfPlayers: List<BlackjackPlayer>, bettingBoxes: MutableList<BettingBox>) {
		for (player in listOfPlayers) {
			val newBettingBox = BettingBox(player)
			if (newBettingBox.bet != 0) {
				bettingBoxes.add(newBettingBox)
			}
		}
	}

	private fun dealCards(bettingBoxes: MutableList<BettingBox>, shoe: Shoe) {
		repeat(2) {
			for (box in bettingBoxes) box.playersHand.dealCard(shoe.takeCard())
			hand.dealCard(shoe.takeCard())
		}
	}

	private fun splitBox(bettingBoxes: MutableList<BettingBox>, box: BettingBox) {
		val newBox = BettingBox(box.player)
		box.playersHand.splitCards(newBox.playersHand)
		bettingBoxes.add(newBox)
	}

	private fun playWithBoxes(bettingBoxes: MutableList<BettingBox>, shoe: Shoe) {
		val dealersFaceUpCard = hand.listOfCards[0]
		var counter = 0

		while (true) {
			if (counter >= bettingBoxes.size) break
			val box = bettingBoxes[counter]

			var playersDecision = box.player.getDecision(box.playersHand, dealersFaceUpCard)
			while (playersDecision != Decision.STAND) {
				if (playersDecision == Decision.HIT) {
					box.playersHand.dealCard(shoe.takeCard())
				} else {
					splitBox(bettingBoxes, box)
					box.playersHand.dealCard(shoe.takeCard())
					bettingBoxes.last().playersHand.dealCard(shoe.takeCard())
				}
				playersDecision = box.player.getDecision(box.playersHand, dealersFaceUpCard)
			}

			bettingBoxes[counter] = box
			counter++
		}
	}

	private fun resolveHand(shoe: Shoe) {
		with(hand) {
			while (sumOfCardValues < 17 || (sumOfCardValues == 17 && aceElevenCount > 0)) {
				dealCard(shoe.takeCard())
				stableSum()
			}
		}
	}

	private fun getResultOfTheGame(box: BettingBox): GameResult {
		return if (box.playersHand.listOfCards.size == 2 && box.playersHand.sumOfCardValues == 21 &&
			(hand.listOfCards.size != 2 || hand.sumOfCardValues != 21)
		) {
			GameResult.BLACKJACK
		} else if (hand.sumOfCardValues == box.playersHand.sumOfCardValues ||
			hand.sumOfCardValues > 21 && box.playersHand.sumOfCardValues > 21
		) {
			GameResult.DRAW
		} else if (box.playersHand.sumOfCardValues <= 21 &&
			(hand.sumOfCardValues > 21 || hand.sumOfCardValues < box.playersHand.sumOfCardValues )) {
			GameResult.DEFEAT
		} else {
			GameResult.VICTORY
		}
	}

	private fun sumUpTheGame(bettingBoxes: MutableList<BettingBox>) {
		for (box in bettingBoxes) {
			val result = getResultOfTheGame(box)
			when (result.name) {
				"BLACKJACK" -> box.player.balance += (5 * box.bet) / 2
				"DRAW" -> box.player.balance += box.bet
				"DEFEAT" -> box.player.balance += 2 * box.bet
			}
		}
	}
}