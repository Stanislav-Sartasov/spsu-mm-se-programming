package casino.lib.blackjack

import casino.lib.card.Card

sealed interface GameState {

    data class BeforeGame(
        val table: TableInfo,
        val discard: List<Card>,
    ) : GameState

    data class InGame(
        val table: TableInfo,
        val dealer: DealerState,
        val player: PlayerState,
        val discard: List<Card>,
    ) : GameState

    sealed interface Ended : GameState {

        val amount: UInt


        data class Won(override val amount: UInt) : Ended

        data class Push(override val amount: UInt) : Ended

        object Lost : Ended {

            override val amount: UInt = 0u
        }
    }
}


data class DealerState(val openCard: Card)

data class PlayerState(val hand: Hand)
