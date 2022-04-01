import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.ValueSource
import kotlin.test.assertEquals
import kotlin.test.assertNotEquals
import kotlin.test.assertFails
import lib.blackjack.base.*
import lib.blackjack.base.PlayerMove.HIT
import lib.blackjack.base.PlayerMove.STAND

class TestBlackjackBase {
    @ParameterizedTest
    @ValueSource(ints = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11])
    fun `Test Card`(count: Int) {
        val card = Card(count, "7")
        if (count in 2..10)
            assertEquals(card.count, count)
        else
            assertEquals(card.count, 11)
        assertEquals(card.value, "7")
    }

    @Test
    fun `Test Hand`() {
        val hands: Array<List<Card>> = arrayOf(
            listOf(Card(2, "2"), Card(10, "J")),
            listOf(Card(1, "A"), Card(10, "J")),
            listOf(Card(1, "A"), Card(6, "9")),
            listOf(Card(1, "A"), Card(9, "9"), Card(11, "A")),
            listOf(Card(11, "A"), Card(7, "7")),
            listOf(Card(10, "10"), Card(10, "10"))
        )
        val hasBJ: Array<Boolean> = arrayOf(false, true, false, false, false, false)
        val maxScope: Array<Int> = arrayOf(12, 21, 17, 21, 18, 20)
        val averageScope: Array<Int> = arrayOf(12, 21, 7, 21, 18, 20)
        val minScope: Array<Int> = arrayOf(12, 11, 7, 11, 8, 20)

        for (i in hands.indices) {
            val hand = Hand()
            for (j in hands[i]) {
                hand.addCard(j)
            }
            assertEquals(hand.hasBlackjack(), hasBJ[i])
            assertEquals(hand.maxScore(), maxScope[i])
            assertEquals(hand.averageScore(), averageScope[i])
            assertEquals(hand.minScore(), minScope[i])
        }
    }

    @ParameterizedTest
    @ValueSource(ints = [1, 2, 4, 8])
    fun `Test Deck`(cntDeck: Int) {
        val deck = Deck(cntDeck.toUInt())
        assertEquals(deck.remain(), cntDeck * 52)
        var cards: List<Card> = emptyList()
        for (i in 1..cntDeck * 52) {
            cards = cards + deck.getCard()
            assertEquals(deck.remain(), cntDeck * 52 - i)
        }

        for (i in 2..11) {
            val filter = cards.filter { it.count == i }
            if (i != 10)
                assertEquals(filter.size, 4 * cntDeck)
            else
                assertEquals(filter.size, 16 * cntDeck)
        }

        deck.shaffle()
        assertEquals(deck.remain(), cntDeck * 52)

        var cards2: List<Card> = emptyList()
        for (i in 1..cntDeck * 52) {
            cards2 = cards2 + deck.getCard()
        }
        assertNotEquals(cards, cards2)
    }

    @Test
    fun `Test GameInfo`() {
        val range = 1u..4u
        val cntDecks = 1u
        val card = Card(11, "A")
        val gameInfo = GameInfo(range, cntDecks)
        gameInfo.croupierCard = card
        gameInfo.addCard(card)
        assertEquals(gameInfo.rangeBet, range)
        assertEquals(gameInfo.cntDecks, cntDecks)
        assertEquals(gameInfo.croupierCard, card)
        assertEquals(gameInfo.dealt, listOf(card))
    }

    @Test
    fun `Test PlayerMove`() {
        val hit: PlayerMove = HIT
        assertEquals(hit, HIT)
        assertNotEquals(hit, STAND)
        val stand: PlayerMove = STAND
        assertEquals(stand, STAND)
        assertNotEquals(stand, HIT)
    }

    @Test
    fun `Test too many or less player GameTable`() {
        val gameInfo = GameInfo(1u..10u, 8u)
        val table = GameTable(gameInfo)

        assertEquals(0, table.playSession(emptyList(), emptyArray(), 8))
        assertEquals(0, gameInfo.dealt.size)
    }

    @Test
    fun `Test shuffle deck GameTable`() {
        val bankrolls: Array<UInt> = arrayOf(100000u)
        val gameInfo = GameInfo(1u..10u, 1u)
        val table = GameTable(gameInfo)
        BotForTests.bet = 1

        table.playSession(listOf(BotForTests), bankrolls, 1)
        for (i in 1..104) {
            table.playSession(listOf(BotForTests), bankrolls, 1)
            if (gameInfo.dealt.size > 52)
                assertFails { "Deck isn't shuffle" }
        }
        assert(true)
    }

    @ParameterizedTest
    @ValueSource(ints = [0, 1, 10, 100, 1, 1, 1, 1, 1, 1, 1])
    fun `Test GameTable returned value`(bankroll: Int) {
        val bankrolls: Array<UInt> = arrayOf(bankroll.toUInt())
        val gameInfo = GameInfo(1u..10u, 8u)
        val table = GameTable(gameInfo)
        BotForTests.log = emptyList()
        BotForTests.bet = bankroll

        val casinoWon = table.playSession(listOf(BotForTests), bankrolls, 1)
        assertEquals(casinoWon, bankroll - bankrolls[0].toInt())
    }

    @ParameterizedTest
    @ValueSource(ints = [0, 1, 10, 100, 1, 1, 1, 1, 1, 1, 1])
    fun `Test GameTable distribution and move`(bankroll: Int) {
        val bankrolls: Array<UInt> = arrayOf(bankroll.toUInt())
        val gameInfo = GameInfo(1u..10u, 8u)
        val table = GameTable(gameInfo)
        BotForTests.log = emptyList()
        BotForTests.bet = bankroll

        table.playSession(listOf(BotForTests), bankrolls, 1)

        val croupierHand = Hand()
        val playerHand = Hand()

        playerHand.addCard(gameInfo.dealt[0])
        playerHand.addCard(gameInfo.dealt[1])
        croupierHand.addCard(gameInfo.dealt[2])
        var ind = 3
        for (i in BotForTests.log) {
            if (i == HIT && playerHand.minScore() < 21) {
                playerHand.addCard(gameInfo.dealt[ind])
                ind++
            } else
                break
        }
        while (croupierHand.maxScore() < 17) {
            croupierHand.addCard(gameInfo.dealt[ind])
            ind++
        }
        assertEquals(ind, gameInfo.dealt.size)
    }

    @ParameterizedTest
    @ValueSource(ints = [0, 1, 10, 100, 1, 1, 1, 1, 1, 1, 1])
    fun `Test GameTable full game`(bankroll: Int) {
        val bankrolls: Array<UInt> = arrayOf(bankroll.toUInt())
        val gameInfo = GameInfo(1u..10u, 8u)
        val table = GameTable(gameInfo)
        BotForTests.log = emptyList()
        BotForTests.bet = bankroll

        val casinoWon = table.playSession(listOf(BotForTests), bankrolls, 1)

        val croupierHand = Hand()
        val playerHand = Hand()

        playerHand.addCard(gameInfo.dealt[0])
        playerHand.addCard(gameInfo.dealt[1])
        croupierHand.addCard(gameInfo.dealt[2])
        var ind = 3
        for (i in BotForTests.log) {
            if (i == HIT && playerHand.minScore() < 21) {
                playerHand.addCard(gameInfo.dealt[ind])
                ind++
            } else
                break
        }
        while (croupierHand.maxScore() < 17) {
            croupierHand.addCard(gameInfo.dealt[ind])
            ind++
        }

        val pScore = playerHand.maxScore()
        val cScore = croupierHand.maxScore()

        var bank = bankroll
        if (bank.toUInt() !in gameInfo.rangeBet)
            bank = 0

        if (pScore !in 1..21 || cScore in 1..21 && pScore < cScore
            || !playerHand.hasBlackjack() && croupierHand.hasBlackjack()
        ) {
            //lose
            assertEquals(bankrolls[0], (bankroll - bank).toUInt())
            assertEquals(casinoWon, bank)
        } else if (pScore == cScore && playerHand.hasBlackjack() == croupierHand.hasBlackjack()) {
            //draw
            assertEquals(bankrolls[0], bank.toUInt())
        } else if (cScore !in 1..21 || pScore > cScore && !playerHand.hasBlackjack()) {
            //base win
            assertEquals(casinoWon, -bank)
            assertEquals(bankrolls[0], (bankroll + bank).toUInt())
        } else if (playerHand.hasBlackjack() && !croupierHand.hasBlackjack()) {
            //win with blackjack
            assertEquals(-casinoWon, (bank.toDouble() * 2.5).toInt() - bank)
            assertEquals(bankrolls[0], (bank.toDouble() * 2.5).toUInt())
        }
    }
}