package casino.lib.blackjack.states

import casino.lib.blackjack.Hand
import casino.lib.card.Card

internal data class InitDealerHand(
    internal val openCard: Card,
    internal val holeCard: Card,
) {

    internal val hand = Hand(listOf(openCard, holeCard))
}
