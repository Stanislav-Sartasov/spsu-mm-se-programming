package botEasyTests

import blackjackHand.BlackjackHand
import botEasy.BotEasy
import org.junit.jupiter.api.Test
import kotlin.random.Random.Default.nextInt
import card.*
import cardValue.*
import cardSuit.*
import decision.Decision
import org.junit.jupiter.api.Assertions.assertEquals

class BotEasyTests {

	@Test
	fun getDecisionTest() {

		val cards = List(nextInt(2, 4)) {
			Card(CardValue.values()[nextInt(13)], CardSuit.values()[nextInt(4)])
		}
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
		val expectedDecision = if (sum >= 17) Decision.STAND else Decision.HIT

		val testBot = BotEasy()
		val testHand = BlackjackHand()
		for (card in cards) testHand.dealCard(card)

		assertEquals(
			expectedDecision,
			testBot.getDecision(
				testHand,
				Card(CardValue.values()[nextInt(13)], CardSuit.values()[nextInt(4)])
			)
		)
	}
}