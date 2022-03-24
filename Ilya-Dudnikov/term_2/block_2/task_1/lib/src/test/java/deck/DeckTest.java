package deck;

import card.Card;
import card.Suits;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;

import static org.junit.jupiter.api.Assertions.*;

class DeckTest {
    private Deck deck;

    @BeforeEach
    void setUp() {
        deck = new Deck();
    }

    @Test
    void testIfDeckIsFull() {
        ArrayList<Card> cardsFromDeck = new ArrayList<>();
        for (int i = 0; i < 52; i++) {
            cardsFromDeck.add(deck.getCardAt(i));
        }

        for (int rank = 2; rank <= 14; rank++) {
            for (var suit : Suits.values()) {
                assertTrue(cardsFromDeck.contains(new Card(rank, suit)));
            }
        }
    }

    @Test
    void testShuffle() {
        assertDoesNotThrow(deck::shuffle);
    }
}