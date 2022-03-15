package model.service;

import model.bet.Bet;

public interface IBetPool {
	public void addBet(Bet bet);

	public void removeBet(int pos);
}
