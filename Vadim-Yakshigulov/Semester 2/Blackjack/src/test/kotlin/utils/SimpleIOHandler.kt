package utils

import blackjack.classes.Hand
import blackjack.interfaces.ICard
import blackjack.interfaces.IOHandler
import java.io.InputStream
import java.io.OutputStream

class SimpleIOHandler : IOHandler {
    override fun println(msg: String) {}
    override val inStream: InputStream = System.`in`
    override val outStream: OutputStream = System.out
    override fun readLine(): String {
        return inStream.bufferedReader().readLine() ?: ""
    }

    override fun showCards(msg: String, cards: List<ICard>) {}

    override fun showHands(msg: String, hands: List<Hand>) {}

    override fun chooseFromPossibleActions(msg: String, vararg actionsNames: String): String {
        return actionsNames.first()
    }

    override fun chooseFromBetsInRange(msg: String, start: Int, stop: Int): Int {
        return start
    }

    override fun chooseFromHands(msg: String, defaultReturn: Hand, vararg hands: Hand): Hand {
        return if (hands.isNotEmpty()) hands.first() else defaultReturn
    }

}