package bots

import blackjack.BlackjackGame
import blackjack.classes.Bot
import blackjack.classes.Dealer
import blackjack.classes.Player
import blackjack.interfaces.IGame


class RandomizedBlackjackBot(player: Player) : Bot(player) {
    override val strategy: Map<Pair<Int, Int>, String> = mapOf()

    override fun chooseFromPossibleActions(msg: String, vararg actionsNames: String): String {
        return actionsNames.random()
    }

    override fun chooseFromBetsInRange(msg: String, start: Int, stop: Int): Int {
        return (start..stop).random()
    }

}