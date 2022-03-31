package casino.lib.card

import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals

internal class CardTest {

    @ParameterizedTest
    @MethodSource("cardsWithSuites")
    fun `get card's suite`(data: Pair<Card, CardSuite>) {
        val (card, suite) = data
        assertEquals(expected = suite, actual = card.suite)
    }

    @ParameterizedTest
    @MethodSource("cardsWithRanks")
    fun `get card's rank`(data: Pair<Card, CardRank>) {
        val (card, rank) = data
        assertEquals(expected = rank, actual = card.rank)
    }

    @ParameterizedTest
    @MethodSource("cardsWithSuites")
    fun `get card's suite via component 1`(data: Pair<Card, CardSuite>) {
        val (card, suite) = data
        assertEquals(expected = suite, actual = card.component1())
    }

    @ParameterizedTest
    @MethodSource("cardsWithRanks")
    fun `get card's rank via component 2`(data: Pair<Card, CardRank>) {
        val (card, rank) = data
        assertEquals(expected = rank, actual = card.component2())
    }

    @Test
    fun `get deck of cards`() {
        assertEquals(expected = cards, actual = Card.deck)
    }


    private companion object {

        @JvmStatic
        fun cardsWithSuites() = suitesWithRanks.map { (s, r) -> Card.get(s, r) to s }

        @JvmStatic
        fun cardsWithRanks() = suitesWithRanks.map { (s, r) -> Card.get(s, r) to r }


        val suitesWithRanks = CardSuite.values().flatMap { s -> CardRank.values().map { r -> s to r } }

        val cards = suitesWithRanks.map { (s, r) -> Card.get(s, r) }
    }
}
