package casino.lib.blackjack.states

import casino.lib.blackjack.Hand

internal data class GameHands(val initDealer: InitDealerHand, val player: Hand)
