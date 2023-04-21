package casino.lib.blackjack

data class TableInfo(
    val numberOfDecks: Int,
    val allowedBets: UIntRange,
)
