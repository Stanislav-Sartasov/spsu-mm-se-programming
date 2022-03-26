package casino.lib.blackjack.bots.simple

import casino.lib.blackjack.PlayerMove.HIT
import casino.lib.blackjack.PlayerMove.STAND
import casino.lib.blackjack.PlayerStrategy
import casino.lib.blackjack.states.BeforeGameState
import casino.lib.blackjack.states.GameState
import casino.lib.blackjack.total

object SimpleStrategy : PlayerStrategy {

    override fun getNextBet(playerBankroll: UInt, gameState: BeforeGameState): UInt =
        (playerBankroll / 500u).coerceIn(gameState.table.allowedBets)

    override fun getNextMove(gameState: GameState.InGame) = if (gameState.player.hand.total() < 16) HIT else STAND
}
