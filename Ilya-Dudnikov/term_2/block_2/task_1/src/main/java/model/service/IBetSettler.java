package model.service;

import model.bet.Bet;

public interface IBetSettler {
	public int settleBet(String bettor, Bet bet);
}
