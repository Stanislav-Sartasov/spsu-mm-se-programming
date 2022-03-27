package casino.lib.blackjack.bots.hilo

import casino.lib.blackjack.*
import casino.lib.blackjack.PlayerMove.HIT
import casino.lib.blackjack.PlayerMove.STAND
import casino.lib.blackjack.states.*
import casino.lib.card.Card
import casino.lib.card.CardRank.*
import casino.lib.card.CardSuite.*
import org.junit.jupiter.api.Nested
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments.arguments
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals

internal class HiLoStrategyTest {

    @Nested
    inner class GetNextBet {

        @Test
        fun `get next bet with big enough bankroll`() {
            val bet = HiLoStrategy.getNextBet(
                playerBankroll = 10000u,
                gameState = BeforeGameState(
                    table = TABLE_INFO,
                    dealt = listOf()
                )
            )

            assertEquals(expected = 20u, actual = bet)
        }

        @Test
        fun `get next bet with big enough bankroll with positive true count`() {
            val dealt = List(size = 32) { TWO_OF_CLUBS } +
                    listOf(ACE_OF_HEARTS, SIX_OF_DIAMONDS, TEN_OF_CLUBS, FOUR_OF_DIAMONDS, NINE_OF_HEARTS)
            val bet = HiLoStrategy.getNextBet(
                playerBankroll = 10000u,
                gameState = BeforeGameState(
                    table = TABLE_INFO,
                    dealt = dealt
                )
            )

            assertEquals(expected = 100u, actual = bet)
        }

        @Test
        fun `get next bet with small bankroll`() {
            val bet = HiLoStrategy.getNextBet(
                playerBankroll = 1u,
                gameState = BeforeGameState(
                    table = TABLE_INFO,
                    dealt = listOf()
                )
            )

            assertEquals(expected = 1u, actual = bet)
        }
    }

    @ParameterizedTest
    @MethodSource("gameStatesWithNextMoves")
    fun getNextMove(gameState: GameState.InGame, nextMove: PlayerMove) {
        assertEquals(expected = HiLoStrategy.getNextMove(gameState), actual = nextMove)
    }


