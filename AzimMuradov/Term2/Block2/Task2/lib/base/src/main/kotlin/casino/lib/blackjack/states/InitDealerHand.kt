package casino.lib.blackjack.states

import casino.lib.blackjack.Hand
import casino.lib.card.Card

internal data class InitDealerHand(val openCard: Card, val holeCard: Card) {

    val hand = Hand(listOf(openCard, holeCard))
}
