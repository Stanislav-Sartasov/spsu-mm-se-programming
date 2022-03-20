package casino.lib.blackjack

interface PlayerStrategy {

    fun getNextBet(playerBankroll: UInt, gameState: GameState.BeforeGame): UInt

    fun getNextMove(gameState: GameState.InGame): PlayerMove
}
