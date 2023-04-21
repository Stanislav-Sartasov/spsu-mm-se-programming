package shoeTests

import card.*
import cardSuit.*
import cardValue.*
import shoe.*
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.Assertions.assertEquals
import java.util.*
import kotlin.random.Random.Default.nextInt

class ShoeTests {

	@Test
	fun shoeConstructorTest() {
		val expectedSet: MutableSet<Card> = mutableSetOf()
		for (i in 0..8) {
			for (cardValue in 0..12) {
				for (cardSuit in 0..3) {
					expectedSet.add(
						Card(
							CardValue.values()[cardValue],
							CardSuit.values()[cardSuit]
						)
					)
				}
			}
		}

		val testShoe = Shoe()
		val actualSet = MutableList<Card>(52 * 8) { testShoe.takeCard() }.toMutableSet()

		assertEquals(expectedSet, actualSet)
	}

	@Test
	fun takeCardTest() {
		val testShoe = Shoe()
		val cards: Queue<Card> = LinkedList(testShoe.cards)

		testShoe.takeCard()
		cards.remove()

		assertEquals(cards, testShoe.cards)
	}

	@Test
	fun reshuffleTest() {
		val testShoe = Shoe()

		repeat(nextInt(100)) {
			testShoe.takeCard()
		}
		val cards: Queue<Card> = LinkedList(testShoe.cards)

		testShoe.reshuffle()
		assert(cards != testShoe.cards && testShoe.cards.size == 8 * 52)
	}
}
