package model.service;

import model.Casino;
import model.bet.Bet;

public class BetLocker implements IBetLocker {
	private String lockerId;

	public String getLockerId() {
		return lockerId;
	}

	public void lockBet(String bettor, Bet bet) {
		var accountManager = AccountManager.getInstance();

		accountManager.transfer(Casino.getCasinoId(), lockerId, bet.getValue());
		accountManager.transfer(bettor, lockerId, bet.getValue());
	}

	public void unlockBet(String bettor, Bet bet, int winningAmount) {
		var accountManager = AccountManager.getInstance();

		accountManager.transfer(lockerId, bettor, winningAmount);
		accountManager.transfer(lockerId, Casino.getCasinoId(), 2 * bet.getValue() - winningAmount);
	}
}
