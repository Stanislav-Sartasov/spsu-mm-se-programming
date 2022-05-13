package botHardTests

import blackjackHand.BlackjackHand
import botHard.BotHard
import card.Card
import cardSuit.CardSuit
import cardValue.CardValue
import decision.Decision
import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Test
import kotlin.random.Random

class BotHardTests {

	private fun transformToStrategyMatrix(list: List<String>): List<List<Decision>> {
		return list.map { str ->
			str.split(" ").map { it ->
				when (it) {
					"SP" -> Decision.SPLIT
					"H" -> Decision.HIT
					else -> Decision.STAND
				}
			}
		}
	}

	private val expectedPairStrategy = transformToStrategyMatrix(
		listOf(
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H SP SP SP SP SP SP H H H H",
			"H H SP SP SP SP SP SP H H H H",
			"H H H H H SP SP H H H H H",
			"H H H H H H H H H H H H",
			"H H SP SP SP SP SP H H H H H",
			"H H SP SP SP SP SP SP H H H H",
			"SP SP SP SP SP SP SP SP SP SP SP SP",
			"S S SP SP SP SP SP S SP SP S S",
			"S S S S S S S S S S S S",
			"SP SP SP SP SP SP SP SP SP SP SP SP",
		)
	)

	private val expectedSoftStrategy = transformToStrategyMatrix(
		listOf(
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"S S S S S S S S S H H H",
			"S S S S S S S S S S S S",
			"S S S S S S S S S S S S",
		)
	)

	private val expectedHardStrategy = transformToStrategyMatrix(
		listOf(
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H H H H H H H H H",
			"H H H H S S S H H H H H",
			"H H S S S S S H H H H H",
			"H H S S S S S H H H H H",
			"H H S S S S S H H H H H",
			"H H S S S S S H H H H H",
			"S S S S S S S S S S S S",
			"S S S S S S S S S S S S",
			"S S S S S S S S S S S S",
			"S S S S S S S S S S S S",
			"S S S S S S S S S S S S",
		)
	)

	@Test
	fun getDecisionTest() {
		val cards = List(Random.nextInt(2, 5)) {
			Card(
				CardValue.values()[Random.nextInt(13)],
				CardSuit.values()[Random.nextInt(4)]
			)
		}
		val dealersFaceUpCard = Card(
			CardValue.values()[Random.nextInt(13)],
			CardSuit.values()[Random.nextInt(4)]
		)

		var aceElevenCount = 0
		var sum = 0
		for (card in cards) {
			sum += card.rank.cardValue
			if (card.rank == CardValue.ACE) {
				++aceElevenCount
			}
			while (sum > 21 && aceElevenCount > 0) {
				sum -= 10
				--aceElevenCount
			}
		}

		val expectedDecision: Decision = if (cards.size == 2 && cards[0].rank == cards[1].rank) {
			expectedPairStrategy[cards[0].rank.cardValue][dealersFaceUpCard.rank.cardValue]
		} else if (sum >= 21) {
			Decision.STAND
		} else if (aceElevenCount > 0) {
			expectedSoftStrategy[sum][dealersFaceUpCard.rank.cardValue]
		} else {
			expectedHardStrategy[sum][dealersFaceUpCard.rank.cardValue]
		}

		val testBot = BotHard()
		val testHand = BlackjackHand()
		for (card in cards) testHand.dealCard(card)

		assertEquals(expectedDecision, testBot.getDecision(testHand, dealersFaceUpCard))
	}
}