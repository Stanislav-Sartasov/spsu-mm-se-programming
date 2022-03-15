package model.service;

import model.Casino;
import model.bet.Bet;

public class BetPool implements IBetPool {
	private String poolId;

	public String getPoolId() {
		return poolId;
	}

	public void lockBet(String bettor, Bet bet) {
		var accountManager = AccountManager.getInstance();

		accountManager.transfer(Casino.getCasinoId(), poolId, bet.getValue());
		accountManager.transfer(bettor, poolId, bet.getValue());
	}

	public void unlockBet(String bettor, Bet bet, int winningAmount) {
		var accountManager = AccountManager.getInstance();

		accountManager.transfer(poolId, bettor, winningAmount);
		accountManager.transfer(poolId, Casino.getCasinoId(), 2 * bet.getValue() - winningAmount);
	}
}
