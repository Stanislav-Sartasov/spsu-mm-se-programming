package card;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Nested;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class BlackjackCardTest {
    private BlackjackCard card;
    private final int rank = 10;
    private final Suits suit = Suits.CLUB;

    @BeforeEach
    void setUp() {
        card = new BlackjackCard(new Card(rank, suit));
    }

    @Test
    void testGetters() {
        assertAll(
                () -> assertEquals(new Card(rank, suit), card.getCard()),
                () -> assertEquals(CardStatus.FACE_DOWN, card.getCardStatus())
        );
    }

    @Test
    void testSetter() {
        card.setCardStatus(CardStatus.FACE_UP);

        assertEquals(CardStatus.FACE_UP, card.getCardStatus());
    }
}