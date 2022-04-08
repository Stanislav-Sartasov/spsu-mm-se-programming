package bot

import blackjack.action.inGame.InGameAction
import blackjack.player.Player

class StrategyBasedBlackjackBot(player: Player) : Bot(player) {
    override val strategy = mapOf<Pair<Int, Int>, InGameAction>()
        .addStrategy(0..8, 2..11, InGameAction.HIT)
        .addStrategy(9..9, 2..2, InGameAction.HIT)
        .addStrategy(9..9,3..6, InGameAction.DOUBLE)
        .addStrategy(10..11, 2..9, InGameAction.DOUBLE)
        .addStrategy(11..11, 10..10, InGameAction.DOUBLE)
        .addStrategy(11..11, 11..11, InGameAction.HIT)
        .addStrategy(13..21, 2..6, InGameAction.STAND)
        .addStrategy(12..16, 7..11, InGameAction.HIT)
        .addStrategy(17..21, 2..11, InGameAction.STAND)
}
