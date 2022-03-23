package service.bet_pool;

import bet.Bet;

public interface IBetPool {
	public void addBet(Bet bet);

	public void clearPool();
}
