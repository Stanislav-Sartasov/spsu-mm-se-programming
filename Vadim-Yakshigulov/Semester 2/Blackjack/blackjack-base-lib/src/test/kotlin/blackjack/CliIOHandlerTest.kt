package blackjack.classes

import blackjack.card.Card
import blackjack.card.CardFace
import blackjack.card.CardSuit
import blackjack.hand.Hand
import blackjack.ioHandler.CliIOHandler
import org.junit.jupiter.api.Test
import java.io.ByteArrayOutputStream
import kotlin.test.assertContains
import kotlin.test.assertEquals

internal class CliIOHandlerTest {
    @Test
    fun `readLine() returns inputted text`() {
        val inStream = "test".byteInputStream()
        val result = CliIOHandler(inStream, System.out).readLine()
        assertEquals("test", result)
    }

    @Test
    fun `chooseFromHands() returns defaultHand or one of hands`() {
        val handler = CliIOHandler("1".byteInputStream(), ByteArrayOutputStream())
        val default = Hand()
        val otherHands = mutableListOf(Hand().apply { addCard(Card(CardSuit.SPADES, CardFace.ACE)) }, Hand(), Hand())
        val result = handler.chooseFromHands("", default, *otherHands.toTypedArray())
        assertContains(otherHands + default, result)
    }

    @Test
    fun `chooseFromHands() without hands param returns defaultHand`() {
        val handler = CliIOHandler("1".byteInputStream(), ByteArrayOutputStream())
        val default = Hand()
        val result = handler.chooseFromHands("", default)
        assertEquals(default, result)
    }

    @Test
    fun `chooseFromPossibleActions() returns one of actionsNames`() {
        val handler = CliIOHandler("Stand".byteInputStream(), ByteArrayOutputStream())
        val actions = mutableListOf("Stand", "Hit")
        val result = handler.chooseFromPossibleActions("", *actions.toTypedArray())
        assertContains(actions, result)
    }

    @Test
    fun `chooseFromBetsInRange returns int from range from start to stop`() {
        val handler = CliIOHandler("15".byteInputStream(), ByteArrayOutputStream())
        val result = handler.chooseFromBetsInRange("", 10, 1000)
        assertContains(10..1000, result)
    }
}