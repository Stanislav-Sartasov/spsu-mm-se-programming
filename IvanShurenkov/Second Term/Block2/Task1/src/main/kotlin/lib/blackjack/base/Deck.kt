package lib.blackjack.base

class Deck(cntDecks: UInt) {
    private var deck: Array<Card> = emptyArray()
    private var pos: Int = -1

    init {
        for (i in 1..cntDecks.toInt()) {
            for (suit in "♥♠♦♣") {
                for (rank in 2..10)
                    deck += Card(rank, "$rank$suit")
                for (rank in "KQJ")
                    deck += Card(10, "$rank$suit")
                deck += Card(11, "A$suit")
            }
        }
    }

    fun getCard(): Card {
        pos++
        return deck[pos]
    }

    fun remain(): Int {
        return deck.size - (pos + 1)
    }

    fun shaffle() {
        pos = -1
        deck.shuffle()
    }
}
