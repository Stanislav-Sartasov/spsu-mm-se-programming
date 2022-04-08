package bot

import blackjack.action.inGame.InGameAction
import blackjack.player.Player


class RandomizedBlackjackBot(player: Player) : Bot(player) {
    override val strategy: Map<Pair<Int, Int>, InGameAction> = mapOf()

    override fun chooseFromPossibleActions(msg: String, vararg actionsNames: InGameAction): InGameAction {
        return actionsNames.random()
    }

    override fun chooseFromBetsInRange(msg: String, start: Int, stop: Int): Int {
        return (start..stop).random()
    }

}