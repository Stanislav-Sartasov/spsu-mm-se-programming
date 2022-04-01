import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import lib.blackjack.base.*
import lib.blackjack.base.PlayerMove.HIT
import lib.blackjack.base.PlayerMove.STAND
import lib.blackjack.bots.BaseBot
import lib.blackjack.bots.RandomBot
import lib.blackjack.bots.SimpleBot

class TestBlackjackBots {
    @Test
    fun `Test SimpleBot bet`() {
        val gameInfo = GameInfo(1u..10u, 1u)
        assertEquals(1u, SimpleBot.getBet(10u, gameInfo))
    }

    @Test
    fun `Test SimpleBot move`() {
        val gameInfo = GameInfo(1u..10u, 1u)
        val hands: Array<List<Card>> = arrayOf(
            listOf(Card(2, "2"), Card(10, "J")),
            listOf(Card(1, "A"), Card(6, "6"))
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

    @Test
    fun `Test BaseBot bet`() {
        val gameInfo = GameInfo(1u..10u, 1u)
        assertEquals(1u, BaseBot.getBet(10u, gameInfo))
    }

    @Test
    fun `Test BaseBot move HIT`() {
        val gameInfo = GameInfo(1u..10u, 1u)
        val hands: Array<List<Card>> = arrayOf(
            listOf(Card(2, "2"), Card(2, "2")),
            listOf(Card(2, "2"), Card(9, "9")),
            listOf(Card(10, "10"), Card(2, "2")),
            listOf(Card(10, "10"), Card(3, "3")),
            listOf(Card(10, "10"), Card(6, "6")),
            listOf(Card(11, "A"), Card(2, "2")),
            listOf(Card(11, "A"), Card(7, "7"))
        )
        val croupierCards: Array<Card> = arrayOf(
            Card(1, "A"), Card(1, "A"), Card(7, "7"),
            Card(7, "7"), Card(8, "8"), Card(1, "A"), Card(9, "9")
        )
        for (i in hands.indices) {
            val hand = Hand()
            for (j in hands[i]) {
                hand.addCard(j)
            }
            gameInfo.croupierCard = croupierCards[i]
            val move = BaseBot.getMove(hand, gameInfo)
            assertEquals(move, HIT)
        }
    }

    @Test
    fun `Test BaseBot move STAND`() {
        val gameInfo = GameInfo(1u..10u, 1u)
        val hands: Array<List<Card>> = arrayOf(
            listOf(Card(10, "10"), Card(2, "2")),
            listOf(Card(10, "10"), Card(3, "3")),
            listOf(Card(10, "10"), Card(6, "6")),
            listOf(Card(11, "A"), Card(10, "10")),
            listOf(Card(11, "A"), Card(7, "7"))
        )
        val croupierCards: Array<Card> = arrayOf(Card(4, "4"), Card(2, "2"),
            Card(6, "6"), Card(1, "A"), Card(8, "8")
        )
        for (i in hands.indices) {
            val hand = Hand()
            for (j in hands[i]) {
                hand.addCard(j)
            }
            gameInfo.croupierCard = croupierCards[i]
            val move = BaseBot.getMove(hand, gameInfo)
            assertEquals(move, STAND)
        }
    }

    @Test
    fun `Test RandomBot bet`() {
        val gameInfo = GameInfo(1u..10u, 1u)
        assertEquals(1u, RandomBot.getBet(10u, gameInfo))
    }

    @Test
    fun `Test RandomBot move`() {
        val gameInfo = GameInfo(1u..10u, 1u)
        var moves: List<PlayerMove> = emptyList()
        for (i in 1..14) {
            moves = moves + RandomBot.getMove(Hand(), gameInfo)
        }
        assert(HIT in moves)
        assert(STAND in moves)
    }
}