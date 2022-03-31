package casino.lib.blackjack.states

import casino.lib.blackjack.TableInfo
import casino.lib.card.Card

data class BeforeGameState(
    val table: TableInfo,
    val dealt: List<Card>,
)