    private companion object {

        val TABLE_INFO = TableInfo(numberOfDecks = 8, allowedBets = 1u..5000u)

        @JvmStatic
        fun gameStatesWithNextMoves() = listOf(
            stateBy(dealer = FOUR_OF_DIAMONDS, player = listOf(ACE_OF_HEARTS, EIGHT_OF_CLUBS), DEALT_TC_3) to HIT,
            stateBy(dealer = FOUR_OF_DIAMONDS, player = listOf(ACE_OF_HEARTS, EIGHT_OF_CLUBS)) to STAND,
            stateBy(dealer = SIX_OF_DIAMONDS, player = listOf(ACE_OF_HEARTS, EIGHT_OF_CLUBS), DEALT_TC_1) to HIT,
            stateBy(dealer = SIX_OF_DIAMONDS, player = listOf(ACE_OF_HEARTS, EIGHT_OF_CLUBS)) to STAND,
            stateBy(dealer = TWO_OF_CLUBS, player = listOf(ACE_OF_HEARTS, SIX_OF_DIAMONDS), DEALT_TC_1) to STAND,
            stateBy(dealer = TWO_OF_CLUBS, player = listOf(ACE_OF_HEARTS, SIX_OF_DIAMONDS)) to HIT,
            stateBy(dealer = TWO_OF_CLUBS, player = listOf(ACE_OF_HEARTS, TWO_OF_CLUBS)) to HIT,

            stateBy(dealer = NINE_OF_HEARTS, player = listOf(TEN_OF_CLUBS, SIX_OF_DIAMONDS), DEALT_TC_4) to STAND,
            stateBy(dealer = NINE_OF_HEARTS, player = listOf(TEN_OF_CLUBS, SIX_OF_DIAMONDS)) to HIT,
            stateBy(dealer = TEN_OF_CLUBS, player = listOf(NINE_OF_HEARTS, SIX_OF_DIAMONDS), DEALT_TC_4) to STAND,
            stateBy(dealer = TEN_OF_CLUBS, player = listOf(NINE_OF_HEARTS, SIX_OF_DIAMONDS)) to HIT,
            stateBy(dealer = TWO_OF_CLUBS, player = listOf(FOUR_OF_DIAMONDS, NINE_OF_HEARTS), DEALT_TC_NEG_1) to HIT,
            stateBy(dealer = TWO_OF_CLUBS, player = listOf(FOUR_OF_DIAMONDS, NINE_OF_HEARTS)) to STAND,
            stateBy(dealer = TWO_OF_CLUBS, player = listOf(TEN_OF_CLUBS, TWO_OF_CLUBS), DEALT_TC_3) to STAND,
            stateBy(dealer = TWO_OF_CLUBS, player = listOf(TEN_OF_CLUBS, TWO_OF_CLUBS)) to HIT,
            stateBy(dealer = ACE_OF_HEARTS, player = listOf(NINE_OF_HEARTS, TWO_OF_CLUBS), DEALT_TC_1) to STAND,
            stateBy(dealer = ACE_OF_HEARTS, player = listOf(NINE_OF_HEARTS, TWO_OF_CLUBS)) to HIT,
            stateBy(dealer = ACE_OF_SPADES, player = listOf(FOUR_OF_DIAMONDS, SIX_OF_DIAMONDS), DEALT_TC_4) to STAND,
            stateBy(dealer = ACE_OF_SPADES, player = listOf(FOUR_OF_DIAMONDS, SIX_OF_DIAMONDS)) to HIT,
            stateBy(dealer = TWO_OF_CLUBS, player = listOf(TWO_OF_CLUBS, SEVEN_OF_HEARTS), DEALT_TC_1) to STAND,
            stateBy(dealer = TWO_OF_CLUBS, player = listOf(TWO_OF_CLUBS, SEVEN_OF_HEARTS)) to HIT,
            stateBy(
                dealer = SIX_OF_DIAMONDS,
                player = listOf(FOUR_OF_DIAMONDS, FOUR_OF_DIAMONDS),
                DEALT_TC_2
            ) to STAND,
            stateBy(dealer = SIX_OF_DIAMONDS, player = listOf(FOUR_OF_DIAMONDS, FOUR_OF_DIAMONDS)) to HIT,
            stateBy(dealer = TWO_OF_CLUBS, player = listOf(TWO_OF_CLUBS, TWO_OF_CLUBS)) to HIT,
        ).map { (a, b) -> arguments(a, b) }


        val TWO_OF_CLUBS = Card.get(suite = CLUBS, rank = TWO)
        val ACE_OF_SPADES = Card.get(suite = SPADES, rank = ACE)
        val ACE_OF_HEARTS = Card.get(suite = HEARTS, rank = ACE)
        val FOUR_OF_DIAMONDS = Card.get(suite = DIAMONDS, rank = FOUR)
        val SIX_OF_DIAMONDS = Card.get(suite = DIAMONDS, rank = SIX)
        val SEVEN_OF_HEARTS = Card.get(suite = HEARTS, rank = SEVEN)
        val NINE_OF_HEARTS = Card.get(suite = HEARTS, rank = NINE)
        val TEN_OF_CLUBS = Card.get(suite = CLUBS, rank = TEN)
        val EIGHT_OF_CLUBS = Card.get(suite = CLUBS, rank = EIGHT)

        val DEALT_TC_NEG_1 = List(size = 10) { ACE_OF_SPADES }
        val DEALT_TC_1 = List(size = 10) { TWO_OF_CLUBS }
        val DEALT_TC_2 = List(size = 20) { TWO_OF_CLUBS }
        val DEALT_TC_3 = List(size = 25) { TWO_OF_CLUBS }
        val DEALT_TC_4 = List(size = 30) { TWO_OF_CLUBS }

        fun stateBy(dealer: Card, player: List<Card>, dealt: List<Card> = emptyList()) = GameState.InGame(
            table = TABLE_INFO,
            dealer = DealerState(openCard = dealer),
            player = PlayerState(hand = Hand(cards = player)),
            dealt = dealt
        )
    }
}
