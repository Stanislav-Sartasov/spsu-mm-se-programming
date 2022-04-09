package blackjackTable

import bettingBox.*
import blackjackPlayer.*
import dealer.*
import shoe.*

class BlackjackTable(
	internal val shoe: Shoe = Shoe(),
	internal val bettingBoxes: MutableList<BettingBox> = mutableListOf()
) {
	private val numbOfBids = 40

	fun playBlackjackAndPrintResult(listOfPlayers: List<BlackjackPlayer>, numbOfGames: Int) {
		val gameDealer = Dealer()
		val listOfStartBalances: MutableList<Int> = mutableListOf()
		val listOfResults: MutableList<Int> = mutableListOf()
		for (player in listOfPlayers) {
			listOfStartBalances.add(player.balance)
			listOfResults.add(0)
		}

		for (i in 1..numbOfGames) {
			for (j in 1..numbOfBids) {
				if (shoe.cards.size <= 52 * 3) shoe.reshuffle()
				gameDealer.playBlackjack(listOfPlayers, this)
			}
			for (k in listOfPlayers.indices) {
				listOfResults[k] += listOfPlayers[k].balance - listOfStartBalances[k]
				listOfPlayers[k].balance = listOfStartBalances[k]
			}
		}

		println("-------------------------------------------")
		for (i in listOfResults.indices) {
			println(
				"Player No.${i + 1} on average after $numbOfBids bets of" +
						" \$ ${listOfPlayers[i].betAmount}" +
						" and an initial balance of \$${listOfStartBalances[i]} has " +
						"${listOfResults[i].toDouble() / numbOfGames + listOfStartBalances[i]}"
			)
		}
		println("-------------------------------------------")
	}
}