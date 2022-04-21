package casino.lib.blackjack

import casino.lib.card.*
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.math.sign
import kotlin.test.assertEquals

internal class HandTest {

    @ParameterizedTest
    @MethodSource("handsWithTotals")
    fun `calculate hand's total`(data: Pair<Hand, Int>) {
        val (hand, total) = data
        assertEquals(expected = total, actual = hand.total())
    }

    @ParameterizedTest
    @MethodSource("handsWithIsSoft")
    fun `check if hand has soft total`(data: Pair<Hand, Boolean>) {
        val (hand, isSoft) = data
        assertEquals(expected = isSoft, actual = hand.isSoft())
    }

    @ParameterizedTest
    @MethodSource("handsWithIsHard")
    fun `check if hand has hard total`(data: Pair<Hand, Boolean>) {
        val (hand, isHard) = data
        assertEquals(expected = isHard, actual = hand.isHard())
    }

    @ParameterizedTest
    @MethodSource("handsWithIsBJ")
    fun `check if hand has black jack`(data: Pair<Hand, Boolean>) {
        val (hand, isBJ) = data
        assertEquals(expected = isBJ, actual = hand.isBJ())
    }

    @ParameterizedTest
    @MethodSource("handsWithIsBust")
    fun `check if hand is bust`(data: Pair<Hand, Boolean>) {
        val (hand, isBust) = data
        assertEquals(expected = isBust, actual = hand.isBust())
    }

    @ParameterizedTest
    @MethodSource("handsPairsWithCompareSigns")
    fun `compare two hands`(data: Triple<Hand, Hand, Int>) {
        val (leftHand, rightHand, compareSign) = data
        assertEquals(expected = compareSign, actual = leftHand.compareTo(rightHand).sign)
    }


