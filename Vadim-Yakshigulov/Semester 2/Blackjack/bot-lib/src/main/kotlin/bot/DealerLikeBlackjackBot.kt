package bot

import blackjack.action.inGame.InGameAction
import blackjack.player.Player
import blackjack.util.ScoreCounter

class DealerLikeBlackjackBot(player: Player) : Bot(player) {
    override val strategy: Map<Pair<Int, Int>, InGameAction> = mapOf()

    override fun chooseFromPossibleActions(msg: String, vararg actionsNames: InGameAction): InGameAction {
        if (ScoreCounter.calculateForHand(player.activeHand) < 17)
            return InGameAction.HIT
        return InGameAction.STAND
    }
}