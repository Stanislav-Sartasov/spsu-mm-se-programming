package blackjack.interfaces

import blackjack.classes.Hand

interface IPlayer {
    val hands: MutableList<Hand>
    var balance: Int
    var activeHand: Hand
}
