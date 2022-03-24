package service.bet_pool;

import bet.Bet;

public interface IBetPool {
	public void addBet(Bet bet);

	public int getBetValueAt(int pos);

	public void clearPool();
}
