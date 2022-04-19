package botMediumTests

import blackjackHand.BlackjackHand
import botMedium.BotMedium
import card.Card
import cardSuit.CardSuit
import cardValue.CardValue
import decision.Decision
import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Test
import kotlin.random.Random

class BotMediumTests {

	private fun calculate(cards: List<Card>): Pair<Int, Int> {
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

		return Pair(sum, aceElevenCount)
	}

	private fun getExpectedDecision(sum: Int, dealersFaceUpCard: Card): Decision {
		return if (sum == 12 && dealersFaceUpCard.rank.cardValue in 4..6 ||
			sum in 13 until 17 && dealersFaceUpCard.rank.cardValue in 2..6 ||
			sum >= 17
		) {
			Decision.STAND
		}
		else {
			Decision.HIT
		}
	}

	private fun getActualDecision(cards: List<Card>, dealersFaceUpCard: Card): Decision {
		val testBot = BotMedium()
		val testHand = BlackjackHand()
		for (card in cards) testHand.dealCard(card)

		return testBot.getDecision(testHand, dealersFaceUpCard)
	}

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

		val (sum, aceElevenCount) = calculate(cards)

		assertEquals(
			getExpectedDecision(sum, dealersFaceUpCard),
			getActualDecision(cards, dealersFaceUpCard)
		)
	}

	@Test
	fun getDecisionFullCoverageTest() {
		val cards = listOf(
			Card(CardValue.TEN, CardSuit.values()[Random.nextInt(4)]),
			Card(CardValue.TWO, CardSuit.values()[Random.nextInt(4)]),
			Card(CardValue.FIVE, CardSuit.values()[Random.nextInt(4)]),
		)
		val dealersFaceUpCard = Card(
			CardValue.values()[Random.nextInt(13)],
			CardSuit.values()[Random.nextInt(4)]
		)

		val (sum, aceElevenCount) = calculate(cards)

		assertEquals(
			getExpectedDecision(sum, dealersFaceUpCard),
			getActualDecision(cards, dealersFaceUpCard)
		)
	}
}