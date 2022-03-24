package player;

import card.CardStatus;
import deck.Shoe;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class BlackjackDealerTest {
	private BlackjackDealer dealer;
	private BlackjackPlayer player;
	private Shoe shoe;

	@BeforeEach
	void setUp() {
		dealer = new BlackjackDealer("dealer1337");
		player = new BlackjackPlayer("player1337");
		shoe = Shoe.createShoe();
	}

	@Test
	void dealTo() {
		dealer.dealTo(player, shoe, CardStatus.FACE_UP);
		dealer.dealTo(player, shoe, CardStatus.FACE_DOWN);

		var playerHand = player.getHand().getVisibleCards();
		assertEquals(1, playerHand.size());
	}

	@Test
	void draw() {
		dealer.draw(shoe);
		assertTrue(dealer.getHand().getHandScore() >= 17);
	}

	@Test
	void getId() {
		assertEquals("dealer1337", dealer.getId());
	}
}