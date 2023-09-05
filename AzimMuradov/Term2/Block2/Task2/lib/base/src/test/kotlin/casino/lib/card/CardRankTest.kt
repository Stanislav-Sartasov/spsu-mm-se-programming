package casino.lib.card

import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments.arguments
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals

internal class CardRankTest {

    @ParameterizedTest
    @MethodSource("ranksWithIsNumeral")
    fun `check if rank is numeral`(rank: CardRank, isNumeral: Boolean) {
        assertEquals(expected = isNumeral, actual = rank.isNumeral())
    }

    @ParameterizedTest
    @MethodSource("ranksWithIsFace")
    fun `check if rank is face`(rank: CardRank, isFace: Boolean) {
        assertEquals(expected = isFace, actual = rank.isFace())
    }


    private companion object {

        @JvmStatic
        fun ranksWithIsNumeral() = (ranks zip listOf(
            false,
            true, true, true, true, true, true, true, true, true,
            false, false, false
        )).map { (a, b) -> arguments(a, b) }

        @JvmStatic
        fun ranksWithIsFace() = (ranks zip listOf(
            false,
            false, false, false, false, false, false, false, false, false,
            true, true, true
        )).map { (a, b) -> arguments(a, b) }


        val ranks = CardRank.values()
    }
}
