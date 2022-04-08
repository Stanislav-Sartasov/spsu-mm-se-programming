package blackjack.player
import blackjack.hand.Hand

class Player(override var balance: Int) : IPlayer {
    override val hands = mutableListOf(Hand())
    override var activeHand: Hand = hands.first()
}