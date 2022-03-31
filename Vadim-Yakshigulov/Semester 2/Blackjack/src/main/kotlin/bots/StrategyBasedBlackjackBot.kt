package bots

import blackjack.classes.Bot
import blackjack.classes.Player

class StrategyBasedBlackjackBot(player: Player) : Bot(player) {
    override val strategy = mapOf<Pair<Int, Int>, String>()
        .addStrategy(0..8, 2..11, "Hit")
        .addStrategy(9..9, 2..2, "Hit")
        .addStrategy(9..9,3..6, "Double")
        .addStrategy(10..11, 2..9, "Double")
        .addStrategy(11..11, 10..10, "Double")
        .addStrategy(11..11, 11..11, "Hit")
        .addStrategy(13..21, 2..6, "Stand")
        .addStrategy(12..16, 7..11, "Hit")
        .addStrategy(17..21, 2..11, "Stand")
}
