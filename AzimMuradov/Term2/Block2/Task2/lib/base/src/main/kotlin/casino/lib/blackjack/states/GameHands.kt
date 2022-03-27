package casino.lib.blackjack.states

import casino.lib.blackjack.Hand

internal data class GameHands(
    internal val initDealer: InitDealerHand,
    internal val player: Hand,
)
