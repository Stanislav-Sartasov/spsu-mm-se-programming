package dealerTests

import bettingBox.BettingBox
import blackjackPlayer.BlackjackPlayer
import blackjackTable.BlackjackTable
import decision.*
import card.*
import cardSuit.*
import cardValue.*
import dealer.*
import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Test
import java.util.*
import kotlin.random.Random.Default.nextInt
import shoe.*
import testPlayer.*

class DealerTests {

	private fun getRandomListsOfPlayers(): Pair<List<BlackjackPlayer>, List<BlackjackPlayer>> {
		val expectedListOfPlayers = List(nextInt(100)) {
			TestPlayer(nextInt( 1000), nextInt(1, 1000), nextInt(1,21))
		}
		val actualListOfPlayers = List(expectedListOfPlayers.size) { i ->
			TestPlayer(
				expectedListOfPlayers[i].balance,
				expectedListOfPlayers[i].betAmount,
				expectedListOfPlayers[i].keyValue
			)
		}

		return Pair(expectedListOfPlayers, actualListOfPlayers)
	}

	private fun expectedAcceptBetsAndDealCards(expectedListOfPlayers: List<BlackjackPlayer>) {
		for (player in expectedListOfPlayers) {
			if (player.betAmount <= player.balance) {
				expectedBettingBoxes.add(BettingBox(player))
			}
		}
		repeat(2) {
			for (box in expectedBettingBoxes) {
				box.playersHand.dealCard(cards.remove())
			}
			expectedDealer.hand.dealCard(cards.remove())
		}
	}

	private fun expectedSplitBoxesAndPlayWithBoxes() {
		var newNeedToPlayBoxes = mutableListOf<BettingBox>()
		var needToPlayBoxes = expectedBettingBoxes.toMutableList()
		expectedBettingBoxes.clear()
		while (needToPlayBoxes.isNotEmpty()) {
			for (box in needToPlayBoxes) {
				var decision = box.player.getDecision(
					box.playersHand,
					Card(CardValue.ACE, CardSuit.CLUBS)
				)
				while (decision != Decision.STAND) {
					if (decision == Decision.SPLIT) {
						newNeedToPlayBoxes.add(BettingBox(box.player))
						box.playersHand.splitCards(newNeedToPlayBoxes.last().playersHand)
						box.playersHand.dealCard(cards.remove())
						newNeedToPlayBoxes.last().playersHand.dealCard(cards.remove())
					} else {
						box.playersHand.dealCard(cards.remove())
					}
					decision = box.player.getDecision(
						box.playersHand,
						Card(CardValue.ACE, CardSuit.CLUBS)
					)
				}
			}
			expectedBettingBoxes += needToPlayBoxes.toMutableList()
			needToPlayBoxes = newNeedToPlayBoxes.toMutableList()
			newNeedToPlayBoxes.clear()
		}
	}

	private fun expectedResolveHand() {
		with(expectedDealer.hand) {
			while (sumOfCardValues < 17 || (sumOfCardValues == 17 && aceElevenCount > 0)) {
				dealCard(cards.remove())
				stableSum()
			}
		}
	}

	private fun expectedSumUpTheGame() {
		for (box in expectedBettingBoxes) {
			val dealersSum = expectedDealer.hand.sumOfCardValues
			val dealersSize = expectedDealer.hand.listOfCards.size
			val boxSum = box.playersHand.sumOfCardValues
			val boxSize = box.playersHand.listOfCards.size

			if (boxSize == 2 && boxSum == 21 && !(dealersSum == 21 && dealersSize == 2)) {
				box.player.balance += (box.bet * 5) / 2
			} else if (boxSum <= 21 && (dealersSum > 21 || dealersSum < boxSum)) {
				box.player.balance += 2 * box.bet
			} else if (dealersSum == boxSum || dealersSum > 21 && boxSum > 21) {
				box.player.balance += box.bet
			}
		}
	}

	private val testShoe: Shoe
	private val actualDealer: Dealer
	private val actualBettingBoxes: MutableList<BettingBox>
	private val testTable: BlackjackTable

	private val cards: Queue<Card>
	private val expectedDealer: Dealer
	private val expectedBettingBoxes: MutableList<BettingBox>

	init {
		val (expectedListOfPlayers, actualListOfPlayers) = getRandomListsOfPlayers()

		testShoe = Shoe()
		actualDealer = Dealer()
		actualBettingBoxes = mutableListOf<BettingBox>()
		testTable = BlackjackTable(testShoe, actualBettingBoxes)

		cards = LinkedList(testShoe.cards)
		expectedDealer = Dealer()
		expectedBettingBoxes = mutableListOf<BettingBox>()

		expectedAcceptBetsAndDealCards(expectedListOfPlayers)
		expectedSplitBoxesAndPlayWithBoxes()
		expectedResolveHand()
		expectedSumUpTheGame()

		actualDealer.playBlackjack(actualListOfPlayers, testTable)
	}

	@Test
	fun acceptBetsAndDealCardsTest() {
		assertEquals(expectedBettingBoxes.map {
			listOf(
				it.playersHand.listOfCards[0],
				it.playersHand.listOfCards[1]
			)
		},
			actualBettingBoxes.map {
				listOf(
					it.playersHand.listOfCards[0],
					it.playersHand.listOfCards[1]
				)
			}
		)
	}

	@Test
	fun playWithBoxesAndSplitBoxesTest() {
		assertEquals(expectedBettingBoxes.map {
			listOf(
				it.playersHand.listOfCards,
				it.playersHand.sumOfCardValues,
				it.playersHand.aceElevenCount
			)
		}, actualBettingBoxes.map {
			listOf(
				it.playersHand.listOfCards,
				it.playersHand.sumOfCardValues,
				it.playersHand.aceElevenCount
			)
		})
	}

	@Test
	fun dealersHandTest() {
		assertEquals(
			listOf(
				expectedDealer.hand.listOfCards,
				expectedDealer.hand.sumOfCardValues,
				expectedDealer.hand.aceElevenCount
			),
			listOf(
				actualDealer.hand.listOfCards,
				actualDealer.hand.sumOfCardValues,
				actualDealer.hand.aceElevenCount
			)
		)
	}

	@Test
	fun sumUpTheGameTest() {
		assertEquals(expectedBettingBoxes.map {
			listOf(
				it.player.balance,
				it.player.betAmount
			)
		},
			actualBettingBoxes.map {
				listOf(
					it.player.balance,
					it.player.betAmount
				)
			}
		)
	}
}