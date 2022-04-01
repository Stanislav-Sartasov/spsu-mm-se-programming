package lib.blackjack.base

class Hand {
    var cards: List<Card> = emptyList()
        private set

    fun hasBlackjack(): Boolean {
        return (this.maxScore() == 21 && cards.size == 2)
    }

    fun addCard(card: Card) {
        cards = cards + card
    }

    fun maxScore(): Int {
        var cntAse = 0
        var value = 0

        for (i in cards) {
            if (i.count == 11)
                cntAse++
            else
                value += i.count
        }
        return if (cntAse > 0 && value + 10 + cntAse <= 21) (value + 10 + cntAse)
        else (value + cntAse)
    }

    fun averageScore(): Int {
        var cntAse = 0
        var value = 0

        for (i in cards) {
            if (i.count == 11)
                cntAse++
            else
                value += i.count
        }

        return if (cntAse > 0 && value + 10 + cntAse in 18..21) (value + 10 + cntAse)
        else (value + cntAse)
    }

    fun minScore(): Int {
        var value = 0

        for (i in cards) {
            if (i.count == 11)
                value++
            else
                value += i.count
        }

        return value
    }
}