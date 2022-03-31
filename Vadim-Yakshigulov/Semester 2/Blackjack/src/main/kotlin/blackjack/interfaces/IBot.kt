package blackjack.interfaces
import blackjack.classes.Hand

interface IBot : IOHandler {
    override fun println(msg: String) {}
    override fun readLine(): String = ""
    override fun showCards(msg: String, cards: List<ICard>) {}
    override fun showHands(msg: String, hands: List<Hand>) {}
    override fun chooseFromHands(msg: String, defaultReturn: Hand, vararg hands: Hand): Hand {
        return if (hands.isNotEmpty()) hands.first() else defaultReturn
    }

    /**
     * first value of pair is player cards value sum
     * second value of pair is dealer cards value sum
     *
     * second value of map is your action name (e.g. Hit or Stand)
     **/
    val strategy: Map<Pair<Int, Int>, String>
    fun Map<Pair<Int, Int>, String>.addStrategy(
        playerScoreRange: IntRange,
        dealerScoreRange: IntRange,
        action: String
    ): Map<Pair<Int, Int>, String>

    fun run(n: Int, resetBalanceAfterGame: Boolean = true)
}