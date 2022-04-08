package blackjack.card

class Card(override val suit: CardSuit, override val face: CardFace) : ICard {
    override var isFaceUp = true
    override fun toString(): String {
        return "Card($suit, $face)"
    }
}