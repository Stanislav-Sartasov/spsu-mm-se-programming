package deck;

import card.BlackjackCard;
import card.Card;
import card.CardStatus;
import card.Suits;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.function.Executable;

import java.util.ArrayList;

import static org.junit.jupiter.api.Assertions.*;

class ShoeTest {
    Shoe shoe;
    @BeforeEach
    void setUp() {
        shoe = Shoe.createShoe();
    }

    @Test
    void testShuffle() {
        assertDoesNotThrow(shoe::shuffle);
    }

    @Test
    void testDealCardExpectFullDeck() {
        ArrayList<Card> cardsArray = new ArrayList<>();
        ArrayList<CardStatus> statusArray = new ArrayList<>();

        for (int i = 0; i < 52; i++) {
            var currentCard = shoe.dealCard();
            cardsArray.add(currentCard.getCard());
            statusArray.add(currentCard.getCardStatus());
        }

        for (int rank = 2; rank <= 14; rank++) {
            for (var suit : Suits.values()) {
                assertTrue(cardsArray.contains(new Card(rank, suit)));
            }
        }

        for (int i = 0; i < 52; i++) {
            assertEquals(CardStatus.FACE_DOWN, statusArray.get(i));
        }
    }

    @Test
    void testSingleDeckReset() {
        for (int i = 0; i < 100; i++) {
            shoe.dealCard();
        }
    }

    @Test
    void testMultipleDeckReset() {
        shoe = Shoe.createShoeWithNDecks(4);
        for (int i = 0; i < 1000; i++) {
            shoe.dealCard();
        }
    }

    @Test
    void testTooManyDecksExpectException() {
        assertThrows(IllegalArgumentException.class, () -> Shoe.createShoeWithNDecks(10));
    }
}