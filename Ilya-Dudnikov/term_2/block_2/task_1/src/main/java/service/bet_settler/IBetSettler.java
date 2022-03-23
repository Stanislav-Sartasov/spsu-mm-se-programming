package service.bet_settler;

import bet.Bet;

public interface IBetSettler {
	public int settleBet(String bettor, Bet bet);
}
