package model.service.bet_settler;

import model.bet.Bet;

public interface IBetSettler {
	public int settleBet(String bettor, Bet bet);
}
