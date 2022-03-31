package casino.lib.shoe

import org.junit.jupiter.api.*
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.ValueSource
import kotlin.test.*

internal class ShuffledShoeTest {

    lateinit var shoe: Shoe

    @BeforeEach
    fun init() {
        shoe = ShoesFabric.shuffled(numberOfDecks = 1)
    }


    @ParameterizedTest
    @ValueSource(ints = [0, 1, 2, 3, 52])
    fun `get dealt cards from shoe`(times: Int) {
        val cards = List(times) { shoe.dealCard() }
        assertEquals(expected = cards, actual = shoe.dealt)
    }

    @Nested
    inner class DealCard {

        @ParameterizedTest
        @ValueSource(ints = [0, 1, 2, 3, 52])
        fun `successfully deal several cards from a shoe`(times: Int) {
            val cards = assertDoesNotThrow { List(times) { shoe.dealCard() } }
            assertTrue { cards.size == cards.toSet().size }
        }

        @Test
        fun `fail to deal a card from an empty shoe`() {
            assertFails { repeat(times = 53) { shoe.dealCard() } }
        }
    }

    @Nested
    inner class IsNotEmpty {

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

    @Nested
    inner class ShuffledShoeCreation {

        @ParameterizedTest
        @ValueSource(ints = [1, 2, 3, 4, 5, 6, 7, 8])
        fun `create shoe of correct size`(n: Int) {
            assertDoesNotThrow { ShoesFabric.shuffled(n) }
        }

        @Test
        fun `fail to create shoe of 0 size`() {
            assertFailsWith<IllegalArgumentException>(message = "Shoe's size must be in the range of 1 to 8 decks") {
                ShoesFabric.shuffled(numberOfDecks = 0)
            }
        }

        @Test
        fun `fail to create shoe of negative size`() {
            assertFailsWith<IllegalArgumentException>(message = "Shoe's size must be in the range of 1 to 8 decks") {
                ShoesFabric.shuffled(numberOfDecks = -1)
            }
        }

        @Test
        fun `fail to create too big shoe`() {
            assertFailsWith<IllegalArgumentException>(message = "Shoe's size must be in the range of 1 to 8 decks") {
                ShoesFabric.shuffled(numberOfDecks = 9)
            }
        }
    }
}
