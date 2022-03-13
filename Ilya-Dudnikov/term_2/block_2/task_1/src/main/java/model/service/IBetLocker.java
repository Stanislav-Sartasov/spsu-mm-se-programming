package model.service;

import model.bet.Bet;

public interface IBetLocker {
	public void lockBet(String bettor, Bet bet);

	public void unlockBet(String bettor, Bet bet, int winningAmount);
}
