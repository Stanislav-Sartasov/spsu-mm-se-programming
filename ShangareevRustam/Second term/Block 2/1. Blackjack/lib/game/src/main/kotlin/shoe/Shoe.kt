package shoe

import card.*
import cardSuit.*
import cardValue.*
import java.util.*

class Shoe {
	private val deck = List<Card>(52) { index -> Card(CardValue.values()[index % 13], CardSuit.values()[index % 4]) }
	var cards: Queue<Card> = LinkedList<Card>(MutableList(8 * 52) { index -> deck[index % 52] }.shuffled())
		private set

	internal fun takeCard(): Card {
		return cards.remove()
	}

	internal fun reshuffle() {
		cards = LinkedList<Card>(MutableList(8 * 52) { index -> deck[index % 52] }.shuffled())
	}
}