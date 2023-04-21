import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import lib.blackjack.base.*
import lib.blackjack.base.state.PlayerMove.*
import lib.blackjack.base.state.CardRank.*
import lib.blackjack.base.state.CardSuit.*
import lib.blackjack.base.state.GameInfo
import lib.blackjack.bots.BaseBot

class TestBaseBot {
    @Test
    fun `Test BaseBot bet`() {
        val gameInfo = GameInfo(1u..10u, 1u)
        assertEquals(1u, BaseBot.getBet(10u, gameInfo))
    }

    @Test
    fun `Test BaseBot move HIT`() {
        val gameInfo = GameInfo(1u..10u, 1u)
        val hands: Array<List<Card>> = arrayOf(
            listOf(Card(TWO), Card(TWO, PIKES)),
            listOf(Card(TWO), Card(NINE)),
            listOf(Card(TEN), Card(TWO)),
            listOf(Card(TEN), Card(THREE)),
            listOf(Card(TEN), Card(SIX)),
            listOf(Card(ACE), Card(TWO)),
            listOf(Card(ACE), Card(SEVEN))
        )
        val croupierCards: Array<Card> = arrayOf(
            Card(ACE), Card(ACE), Card(SEVEN),
            Card(SEVEN), Card(EIGHT), Card(ACE), Card(NINE)
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
            listOf(Card(TEN), Card(TWO)),
            listOf(Card(TEN), Card(THREE)),
            listOf(Card(TEN), Card(SIX)),
            listOf(Card(ACE), Card(TEN)),
            listOf(Card(ACE), Card(SEVEN))
        )
        val croupierCards: Array<Card> = arrayOf(Card(FOUR), Card(TWO),
            Card(SIX), Card(ACE), Card(EIGHT)
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
}