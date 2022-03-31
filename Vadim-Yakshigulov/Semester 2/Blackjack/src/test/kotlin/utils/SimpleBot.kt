package utils

import blackjack.classes.Bot
import blackjack.interfaces.IPlayer

class SimpleBot(player: IPlayer) : Bot(player) {
    override val strategy: Map<Pair<Int, Int>, String> = mutableMapOf()
    override fun chooseFromBetsInRange(msg: String, start: Int, stop: Int): Int {
        return start
    }
}