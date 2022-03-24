package hand;

import card.BlackjackCard;
import card.Card;
import card.CardStatus;
import card.Suits;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class HandTest {
	private Hand hand;

	private BlackjackCard firstCard = new BlackjackCard(10, Suits.CLUB);
	private BlackjackCard secondCard = new BlackjackCard(14, Suits.SPADE);

	@BeforeEach
	void setUp() {
		hand = new Hand();

		hand.addCard(firstCard);
		hand.addCard(secondCard);
	}

	@Test
	void testCardGetter() {
		assertAll(
				() -> assertEquals(firstCard.getCard(), hand.getCardAt(0).getCard()),
				() -> assertEquals(secondCard.getCard(), hand.getCardAt(1).getCard()),
				() -> assertEquals(firstCard.getCardStatus(), hand.getCardAt(0).getCardStatus()),
				() -> assertEquals(secondCard.getCardStatus(), hand.getCardAt(1).getCardStatus())
		);
	}

	@Test
	void testGetVisibleCards() {
		assertEquals(0, hand.getVisibleCards().size());

		hand.showCards();
		var cards = hand.getVisibleCards();

		assertEquals(firstCard.getCard(), cards.get(0).getCard());
		assertEquals(secondCard.getCard(), cards.get(1).getCard());
	}

	@Test
	void testHandScore() {
		assertEquals(21, hand.getHandScore());
	}

	@Test
	void testAceCountsAsOne() {
		hand.addCard(new BlackjackCard(10, Suits.CLUB));
		assertEquals(21, hand.getHandScore());
	}

	@Test
	void testIsBlackjackExpectRealBlackjack() {
		assertTrue(hand.isBlackjack());
	}

	@Test
	void testIsBlackjackWithScoreOf21() {
		hand.addCard(new BlackjackCard(10, Suits.CLUB));
		assertFalse(hand.isBlackjack());
	}

	@Test
	void testShowCards() {
		hand.showCards();
		var cards = hand.getVisibleCards();

		assertEquals(firstCard, cards.get(0));
		assertEquals(secondCard, cards.get(1));
	}
}