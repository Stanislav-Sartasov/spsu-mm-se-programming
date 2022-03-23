package model.game;

import model.bet.Bet;
import model.bet.BetStatus;
import model.card.AbstractCard;
import model.card.BlackjackCard;
import model.card.CardStatus;
import model.deck.Shoe;
import model.player.*;
import model.service.account_manager.AccountManager;
import model.service.bet_settler.BetSettler;

import java.util.ArrayList;

public class BlackjackGame extends Game {
	private BlackjackDealer dealer;
	private Shoe shoe;

	public BlackjackGame(
			String poolId,
			AccountManager accountManager,
			BlackjackDealer dealer
	) {
		super(poolId, accountManager);
		this.dealer = dealer;
		this.shoe = Shoe.createShoe();
	}

	protected void initialBet(int position) {
		betPool.addBet(new Bet(playerList.get(position).getId(), controllerList.get(position).getInitialBet()));
	}

	@Override
	protected void addPlayer(PlayerController controller, int initialBalance) {
		if (playerList.size() >= 9) {
			throw new IllegalArgumentException("There are no available seats at the table");
		}

		playerList.add(controller.getPlayer());
		controllerList.add(controller);
		accountManager.createNewAccount(controller.getPlayer().getId(), initialBalance);
	}

	private void hit(int position) {
		BlackjackPlayer player = (BlackjackPlayer) playerList.get(position);

		dealer.dealTo(player, shoe, CardStatus.FACE_UP);

		if (player.getHand().getHandScore() > 21) {
			betPool.changeBetStatus(position, BetStatus.LOST);
		}
	}

	private void split(int position) {
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
		playerList.set(position + 1, splittedFirstPlayer);

		controllerList.add(position, controllerList.get(position));

		betPool.insertBet(new Bet(player.getId(), betPool.getBetValueAt(position)), position);
	}

	private void surrender(int position) {
		betPool.changeBetStatus(position, BetStatus.SURRENDERED);
	}

	private void doubleDown(int position) {
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

	private ArrayList<AbstractCard> getCardsOnTable() {
		ArrayList<AbstractCard> result = new ArrayList<>();
		result.add(dealer.getHand().getCardAt(0));
		for (var player : playerList) {
			result.addAll(player.getHand().getVisibleCards());
		}

		return result;
	}

	public void finishRound() {
		dealer.draw(shoe);
		BetSettler betSettler = new BetSettler();

		for (int i = 0; i < playerList.size(); i++) {
			BlackjackPlayer player = (BlackjackPlayer) playerList.get(i);

			if (betPool.getBetStatusAt(i) == BetStatus.PENDING) {
				betPool.changeBetStatus(i, calculateBetStatus(player));
			}

			int winningAmount = betSettler.settleBet(player.getId(), betPool.getBetAt(i));
			accountManager.transfer(betPool.getPoolId(), player.getId(), winningAmount);
		}
		betPool.clearPool();
		playerList.clear();
		controllerList.clear();
	}

	public void playRound() {
		for (int i = 0; i < 2; i++) {
			for (var player : playerList) {
				dealer.dealTo(player, shoe, CardStatus.FACE_UP);
			}
			if (i == 0) {
				dealer.dealTo(dealer, shoe, CardStatus.FACE_UP);
			} else {
				dealer.dealTo(dealer, shoe, CardStatus.FACE_DOWN);
			}
		}

		boolean[] mayBeSkipped = new boolean[playerList.size()];
		int playersLeft = playerList.size();

		while (playersLeft > 0) {
			for (int i = 0; i < playerList.size(); i++) {
				if (mayBeSkipped[i])
					continue;

				Action actionToPlay = controllerList.get(i).getAction(getCardsOnTable());

				switch (actionToPlay) {
					case STAND ->  {
						mayBeSkipped[i] = true;
						playersLeft--;
					}
					case HIT -> hit(i);
					case SPLIT -> split(i);
					case SURRENDER -> {
						surrender(i);
						mayBeSkipped[i] = true;
						playersLeft--;
					}
					case DOUBLE_DOWN -> doubleDown(i);
				}
			}
		}

		finishRound();
	}
}
