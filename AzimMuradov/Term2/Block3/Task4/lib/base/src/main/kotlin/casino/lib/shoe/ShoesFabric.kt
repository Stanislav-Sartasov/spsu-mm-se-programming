package casino.lib.shoe

import casino.lib.card.Card

object ShoesFabric {

    fun shuffled(numberOfDecks: Int): Shoe = decksBased {
        List(numberOfDecks) { Card.deck }.flatten().shuffled()
    }


    private fun decksBased(cardsFabric: () -> List<Card>): Shoe {
        val cards = cardsFabric()
        val allowedSize = Card.deck.size..Card.deck.size * 8
        require(cards.size in allowedSize) { "Shoe's size must be in the range of 1 to 8 decks" }
        return StackShoe(cards)
    }
}
