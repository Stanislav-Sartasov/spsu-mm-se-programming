package blackjack.classes

import blackjack.enums.CardFace
import blackjack.interfaces.ICard
import blackjack.interfaces.IHand

object ScoreCounter {
    fun calculateForHand(hand: IHand): Int {
        var total = 0
        var aceCount = 0
        for (card in hand.cards) {
            if (!card.isFaceUp)
                continue
            if (card.face != CardFace.ACE)
                total += if (card.face.value > 10) 10 else card.face.value
            else
                aceCount++
        }
        return if (aceCount > 0 && total + aceCount < 12) total + aceCount + 10 else total + aceCount
    }

    fun calculateForCard(card: ICard): Int {
        return if (card.face.value > 10) 10 else card.face.value
    }
}