package casino.lib.blackjack.states

import casino.lib.blackjack.TableInfo
import casino.lib.card.Card

sealed interface GameState {

    data class InGame(
        val table: TableInfo,
        val dealer: DealerState,
        val player: PlayerState,
        val dealt: List<Card>,
    ) : GameState

    sealed interface AfterGame : GameState {

        val amount: UInt


        data class Won(override val amount: UInt) : AfterGame

        data class Push(override val amount: UInt) : AfterGame

        object Lost : AfterGame {

            override val amount: UInt = 0u
        }
    }
}
