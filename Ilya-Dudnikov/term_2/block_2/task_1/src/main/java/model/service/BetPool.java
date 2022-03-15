package model.service;

import model.bet.Bet;

import java.util.ArrayList;

public class BetPool implements IBetPool {
	private String poolId;
	private ArrayList<Bet> betList;

	public BetPool(String poolId) {
		this.poolId = poolId;
	}

	public String getPoolId() {
		return poolId;
	}

	public int getBetValueAt(int pos) {
		return betList.get(pos).getValue();
	}

	public void doubleBet(int pos) {
		betList.set(pos, new Bet(betList.get(pos).getValue() * 2));
	}

	public void addBet(Bet bet) {
		betList.add(bet);
	}

	public void removeBet(int pos) {
		betList.remove(pos);
	}
}
