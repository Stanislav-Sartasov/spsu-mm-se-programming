package bot
import blackjack.action.inGame.InGameAction
import blackjack.hand.Hand
import blackjack.card.ICard
import blackjack.ioHandler.IOHandler

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
    val strategy: Map<Pair<Int, Int>, InGameAction>
    fun Map<Pair<Int, Int>, InGameAction>.addStrategy(
        playerScoreRange: IntRange,
        dealerScoreRange: IntRange,
        action: InGameAction
    ): Map<Pair<Int, Int>, InGameAction>
    
    fun run(n: Int, resetBalanceAfterGame: Boolean = true)
}