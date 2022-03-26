package casino.lib.blackjack

import casino.lib.blackjack.states.BeforeGameState
import casino.lib.blackjack.states.GameState

interface PlayerStrategy {

    fun getNextBet(playerBankroll: UInt, gameState: BeforeGameState): UInt

    fun getNextMove(gameState: GameState.InGame): PlayerMove
}
