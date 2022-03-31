package blackjack.interfaces

import blackjack.classes.Hand
import java.io.InputStream
import java.io.OutputStream


interface IOHandler {
    val inStream: InputStream
    val outStream: OutputStream
    fun println(msg: String)
    fun readLine(): String
    fun showCards(msg: String, cards: List<ICard>)
    fun showHands(msg: String, hands: List<Hand>)
    fun chooseFromPossibleActions(msg: String, vararg actionsNames: String): String
    fun chooseFromBetsInRange(msg: String, start: Int, stop: Int): Int
    fun chooseFromHands(msg: String, defaultReturn: Hand, vararg hands: Hand): Hand
}