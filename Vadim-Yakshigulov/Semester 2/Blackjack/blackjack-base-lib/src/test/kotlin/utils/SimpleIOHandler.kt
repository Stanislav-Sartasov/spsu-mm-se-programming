package utils

import blackjack.action.inGame.InGameAction
import blackjack.hand.Hand
import blackjack.card.ICard
import blackjack.ioHandler.IOHandler
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

    override fun chooseFromPossibleActions(msg: String, vararg actionsNames: Array<InGameAction>): String {
        return actionsNames.first()
    }

    override fun chooseFromBetsInRange(msg: String, start: Int, stop: Int): Int {
        return start
    }

    override fun chooseFromHands(msg: String, defaultReturn: Hand, vararg hands: Hand): Hand {
        return if (hands.isNotEmpty()) hands.first() else defaultReturn
    }

}