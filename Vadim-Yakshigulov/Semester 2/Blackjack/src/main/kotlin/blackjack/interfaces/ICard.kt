package blackjack.interfaces

import blackjack.enums.CardFace
import blackjack.enums.CardSuit

interface ICard {
    val suit: CardSuit
    val face: CardFace
    var isFaceUp: Boolean
}