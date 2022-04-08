package blackjack.player

import blackjack.hand.Hand

interface IPlayer {
    val hands: MutableList<Hand>
    var balance: Int
    var activeHand: Hand
}
