import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import lib.blackjack.base.*
import lib.blackjack.base.state.PlayerMove.*
import lib.blackjack.base.state.CardRank.*
import lib.blackjack.base.state.GameInfo
import lib.blackjack.base.state.PlayerMove
import lib.blackjack.bots.SimpleBot

class TestSimpleBot {
    @Test
    fun `Test SimpleBot bet`() {
        val gameInfo = GameInfo(1u..10u, 1u)
        assertEquals(1u, SimpleBot.getBet(10u, gameInfo))
    }

    @Test
    fun `Test SimpleBot move`() {
        val gameInfo = GameInfo(1u..10u, 1u)
        val hands: Array<List<Card>> = arrayOf(
            listOf(Card(TWO), Card(TEN)),
            listOf(Card(ACE), Card(SIX))
        )
        val moves: Array<PlayerMove> = arrayOf(HIT, STAND)
        for (i in hands.indices) {
            val hand = Hand()
            for (j in hands[i]) {
                hand.addCard(j)
            }
            assertEquals(SimpleBot.getMove(hand, gameInfo), moves[i])
        }
    }
}