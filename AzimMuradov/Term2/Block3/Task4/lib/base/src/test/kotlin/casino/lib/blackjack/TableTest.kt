package casino.lib.blackjack

import casino.lib.blackjack.states.BeforeGameState
import casino.lib.blackjack.states.GameState
import casino.lib.card.Card
import casino.lib.shoe.StackShoe
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.ValueSource
import kotlin.test.assertEquals
import kotlin.test.assertFailsWith

internal class TableTest {

    // Table info

    @Test
    fun `get standard table info`() {
        assertEquals(
            expected = STANDARD_TABLE_INFO,
            actual = Table.standard().info
        )
    }

    @Test
    fun `get custom table info`() {
        assertEquals(
            expected = CUSTOM_TABLE_INFO,
            actual = Table.custom(info = CUSTOM_TABLE_INFO).info
        )
    }


    // Play session

    @Test
    fun `play 1 session with TABLE_1`() {
        val (newBankroll, results) = TABLE_1.playSession(
            strategy = simpleStrategy(bet = 50u),
            bankroll = 5000u
        )

        assertEquals(expected = 5025u, actual = newBankroll)
        assertEquals(
            expected = listOf(
                GameState.AfterGame.Push(amount = 50u),
                GameState.AfterGame.Lost,
                GameState.AfterGame.Won(amount = 125u),
                GameState.AfterGame.Lost,
                GameState.AfterGame.Won(amount = 100u),
            ),
            actual = results
        )
    }

    @Test
    fun `play 1 session with TABLE_2`() {
        val (newBankroll, results) = TABLE_2.playSession(
            strategy = simpleStrategy(bet = 50u),
            bankroll = 5000u
        )

        assertEquals(expected = 4975u, actual = newBankroll)
        assertEquals(
            expected = listOf(
                GameState.AfterGame.Won(amount = 100u),
                GameState.AfterGame.Lost,
                GameState.AfterGame.Won(amount = 125u),
                GameState.AfterGame.Lost,
                GameState.AfterGame.Lost,
            ),
            actual = results
        )
    }

    @Test
    fun `fail to play 1 session with bets that exceeds player's bankroll`() {
        assertFailsWith<IllegalArgumentException>(message = "Player's bet must not exceed his bankroll") {
            TABLE_1.playSession(
                strategy = simpleStrategy(bet = 10000u),
                bankroll = 5000u
            )
        }
    }

    @Test
    fun `stop to play 1 session early with zero bankroll`() {
        val (newBankroll, results) = TABLE_1.playSession(
            strategy = simpleStrategy(bet = 10u),
            bankroll = 10u
        )

        assertEquals(expected = 0u, actual = newBankroll)
        assertEquals(
            expected = listOf(
                GameState.AfterGame.Push(amount = 10u),
                GameState.AfterGame.Lost,
            ),
            actual = results
        )
    }

    @ParameterizedTest
    @ValueSource(ints = [0, 5001, 10000])
    fun `fail to play 1 session with not allowed bets`(bet: Int) {
        assertFailsWith<IllegalArgumentException>(message = "Player's bet must be in the 'allowed bets' range") {
            TABLE_1.playSession(
                strategy = simpleStrategy(bet = bet.toUInt()),
                bankroll = 50000u
            )
        }
    }


    private companion object {

        val STANDARD_TABLE_INFO = TableInfo(numberOfDecks = 8, allowedBets = 1u..5000u)

        val CUSTOM_TABLE_INFO = TableInfo(numberOfDecks = 6, allowedBets = 5u..2000u)

        val ONE_DECK_TABLE_INFO = TableInfo(numberOfDecks = 1, allowedBets = 1u..5000u)

        val TABLE_1 = Table.custom(info = ONE_DECK_TABLE_INFO) {
            StackShoe(
                cards = run {
                    val (s, h, d, c) = Card.deck.chunked(size = 13)
                    val (h9, h10, hJ, hQ, hK) = h.takeLast(n = 5)
                    listOf(s, h.dropLast(n = 5), listOf(h10, h9, hJ, hQ, hK), d, c).flatten()
                }
            )
        }

        val TABLE_2 = Table.custom(info = ONE_DECK_TABLE_INFO) {
            StackShoe(
                cards = run {
                    val (s, h, d, c) = Card.deck.chunked(size = 13)
                    val (sA, s2) = s.take(n = 2)
                    val (s3, s4, s5, s6, s7) = s.slice(indices = 2..6)
                    listOf(listOf(sA, s2, s3, s4, s7, s6, s5), s.drop(n = 7), h, d, c).flatten()
                }
            )
        }

        fun simpleStrategy(bet: UInt) = object : PlayerStrategy {

            override fun getNextBet(playerBankroll: UInt, gameState: BeforeGameState) = bet

            override fun getNextMove(gameState: GameState.InGame): PlayerMove =
                if (gameState.player.hand.total() < 16) PlayerMove.HIT else PlayerMove.STAND
        }
    }
}
