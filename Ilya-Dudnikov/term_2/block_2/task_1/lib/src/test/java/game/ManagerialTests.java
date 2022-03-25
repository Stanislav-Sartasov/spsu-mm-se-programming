package game;

import bet.BetStatus;
import card.BlackjackCard;
import card.Suits;
import game.controllers.StandController;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import player.BlackjackDealer;
import player.BlackjackPlayer;
import service.account_manager.AccountManager;

import java.util.ArrayList;

import static org.junit.jupiter.api.Assertions.*;

public class ManagerialTests {
	private BlackjackGame blackjackGame;

	@BeforeEach
	void setUp() {
		blackjackGame = new BlackjackGame(
				"pool1337",
				new AccountManager(),
				new BlackjackDealer("dealer1337"),
				1000,
				1
		);

		StandController standController = new StandController(new BlackjackPlayer("Player1337"));
		blackjackGame.addPlayer(standController, 100);
	}

	@Test
	void addTooManyPlayersExpectException() {
		assertThrows(IllegalArgumentException.class, () -> {
			for (int i = 0; i < 9; i++) {
				blackjackGame.addPlayer(new StandController(new BlackjackPlayer("player" + i)), 100);
			}
		});
	}

	@Test
	void calculateBetStatusDraw() {
		var dealerHand = blackjackGame.dealer.getHand();
		dealerHand.addCard(new BlackjackCard(10, Suits.CLUB));
		dealerHand.addCard(new BlackjackCard(10, Suits.DIAMOND));

		var playerHand = blackjackGame.playerList.get(0).getHand();
		playerHand.addCard(new BlackjackCard(11, Suits.CLUB));
		playerHand.addCard(new BlackjackCard(13, Suits.CLUB));

		assertEquals(BetStatus.DRAW, blackjackGame.calculateBetStatus((BlackjackPlayer) blackjackGame.playerList.get(0)));
	}

	@Test
	void calculateBetStatusDrawBothHaveBlackjack() {
		var dealerHand = blackjackGame.dealer.getHand();
		dealerHand.addCard(new BlackjackCard(10, Suits.CLUB));
		dealerHand.addCard(new BlackjackCard(14, Suits.DIAMOND));

		var playerHand = blackjackGame.playerList.get(0).getHand();
		playerHand.addCard(new BlackjackCard(11, Suits.CLUB));
		playerHand.addCard(new BlackjackCard(14, Suits.CLUB));

		assertEquals(BetStatus.DRAW, blackjackGame.calculateBetStatus((BlackjackPlayer) blackjackGame.playerList.get(0)));
	}

	@Test
	void playerWinsWithBlackjack() {
		var dealerHand = blackjackGame.dealer.getHand();
		dealerHand.addCard(new BlackjackCard(10, Suits.CLUB));
		dealerHand.addCard(new BlackjackCard(10, Suits.DIAMOND));

		var playerHand = blackjackGame.playerList.get(0).getHand();
		playerHand.addCard(new BlackjackCard(11, Suits.CLUB));
		playerHand.addCard(new BlackjackCard(14, Suits.CLUB));

		assertEquals(BetStatus.WON_WITH_BLACKJACK, blackjackGame.calculateBetStatus((BlackjackPlayer) blackjackGame.playerList.get(0)));
	}

	@Test
	void dealerWinsWithBlackjack() {
		var dealerHand = blackjackGame.dealer.getHand();
		dealerHand.addCard(new BlackjackCard(10, Suits.CLUB));
		dealerHand.addCard(new BlackjackCard(14, Suits.DIAMOND));

		var playerHand = blackjackGame.playerList.get(0).getHand();
		playerHand.addCard(new BlackjackCard(11, Suits.CLUB));
		playerHand.addCard(new BlackjackCard(13, Suits.CLUB));

		assertEquals(BetStatus.LOST, blackjackGame.calculateBetStatus((BlackjackPlayer) blackjackGame.playerList.get(0)));
	}

	@Test
	void playerBusts() {
		var dealerHand = blackjackGame.dealer.getHand();
		dealerHand.addCard(new BlackjackCard(10, Suits.CLUB));
		dealerHand.addCard(new BlackjackCard(10, Suits.DIAMOND));

		var playerHand = blackjackGame.playerList.get(0).getHand();
		playerHand.addCard(new BlackjackCard(11, Suits.CLUB));
		playerHand.addCard(new BlackjackCard(13, Suits.CLUB));
		playerHand.addCard(new BlackjackCard(5, Suits.CLUB));

		assertEquals(BetStatus.LOST, blackjackGame.calculateBetStatus((BlackjackPlayer) blackjackGame.playerList.get(0)));
	}

	@Test
	void dealerWins() {
		var dealerHand = blackjackGame.dealer.getHand();
		dealerHand.addCard(new BlackjackCard(10, Suits.CLUB));
		dealerHand.addCard(new BlackjackCard(10, Suits.DIAMOND));

		var playerHand = blackjackGame.playerList.get(0).getHand();
		playerHand.addCard(new BlackjackCard(5, Suits.CLUB));
		playerHand.addCard(new BlackjackCard(13, Suits.CLUB));

		assertEquals(BetStatus.LOST, blackjackGame.calculateBetStatus((BlackjackPlayer) blackjackGame.playerList.get(0)));
	}

	@Test
	void playerWins() {
		var dealerHand = blackjackGame.dealer.getHand();
		dealerHand.addCard(new BlackjackCard(10, Suits.CLUB));
		dealerHand.addCard(new BlackjackCard(2, Suits.DIAMOND));

		var playerHand = blackjackGame.playerList.get(0).getHand();
		playerHand.addCard(new BlackjackCard(11, Suits.CLUB));
		playerHand.addCard(new BlackjackCard(13, Suits.CLUB));

		assertEquals(BetStatus.WON, blackjackGame.calculateBetStatus((BlackjackPlayer) blackjackGame.playerList.get(0)));
	}

	@Test
	void dealerBusts() {
		var dealerHand = blackjackGame.dealer.getHand();
		dealerHand.addCard(new BlackjackCard(10, Suits.CLUB));
		dealerHand.addCard(new BlackjackCard(13, Suits.DIAMOND));
		dealerHand.addCard(new BlackjackCard(9, Suits.DIAMOND));

		var playerHand = blackjackGame.playerList.get(0).getHand();
		playerHand.addCard(new BlackjackCard(11, Suits.CLUB));
		playerHand.addCard(new BlackjackCard(13, Suits.CLUB));

		assertEquals(BetStatus.WON, blackjackGame.calculateBetStatus((BlackjackPlayer) blackjackGame.playerList.get(0)));
	}
}
