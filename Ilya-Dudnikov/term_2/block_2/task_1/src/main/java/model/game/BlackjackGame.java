package model.game;

import model.bet.Bet;
import model.bet.BetStatus;
import model.card.BlackjackCard;
import model.deck.Shoe;
import model.player.BlackjackDealer;
import model.player.BlackjackPlayer;
import model.service.account_manager.AccountManager;
import model.service.bet_settler.BetSettler;

public class BlackjackGame extends Game {
	private BlackjackDealer dealer;
	private Shoe shoe;

	public BlackjackGame(String poolId, AccountManager accountManager, BlackjackDealer dealer) {
		super(poolId, accountManager);
		this.dealer = dealer;
		this.shoe = Shoe.createShoe();
	}

	public void initialBet(BlackjackPlayer player, int value) {
		if (playerList.size() >= 9) {
			throw new IllegalArgumentException("You can't join, the table is already full");
		}

		accountManager.transfer(player.getId(), betPool.getPoolId(), value);
		playerList.add(player);
		betPool.addBet(new Bet(player.getId(), value));
	}

	public void hit(int position) {
		dealer.dealTo(playerList.get(position), shoe);

		if (playerList.get(position).getHand().getHandScore() > 21) {
			betPool.changeBetStatus(position, BetStatus.LOST);
		}
	}

	public void split(int position) {
		BlackjackPlayer player = (BlackjackPlayer) playerList.get(position);
		BlackjackCard firstCard = player.getHand().getCardAt(0);
		BlackjackCard secondCard = player.getHand().getCardAt(1);

		if (firstCard != secondCard) {
			throw new IllegalArgumentException("You are only allowed to split if the cards are the same rank");
		}

		if (accountManager.getBalanceById(player.getId()) < betPool.getBetValueAt(position)) {
			throw new IllegalArgumentException("Insufficient balance to split");
		}

		BlackjackPlayer splittedFirstPlayer = new BlackjackPlayer(player.getId());
		BlackjackPlayer splittedSecondPlayer = new BlackjackPlayer(player.getId());
		splittedFirstPlayer.getHand().addCard(firstCard);
		splittedSecondPlayer.getHand().addCard(secondCard);

		playerList.add(position, splittedFirstPlayer);
		playerList.set(position + 1, splittedSecondPlayer);

		betPool.insertBet(new Bet(player.getId(), betPool.getBetValueAt(position)), position);
	}

	public void surrender(int position) {
		betPool.changeBetStatus(position, BetStatus.SURRENDERED);
	}

	public void doubleDown(int position) {
		betPool.doubleBet(position);
		hit(position);
	}

	private BetStatus calculateBetStatus(BlackjackPlayer player) {
		if (dealer.getHand().isBlackjack() && player.getHand().isBlackjack()) {
			return BetStatus.DRAW;
		}

		if (player.getHand().isBlackjack()) {
			return BetStatus.WON_WITH_BLACKJACK;
		}

		if (
				dealer.getHand().isBlackjack()
				|| dealer.getHand().getHandScore() > player.getHand().getHandScore()
				|| player.getHand().getHandScore() > 21
		) {
			return BetStatus.LOST;
		}

		if (
				dealer.getHand().getHandScore() < player.getHand().getHandScore()
				|| dealer.getHand().getHandScore() > 21
		) {
			return BetStatus.WON;
		}

		return BetStatus.DRAW;
	}

	public void finishRound() {
		dealer.draw(shoe);
		BetSettler betSettler = new BetSettler();

		for (int i = 0; i < playerList.size(); i++) {
			if (betPool.getBetStatusAt(i) == BetStatus.PENDING) {
				betPool.changeBetStatus(i, calculateBetStatus((BlackjackPlayer) playerList.get(i)));
			}

			int winningAmount = betSettler.settleBet(playerList.get(i).getId(), betPool.getBetAt(i));
			accountManager.transfer(betPool.getPoolId(), playerList.get(i).getId(), winningAmount);
		}
		betPool.clearPool();
		playerList.clear();
	}
}
