package casino.lib.blackjack.bots.simple

import casino.lib.blackjack.Hand
import casino.lib.blackjack.PlayerMove.HIT
import casino.lib.blackjack.PlayerMove.STAND
import casino.lib.blackjack.TableInfo
import casino.lib.blackjack.states.*
import casino.lib.card.Card
import casino.lib.card.CardRank.ACE
import casino.lib.card.CardRank.THREE
import casino.lib.card.CardSuite.SPADES
import org.junit.jupiter.api.Nested
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.ValueSource
import kotlin.test.assertEquals

internal class SimpleStrategyTest {

    @Nested
    inner class GetNextBet {

        @Test
        fun `get next bet with big enough bankroll`() {
            val bet = SimpleStrategy.getNextBet(
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
            val bet = SimpleStrategy.getNextBet(
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
    @ValueSource(ints = [0, 1, 2, 3, 5, 6])
    fun getNextMove(numberOfThrees: Int) {
        val nextMove = SimpleStrategy.getNextMove(
            GameState.InGame(
                table = TABLE_INFO,
                dealer = DealerState(Card.get(SPADES, ACE)),
                player = PlayerState(
                    Hand(
                        List(size = numberOfThrees) {
                            Card.get(SPADES, THREE)
                        }
                    )
                ),
                dealt = listOf()
            )
        )

        assertEquals(
            expected = if (numberOfThrees * 3 < 16) HIT else STAND,
            actual = nextMove
        )
    }


    private companion object {

        val TABLE_INFO = TableInfo(numberOfDecks = 8, allowedBets = 1u..5000u)
    }
}
