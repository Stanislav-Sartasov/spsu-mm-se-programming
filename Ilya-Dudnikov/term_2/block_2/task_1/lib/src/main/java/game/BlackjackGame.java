package game;

import bet.Bet;
import bet.BetStatus;
import card.AbstractCard;
import card.BlackjackCard;
import card.CardStatus;
import deck.Shoe;
import service.account_manager.AccountManager;
import service.bet_settler.BetSettler;
import player.BlackjackDealer;
import player.BlackjackPlayer;
import player.PlayerController;

import java.util.ArrayList;
import java.util.Collections;

public class BlackjackGame extends Game implements IBlackjackGame {
	protected BlackjackDealer dealer;
	protected Shoe shoe;

	public BlackjackGame(
			String poolId,
			AccountManager accountManager,
			BlackjackDealer dealer,
			int initialPoolBalance,
			int numberOfDecks
	) {
		super(poolId, accountManager, initialPoolBalance);
		this.dealer = dealer;
		this.shoe = Shoe.createShoeWithNDecks(numberOfDecks);
	}

	protected void initialBet(int position) {
		Bet bet = new Bet(playerList.get(position).getId(), controllerList.get(position).getInitialBet());
		accountManager.transfer(playerList.get(position).getId(), betPool.getPoolId(), bet.getValue());

		betPool.addBet(bet);
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

	protected void hit(int position) {
		BlackjackPlayer player = (BlackjackPlayer) playerList.get(position);

		dealer.dealTo(player, shoe, CardStatus.FACE_UP);

		if (player.getHand().getHandScore() > 21) {
			betPool.changeBetStatus(position, BetStatus.LOST);
		}
	}

	protected void split(int position) {
		BlackjackPlayer player = (BlackjackPlayer) playerList.get(position);
		if (player.getHand().getVisibleCards().size() != 2) {
			throw new IllegalArgumentException("You can't split unless you have two cards of the same rank");
		}

		BlackjackCard firstCard = player.getHand().getCardAt(0);
		BlackjackCard secondCard = player.getHand().getCardAt(1);

		if (firstCard.getCard().rank() != secondCard.getCard().rank()) {
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

		dealer.dealTo(splittedFirstPlayer, shoe, CardStatus.FACE_UP);
		dealer.dealTo(splittedSecondPlayer, shoe, CardStatus.FACE_UP);

		controllerList.add(position, controllerList.get(position));

		betPool.insertBet(new Bet(player.getId(), betPool.getBetValueAt(position)), position);
	}

	protected void surrender(int position) {
		betPool.changeBetStatus(position, BetStatus.SURRENDERED);
	}

	protected void doubleDown(int position) {
		betPool.doubleBet(position);
		hit(position);
	}

	protected BetStatus calculateBetStatus(BlackjackPlayer player) {
		if (dealer.getHand().isBlackjack() && player.getHand().isBlackjack()) {
			return BetStatus.DRAW;
		}

		if (player.getHand().isBlackjack()) {
			return BetStatus.WON_WITH_BLACKJACK;
		}

		if (dealer.getHand().getHandScore() > 21)
			return BetStatus.WON;

		if (player.getHand().getHandScore() > 21)
			return BetStatus.LOST;

		if (    dealer.getHand().isBlackjack() ||
				dealer.getHand().getHandScore() > player.getHand().getHandScore()) {
			return BetStatus.LOST;
		}

		if (dealer.getHand().getHandScore() < player.getHand().getHandScore()) {
			return BetStatus.WON;
		}

		return BetStatus.DRAW;
	}

	protected ArrayList<AbstractCard> getCardsOnTable() {
		ArrayList<AbstractCard> result = new ArrayList<>();
		result.add(dealer.getHand().getCardAt(0));
		for (var player : playerList) {
			result.addAll(player.getHand().getVisibleCards());
		}

		return result;
	}

	protected void finishRound() {
		dealer.draw(shoe);
		BetSettler betSettler = new BetSettler();

		for (int i = 0; i < playerList.size(); i++) {
			BlackjackPlayer player = (BlackjackPlayer) playerList.get(i);

			if (betPool.getBetStatusAt(i) == BetStatus.PENDING) {
				betPool.changeBetStatus(i, calculateBetStatus(player));
			}

			Bet bet = new Bet(playerList.get(i).getId(), betPool.getBetValueAt(i));
			bet.changeBetStatus(betPool.getBetStatusAt(i));
			int winningAmount = betSettler.settleBet(bet);
			accountManager.transfer(betPool.getPoolId(), player.getId(), winningAmount);
		}
		betPool.clearPool();

		for (var player : playerList)
			player.getHand().clear();

		playerList.clear();
		controllerList.clear();

		dealer.getHand().clear();
	}

	public void getInitialBets() {
		ArrayList<Integer> toRemove = new ArrayList<>();
		for (int i = 0; i < controllerList.size(); i++) {
			try {
				initialBet(i);
			} catch (IllegalArgumentException e) {
				toRemove.add(i);
			}
		}

		for (int i = toRemove.size() - 1; i >= 0; i--) {
			playerList.remove(toRemove.get(i).intValue());
			controllerList.remove(toRemove.get(i).intValue());
		}
	}

	public void playRound() {
		getInitialBets();

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

		ArrayList<Boolean> mayBeSkipped = new ArrayList<>(Collections.nCopies(playerList.size(), false));
		int playersLeft = playerList.size();

		while (playersLeft > 0) {
			for (int i = 0; i < playerList.size(); i++) {
				if (mayBeSkipped.get(i))
					continue;

				Action actionToPlay = controllerList.get(i).getAction(getCardsOnTable());

				switch (actionToPlay) {
					case STAND ->  {
						mayBeSkipped.set(i, true);
						playersLeft--;
					}
					case HIT -> {
						hit(i);
						if (betPool.getBetStatusAt(i) == BetStatus.LOST) {
							mayBeSkipped.set(i, true);
							playersLeft--;
						}
					}
					case SPLIT -> {
						try {
							split(i);
							mayBeSkipped.add(i, false);
						} catch (IllegalArgumentException e) {
							mayBeSkipped.set(i, true);
							playersLeft--;
						}
					}
					case SURRENDER -> {
						surrender(i);
						mayBeSkipped.set(i, true);
						playersLeft--;
					}
					case DOUBLE_DOWN -> {
						try {
							doubleDown(i);
						} catch (IllegalArgumentException e) {
							hit(i);
						}
						mayBeSkipped.set(i, true);
						playersLeft--;
					}
				}
			}
		}

		finishRound();
	}
}
