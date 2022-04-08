package blackjack.ioHandler

import blackjack.action.inGame.InGameAction
import blackjack.card.ICard
import blackjack.hand.Hand
import java.io.InputStream
import java.io.OutputStream


interface IOHandler {
    val inStream: InputStream
    val outStream: OutputStream
    fun println(msg: String)
    fun readLine(): String
    fun showCards(msg: String, cards: List<ICard>)
    fun showHands(msg: String, hands: List<Hand>)
    fun chooseFromPossibleActions(msg: String, vararg actionsNames: InGameAction): InGameAction
    fun chooseFromBetsInRange(msg: String, start: Int, stop: Int): Int
    fun chooseFromHands(msg: String, defaultReturn: Hand, vararg hands: Hand): Hand
}