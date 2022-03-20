package casino.lib.blackjack.bots.simple

import casino.lib.blackjack.*
import casino.lib.blackjack.PlayerMove.HIT
import casino.lib.blackjack.PlayerMove.STAND

object SimpleStrategy : PlayerStrategy {

    override fun getNextBet(playerBankroll: UInt, gameState: GameState.BeforeGame): UInt =
        (playerBankroll / 500u).coerceIn(gameState.table.allowedBets)

    override fun getNextMove(gameState: GameState.InGame) = if (gameState.player.hand.total() < 16) HIT else STAND
}
