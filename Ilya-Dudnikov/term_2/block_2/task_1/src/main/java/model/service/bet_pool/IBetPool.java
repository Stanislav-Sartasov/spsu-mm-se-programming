package model.service.bet_pool;

import model.bet.Bet;

public interface IBetPool {
	public void addBet(Bet bet);

	public void clearPool();
}
