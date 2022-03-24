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

public class BlackjackGame extends Game implements IBlackjackGame {
	protected BlackjackDealer dealer;
	protected Shoe shoe;

	public BlackjackGame(
			String poolId,
			AccountManager accountManager,
			BlackjackDealer dealer,
			int initialPoolBalance
	) {
		super(poolId, accountManager, initialPoolBalance);
		this.dealer = dealer;
		this.shoe = Shoe.createShoe();
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
		playerList.clear();
		controllerList.clear();
	}

	public void getInitialBets() {
		for (int i = 0; i < controllerList.size(); i++) {
			initialBet(i);
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
					case HIT -> {
						hit(i);
						if (betPool.getBetStatusAt(i) == BetStatus.LOST) {
							mayBeSkipped[i] = true;
							playersLeft--;
						}
					}
					case SPLIT -> {
						try {
							split(i);
						} catch (IllegalArgumentException e) {
							mayBeSkipped[i] = true;
							playersLeft--;
						}
					}
					case SURRENDER -> {
						surrender(i);
						mayBeSkipped[i] = true;
						playersLeft--;
					}
					case DOUBLE_DOWN -> {
						try {
							doubleDown(i);
						} catch (IllegalArgumentException e) {
							hit(i);
						}
						mayBeSkipped[i] = true;
						playersLeft--;
					}
				}
			}
		}

		finishRound();
	}
}
