package blackjack.classes
import blackjack.interfaces.IPlayer

class Player(override var balance: Int) : IPlayer {
    override val hands = mutableListOf(Hand())
    override var activeHand: Hand = hands.first()
}