package game;

import bet.Bet;
import bet.BetStatus;
import card.BlackjackCard;
import card.Suits;
import game.controllers.*;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Nested;
import org.junit.jupiter.api.Test;
import player.BlackjackDealer;
import player.BlackjackPlayer;
import service.account_manager.AccountManager;

import static org.junit.jupiter.api.Assertions.*;

class GameTest {
	BlackjackGame blackjackGame;

	@BeforeEach
	void setUp() {
		blackjackGame = new BlackjackGame(
				"pool1337",
				new AccountManager(),
				new BlackjackDealer("dealer1337"),
				1000
		);

		StandController standController = new StandController(new BlackjackPlayer("Player1337"));
		blackjackGame.addPlayer(standController, 100);
	}

	@Nested
	class GameRelatedTests {
		@Test
		void initialBet() {
			blackjackGame.initialBet(0);

			assertEquals(15, blackjackGame.betPool.getBetValueAt(0));
		}

		@Test
		void initialBetOutOfBounds() {
			assertThrows(IndexOutOfBoundsException.class, () -> blackjackGame.initialBet(2));
		}

		@Test
		void hit() {
			blackjackGame.hit(0);

			assertEquals(1, blackjackGame.playerList.get(0).getHand().getVisibleCards().size());
		}

		@Test
		void hitBust() {
			blackjackGame.betPool.addBet(new Bet("Player1337", 25));
			while (blackjackGame.playerList.get(0).getHand().getHandScore() <= 21)
				blackjackGame.hit(0);
			assertEquals(BetStatus.LOST, blackjackGame.betPool.getBetStatusAt(0));
		}

		@Test
		void hitOutOfBounds() {
			assertThrows(IndexOutOfBoundsException.class, () -> blackjackGame.hit(2));
		}

		@Test
		void split() {
			blackjackGame.playerList.get(0).getHand().addCard(new BlackjackCard(10, Suits.CLUB));
			blackjackGame.playerList.get(0).getHand().addCard(new BlackjackCard(10, Suits.DIAMOND));

			Bet initialBet = new Bet("Player1337", 10);
			blackjackGame.betPool.addBet(initialBet);

			blackjackGame.split(0);
			assertEquals(2, blackjackGame.playerList.size());
			assertEquals(2, blackjackGame.controllerList.size());

			assertEquals(initialBet.getValue(), blackjackGame.betPool.getBetValueAt(0));
			assertEquals(initialBet.getBettorId(), blackjackGame.betPool.getBettorIdAt(0));
			assertEquals(initialBet.getValue(), blackjackGame.betPool.getBetValueAt(1));
			assertEquals(initialBet.getBettorId(), blackjackGame.betPool.getBettorIdAt(1));
		}

		@Test
		void splitWithCardsOfDifferentRanksExpectIllegalArgumentException() {
			blackjackGame.playerList.get(0).getHand().addCard(new BlackjackCard(10, Suits.CLUB));
			blackjackGame.playerList.get(0).getHand().addCard(new BlackjackCard(5, Suits.DIAMOND));

			assertThrows(IllegalArgumentException.class, () -> blackjackGame.split(0));
		}

		@Test
		void testSplitWithInsufficientBalance() {
			blackjackGame.playerList.get(0).getHand().addCard(new BlackjackCard(10, Suits.CLUB));
			blackjackGame.playerList.get(0).getHand().addCard(new BlackjackCard(10, Suits.DIAMOND));

			Bet initialBet = new Bet("Player1337", 100);
			blackjackGame.betPool.addBet(initialBet);
			blackjackGame.accountManager.transfer("Player1337", "pool1337", 100);

			assertThrows(IllegalArgumentException.class, () -> blackjackGame.split(0));
		}

		@Test
		void testSurrender() {
			blackjackGame.betPool.addBet(new Bet("Player1337", 50));
			blackjackGame.surrender(0);

			assertEquals(BetStatus.SURRENDERED, blackjackGame.betPool.getBetStatusAt(0));
		}

		@Test
		void testSurrenderOutOfBounds() {
			assertThrows(IndexOutOfBoundsException.class, () -> blackjackGame.surrender(0));
		}

		@Test
		void testDoubleDown() {
			Bet initialBet = new Bet("Player1337", 100);
			blackjackGame.betPool.addBet(initialBet);

			blackjackGame.doubleDown(0);

			assertEquals(initialBet.getValue() * 2, blackjackGame.betPool.getBetValueAt(0));
		}

		@Test
		void testDoubleDownOutOfBounds() {
			assertThrows(IndexOutOfBoundsException.class, () -> blackjackGame.doubleDown(0));
		}

		@Test
		void testFinishRound() {
			Bet initialBet = new Bet("Player1337", 100);
			blackjackGame.betPool.addBet(initialBet);
			blackjackGame.hit(0);

			blackjackGame.finishRound();

			assertTrue(blackjackGame.dealer.getHand().getHandScore() >= 17);
			assertTrue(blackjackGame.playerList.isEmpty());
			assertTrue(blackjackGame.controllerList.isEmpty());
			assertThrows(IndexOutOfBoundsException.class, () -> blackjackGame.betPool.getBetValueAt(0));
		}

		@Test
		void testPlayRound() {
			HitController hitController = new HitController(new BlackjackPlayer("Player1337Hit"));
			DoubleDownController doubleDownController = new DoubleDownController(new BlackjackPlayer("Player1337DoubleDown"));
			SplitController splitController = new SplitController(new BlackjackPlayer("Player1337Split"));
			SurrenderController surrenderController = new SurrenderController(new BlackjackPlayer("Player1337Surrender"));

			blackjackGame.addPlayer(hitController, 10000);
			blackjackGame.addPlayer(doubleDownController, 10000);
			blackjackGame.addPlayer(splitController, 10000);
			blackjackGame.addPlayer(surrenderController, 10000);

			blackjackGame.playRound();
		}
	}
}