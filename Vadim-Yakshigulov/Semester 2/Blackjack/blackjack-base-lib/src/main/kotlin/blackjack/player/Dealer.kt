package blackjack.player

import blackjack.hand.Hand

class Dealer(override var balance: Int = 999999) : IDealer {
    override val hands = mutableListOf(Hand())
    override var activeHand: Hand = hands.first()
}