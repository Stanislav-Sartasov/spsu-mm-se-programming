package service.bet_settler;

import bet.Bet;

public class BetSettler implements IBetSettler {
	public int settleBet(String bettor, Bet bet) {
		int betValue = bet.getValue();
		int playerWithdrawalAmount = 0;
		switch (bet.getBetStatus()) {
			case PENDING -> throw new IllegalArgumentException("The round is not over and you did not surrender, so you cannot settle your bet");
			case WON -> {
				playerWithdrawalAmount = 2 * betValue;
			}
			case WON_WITH_BLACKJACK -> {
				playerWithdrawalAmount = 5 * betValue / 2;
			}
			case LOST -> {
				playerWithdrawalAmount = 0;
			}
			case SURRENDERED -> {
				playerWithdrawalAmount = betValue / 2;
			}
			case DRAW -> {
				playerWithdrawalAmount = betValue;
			}
		}

		return playerWithdrawalAmount;
	}
}