    private companion object {

        @JvmStatic
        fun handsWithTotals() = listOf(
            listOf<Card>() to 0,
            listOf(TWO_OF_CLUBS) to 2,
            listOf(ACE_OF_SPADES) to 11,
            listOf(QUEEN_OF_SPADES) to 10,
            listOf(ACE_OF_SPADES, QUEEN_OF_SPADES) to 21,
            listOf(ACE_OF_HEARTS, QUEEN_OF_SPADES) to 21,
            listOf(TEN_OF_CLUBS, ACE_OF_HEARTS) to 21,
            listOf(NINE_OF_HEARTS, TWO_OF_CLUBS, QUEEN_OF_SPADES) to 21,
            listOf(ACE_OF_HEARTS, SIX_OF_DIAMONDS, TWO_OF_CLUBS, TWO_OF_CLUBS) to 21,
            listOf(SIX_OF_DIAMONDS, SIX_OF_DIAMONDS, QUEEN_OF_SPADES, TWO_OF_CLUBS) to 24,
            listOf(ACE_OF_HEARTS, ACE_OF_SPADES) to 12,
            listOf(ACE_OF_HEARTS, ACE_OF_SPADES, QUEEN_OF_SPADES) to 12,
            listOf(ACE_OF_HEARTS, ACE_OF_SPADES, QUEEN_OF_SPADES, QUEEN_OF_SPADES) to 22,
            listOf(ACE_OF_HEARTS, ACE_OF_SPADES, ACE_OF_SPADES, ACE_OF_SPADES, ACE_OF_SPADES, ACE_OF_SPADES) to 16,
        ).map { (a, b) -> Hand(a) to b }

        @JvmStatic
        fun handsWithIsSoft() = listOf(
            listOf<Card>() to false,
            listOf(TWO_OF_CLUBS) to false,
            listOf(ACE_OF_SPADES) to true,
            listOf(QUEEN_OF_SPADES) to false,
            listOf(ACE_OF_SPADES, QUEEN_OF_SPADES) to true,
            listOf(ACE_OF_HEARTS, QUEEN_OF_SPADES) to true,
            listOf(TEN_OF_CLUBS, ACE_OF_HEARTS) to true,
            listOf(NINE_OF_HEARTS, TWO_OF_CLUBS, QUEEN_OF_SPADES) to false,
            listOf(ACE_OF_HEARTS, SIX_OF_DIAMONDS, TWO_OF_CLUBS, TWO_OF_CLUBS) to true,
            listOf(SIX_OF_DIAMONDS, SIX_OF_DIAMONDS, QUEEN_OF_SPADES, TWO_OF_CLUBS) to false,
            listOf(ACE_OF_HEARTS, ACE_OF_SPADES) to true,
            listOf(ACE_OF_HEARTS, ACE_OF_SPADES, QUEEN_OF_SPADES) to false,
            listOf(ACE_OF_HEARTS, ACE_OF_SPADES, QUEEN_OF_SPADES, QUEEN_OF_SPADES) to false,
            listOf(ACE_OF_HEARTS, ACE_OF_SPADES, ACE_OF_SPADES, ACE_OF_SPADES, ACE_OF_SPADES, ACE_OF_SPADES) to true,
        ).map { (a, b) -> Hand(a) to b }

        @JvmStatic
        fun handsWithIsHard() = handsWithIsSoft().map { (a, b) -> a to !b }

        @JvmStatic
        fun handsWithIsBJ() = listOf(
            listOf<Card>() to false,
            listOf(TWO_OF_CLUBS) to false,
            listOf(ACE_OF_SPADES) to false,
            listOf(QUEEN_OF_SPADES) to false,
            listOf(ACE_OF_SPADES, QUEEN_OF_SPADES) to true,
            listOf(ACE_OF_HEARTS, QUEEN_OF_SPADES) to true,
            listOf(TEN_OF_CLUBS, ACE_OF_HEARTS) to true,
            listOf(NINE_OF_HEARTS, TWO_OF_CLUBS, QUEEN_OF_SPADES) to false,
            listOf(ACE_OF_HEARTS, SIX_OF_DIAMONDS, TWO_OF_CLUBS, TWO_OF_CLUBS) to false,
        ).map { (a, b) -> Hand(a) to b }

        @JvmStatic
        fun handsWithIsBust() = listOf(
            listOf<Card>() to false,
            listOf(TWO_OF_CLUBS) to false,
            listOf(ACE_OF_SPADES) to false,
            listOf(QUEEN_OF_SPADES) to false,
            listOf(ACE_OF_SPADES, QUEEN_OF_SPADES) to false,
            listOf(ACE_OF_HEARTS, QUEEN_OF_SPADES) to false,
            listOf(TEN_OF_CLUBS, ACE_OF_HEARTS) to false,
            listOf(NINE_OF_HEARTS, TWO_OF_CLUBS, QUEEN_OF_SPADES) to false,
            listOf(ACE_OF_HEARTS, SIX_OF_DIAMONDS, TWO_OF_CLUBS, TWO_OF_CLUBS) to false,
            listOf(SIX_OF_DIAMONDS, SIX_OF_DIAMONDS, QUEEN_OF_SPADES, TWO_OF_CLUBS) to true,
            listOf(ACE_OF_HEARTS, ACE_OF_SPADES) to false,
            listOf(ACE_OF_HEARTS, ACE_OF_SPADES, QUEEN_OF_SPADES) to false,
            listOf(ACE_OF_HEARTS, ACE_OF_SPADES, QUEEN_OF_SPADES, QUEEN_OF_SPADES) to true,
            listOf(ACE_OF_HEARTS, ACE_OF_SPADES, ACE_OF_SPADES, ACE_OF_SPADES, ACE_OF_SPADES, ACE_OF_SPADES) to false,
        ).map { (a, b) -> Hand(a) to b }

        @JvmStatic
        fun handsPairsWithCompareSigns() = listOf(
            Triple(listOf(), listOf(), 0),
            Triple(listOf(TWO_OF_CLUBS), listOf(), 1),
            Triple(listOf(), listOf(TWO_OF_CLUBS), -1),
            Triple(listOf(TWO_OF_CLUBS), listOf(ACE_OF_SPADES), -1),
            Triple(listOf(NINE_OF_HEARTS, TWO_OF_CLUBS), listOf(ACE_OF_HEARTS), 0),
            Triple(listOf(TEN_OF_CLUBS, ACE_OF_HEARTS), listOf(NINE_OF_HEARTS, TWO_OF_CLUBS, QUEEN_OF_SPADES), 1),
            Triple(listOf(TEN_OF_CLUBS, ACE_OF_HEARTS), listOf(TEN_OF_CLUBS, ACE_OF_HEARTS), 0),
            Triple(listOf(ACE_OF_HEARTS, ACE_OF_SPADES, QUEEN_OF_SPADES, QUEEN_OF_SPADES), listOf(), -1),
        ).map { (a, b, c) -> Triple(Hand(a), Hand(b), c) }


        val TWO_OF_CLUBS = Card.get(suite = CardSuite.CLUBS, rank = CardRank.TWO)
        val ACE_OF_SPADES = Card.get(suite = CardSuite.SPADES, rank = CardRank.ACE)
        val ACE_OF_HEARTS = Card.get(suite = CardSuite.HEARTS, rank = CardRank.ACE)
        val QUEEN_OF_SPADES = Card.get(suite = CardSuite.SPADES, rank = CardRank.QUEEN)
        val SIX_OF_DIAMONDS = Card.get(suite = CardSuite.DIAMONDS, rank = CardRank.SIX)
        val NINE_OF_HEARTS = Card.get(suite = CardSuite.HEARTS, rank = CardRank.NINE)
        val TEN_OF_CLUBS = Card.get(suite = CardSuite.CLUBS, rank = CardRank.TEN)
    }
}
