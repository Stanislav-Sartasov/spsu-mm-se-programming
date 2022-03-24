package service.bet_settler;

import bet.Bet;
import bet.BetStatus;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class BetSettlerTest {
	private BetSettler betSettler;
	private Bet bet;

	@BeforeEach
	void setUp() {
		betSettler = new BetSettler();
		bet = new Bet("bettor1337", 100);
	}

	@Test
	void settleBetPending() {
		assertThrows(IllegalArgumentException.class, () -> betSettler.settleBet(bet));
	}

	@Test
	void settleBetWon() {
		bet.changeBetStatus(BetStatus.WON);
		assertEquals(200, betSettler.settleBet(bet));
	}

	@Test
	void settleBetWonWithBlackjack() {
		bet.changeBetStatus(BetStatus.WON_WITH_BLACKJACK);
		assertEquals(250, betSettler.settleBet(bet));
	}

	@Test
	void settleBetLost() {
		bet.changeBetStatus(BetStatus.LOST);
		assertEquals(0, betSettler.settleBet(bet));
	}

	@Test
	void settleBetSurrendered() {
		bet.changeBetStatus(BetStatus.SURRENDERED);
		assertEquals(50, betSettler.settleBet(bet));
	}

	@Test
	void settleBetDraw() {
		bet.changeBetStatus(BetStatus.DRAW);
		assertEquals(100, betSettler.settleBet(bet));
	}
}