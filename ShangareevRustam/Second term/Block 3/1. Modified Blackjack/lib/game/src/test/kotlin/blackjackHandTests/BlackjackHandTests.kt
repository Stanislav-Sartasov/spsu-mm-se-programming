package blackjackHandTests

import blackjackHand.BlackjackHand
import card.Card
import cardSuit.CardSuit
import cardValue.CardValue
import org.junit.jupiter.api.Assertions
import org.junit.jupiter.api.Test
import kotlin.random.Random

class BlackjackHandTests {

	private fun Boolean.toInt() = if (this) 1 else 0

	private fun getRandomCard(): Card {
		return Card(
			CardValue.values()[Random.nextInt(0, 13)],
			CardSuit.values()[Random.nextInt(0, 4)]
		)
	}

	@Test
	fun dealCardTest() {
		val testHand = BlackjackHand()
		val card = getRandomCard()
		testHand.dealCard(card)

		Assertions.assertEquals(
			listOf(listOf(card), card.rank.cardValue, (card.rank == CardValue.ACE).toInt()),
			listOf(testHand.listOfCards, testHand.sumOfCardValues, testHand.aceElevenCount)
		)
	}

	@Test
	fun splitCardsTest() {
		val testHand = BlackjackHand()
		val testHand2 = BlackjackHand()

		val firstCard = getRandomCard()
		val secondCard = Card(firstCard.rank, CardSuit.values()[Random.nextInt(0, 4)])
		testHand.dealCard(firstCard)
		testHand.dealCard(secondCard)

		testHand.splitCards(testHand2)

		Assertions.assertEquals(
			listOf(
				listOf(firstCard), firstCard.rank.cardValue, (firstCard.rank == CardValue.ACE).toInt(),
				listOf(secondCard), secondCard.rank.cardValue, (secondCard.rank == CardValue.ACE).toInt()
			),
			listOf(
				testHand.listOfCards, testHand.sumOfCardValues, testHand.aceElevenCount,
				testHand2.listOfCards, testHand2.sumOfCardValues, testHand2.aceElevenCount
			)
		)
	}
}