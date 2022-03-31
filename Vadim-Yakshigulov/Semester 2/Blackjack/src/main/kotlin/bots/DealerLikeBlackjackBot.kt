package bots

import blackjack.classes.Bot
import blackjack.classes.Player
import blackjack.classes.ScoreCounter

class DealerLikeBlackjackBot(player: Player) : Bot(player) {
    override val strategy: Map<Pair<Int, Int>, String> = mapOf()

    override fun chooseFromPossibleActions(msg: String, vararg actionsNames: String): String {
        if (ScoreCounter.calculateForHand(player.activeHand) < 17)
            return "Hit"
        return "Stand"
    }
}