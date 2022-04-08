package blackjack.card

import blackjack.card.CardFace
import blackjack.card.CardSuit

interface ICard {
    val suit: CardSuit
    val face: CardFace
    var isFaceUp: Boolean
}