package casino.lib.shoe

import casino.lib.card.Card
import org.junit.jupiter.api.*
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.ValueSource
import kotlin.test.*

internal class StackShoeTest {

    private lateinit var shoe: Shoe

    @BeforeEach
    fun init() {
        shoe = StackShoe(cards = List(size = 1) { Card.deck }.flatten())
    }


    @ParameterizedTest
    @ValueSource(ints = [0, 1, 2, 3, 52])
    fun `get dealt cards from shoe`(times: Int) {
        val cards = List(times) { shoe.dealCard() }
        assertEquals(expected = cards, actual = shoe.dealt)
    }


    // Deal card

    @ParameterizedTest
    @ValueSource(ints = [0, 1, 2, 3, 52])
    fun `successfully deal several cards from a shoe`(times: Int) {
        val cards = assertDoesNotThrow { List(times) { shoe.dealCard() } }
        assertEquals(expected = Card.deck.take(times), actual = cards)
    }

    @Test
    fun `fail to deal a card from an empty shoe`() {
        assertFails { repeat(times = 53) { shoe.dealCard() } }
    }


    // Is not empty

    @ParameterizedTest
    @ValueSource(ints = [0, 1, 2, 3, 51])
    fun `check if non-empty shoe is not empty`(times: Int) {
        repeat(times) { shoe.dealCard() }
        assertTrue { shoe.isNotEmpty() }
    }

    @Test
    fun `check if empty shoe is empty`() {
        repeat(times = 52) { shoe.dealCard() }
        assertFalse { shoe.isNotEmpty() }
    }
}
