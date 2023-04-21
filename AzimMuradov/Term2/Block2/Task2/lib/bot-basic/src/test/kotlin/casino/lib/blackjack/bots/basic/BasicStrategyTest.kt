package casino.lib.blackjack.bots.basic

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

internal class BasicStrategyTest {

    @Nested
    inner class GetNextBet {

        @Test
        fun `get next bet with big enough bankroll`() {
            val bet = BasicStrategy.getNextBet(
                playerBankroll = 10000u,
                gameState = BeforeGameState(
                    table = TABLE_INFO,
                    dealt = listOf()
                )
            )

            assertEquals(expected = 20u, actual = bet)
        }

        @Test
        fun `get next bet with small bankroll`() {
            val bet = BasicStrategy.getNextBet(
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
        assertEquals(expected = BasicStrategy.getNextMove(gameState), actual = nextMove)
    }


    private companion object {

        val TABLE_INFO = TableInfo(numberOfDecks = 8, allowedBets = 1u..5000u)

        @JvmStatic
        fun gameStatesWithNextMoves() = listOf(
            stateBy(dealer = SIX_OF_DIAMONDS, player = listOf(ACE_OF_HEARTS, NINE_OF_HEARTS)) to STAND,
            stateBy(dealer = SIX_OF_DIAMONDS, player = listOf(ACE_OF_SPADES, SEVEN_OF_HEARTS)) to STAND,
            stateBy(dealer = NINE_OF_HEARTS, player = listOf(ACE_OF_SPADES, SEVEN_OF_HEARTS)) to HIT,
            stateBy(dealer = SIX_OF_DIAMONDS, player = listOf(ACE_OF_SPADES, SIX_OF_DIAMONDS)) to HIT,

            stateBy(dealer = SIX_OF_DIAMONDS, player = listOf(TEN_OF_CLUBS, NINE_OF_HEARTS)) to STAND,
            stateBy(dealer = TWO_OF_CLUBS, player = listOf(SEVEN_OF_HEARTS, SEVEN_OF_HEARTS)) to STAND,
            stateBy(dealer = QUEEN_OF_SPADES, player = listOf(SEVEN_OF_HEARTS, SEVEN_OF_HEARTS)) to HIT,
            stateBy(dealer = TWO_OF_CLUBS, player = listOf(TWO_OF_CLUBS, TEN_OF_CLUBS)) to HIT,
            stateBy(dealer = FOUR_OF_DIAMONDS, player = listOf(TEN_OF_CLUBS, TWO_OF_CLUBS)) to STAND,
            stateBy(dealer = QUEEN_OF_SPADES, player = listOf(TWO_OF_CLUBS, TEN_OF_CLUBS)) to HIT,
            stateBy(dealer = SIX_OF_DIAMONDS, player = listOf(FOUR_OF_DIAMONDS, SIX_OF_DIAMONDS)) to HIT,
        ).map { (a, b) -> arguments(a, b) }


        val TWO_OF_CLUBS = Card.get(suite = CLUBS, rank = TWO)
        val ACE_OF_SPADES = Card.get(suite = SPADES, rank = ACE)
        val ACE_OF_HEARTS = Card.get(suite = HEARTS, rank = ACE)
        val QUEEN_OF_SPADES = Card.get(suite = SPADES, rank = QUEEN)
        val FOUR_OF_DIAMONDS = Card.get(suite = DIAMONDS, rank = FOUR)
        val SIX_OF_DIAMONDS = Card.get(suite = DIAMONDS, rank = SIX)
        val SEVEN_OF_HEARTS = Card.get(suite = HEARTS, rank = SEVEN)
        val NINE_OF_HEARTS = Card.get(suite = HEARTS, rank = NINE)
        val TEN_OF_CLUBS = Card.get(suite = CLUBS, rank = TEN)

        fun stateBy(dealer: Card, player: List<Card>) = GameState.InGame(
            table = TABLE_INFO,
            dealer = DealerState(openCard = dealer),
            player = PlayerState(hand = Hand(cards = player)),
            dealt = listOf()
        )
    }
}
