package model.service.bet_pool;

import model.bet.Bet;
import model.bet.BetStatus;

import java.util.ArrayList;

public class BetPool implements IBetPool {
	private String poolId;
	private ArrayList<Bet> betList;

	public BetPool(String poolId) {
		this.poolId = poolId;
	}

	public int getTotalBets() {
		return betList.size();
	}

	public Bet getBetAt(int pos) {
		return betList.get(pos);
	}

	public String getPoolId() {
		return poolId;
	}

	public int getBetValueAt(int pos) {
		return betList.get(pos).getValue();
	}

	public BetStatus getBetStatusAt(int pos) {
		return betList.get(pos).getBetStatus();
	}

	public String getBettorIdAt(int pos) {
		return betList.get(pos).getBettorId();
	}

	public void doubleBet(int pos) {
		Bet previousBet = betList.get(pos);
		betList.set(pos, new Bet(previousBet.getBettorId(), previousBet.getValue() * 2));
	}

	public void insertBet(Bet bet, int pos) {
		betList.add(pos, bet);
	}

	public void addBet(Bet bet) {
		betList.add(bet);
	}

	public void clearPool() {
		betList.clear();
	}

	public void changeBetStatus(int pos, BetStatus newBetStatus) {
		betList.get(pos).changeBetStatus(newBetStatus);
	}
}
