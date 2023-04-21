package casino.lib.blackjack.bots.basic

import casino.lib.blackjack.*
import casino.lib.blackjack.PlayerMove.HIT
import casino.lib.blackjack.PlayerMove.STAND
import casino.lib.blackjack.states.BeforeGameState
import casino.lib.blackjack.states.GameState
import casino.lib.card.*

object BasicStrategy : PlayerStrategy {

    override fun getNextBet(playerBankroll: UInt, gameState: BeforeGameState): UInt =
        (playerBankroll / 500u).coerceIn(gameState.table.allowedBets)

    override fun getNextMove(gameState: GameState.InGame): PlayerMove {
        val (_, dealer, player) = gameState
        val total = player.hand.total()
        val i = dealerIndex[dealer.openCard.rank]
        return if (player.hand.isSoft()) {
            when (total) {
                in 19..20 -> STAND
                18 -> if (i in 0..6) STAND else HIT
                else -> HIT
            }
        } else {
            when (total) {
                in 17..20 -> STAND
                in 13..16 -> if (i in 0..4) STAND else HIT
                12 -> if (i in 2..4) STAND else HIT
                else -> HIT
            }
        }
    }


    private val dealerIndex = buildMap {
        put(CardRank.ACE, 9)
        putAll(CardRank.values().filter(CardRank::isNumeral).associateWith { it.ordinal - 1 })
        putAll(CardRank.values().filter(CardRank::isFace).associateWith { 8 })
    }
}
